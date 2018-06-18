<template lang='pug'>
  sidebar
    .room-container
      .room-head
        .room-head-name
          span.room-name {{activity.Name}}
          span.room-code  {{activity.Code}}
        .room-head-description(v-if='activity.Description')
          span {{activity.Description}}
      .room-content
        form(
          novalidate,
          @submit.prevent='validate'
        )
          md-field(:class='getValidationClass("Title")')
            label(for='title') Título da atividade
            md-input#title(
              name='title',
              :disabled='!canIEdit',
              v-model='form.Title')
            span.md-error(v-if='!$v.form.Title.required') Obrigatório!

          div(v-if='loaded')
            upload(
              @update:files='files = $event',
              :fileName='"atividade"',
              :canUpload='true',
              :files='files'
            )

          md-field(:class='getValidationClass("Description")')
            label(for='description') Descrição da atividade
            md-textarea#description(
              name='description',
              :disabled='!canIEdit',
              v-model='form.Description'
            )
            span.md-error(v-if='!$v.form.Description.required') Obrigatório!
          .md-inline
            md-field(:class='getValidationClass("Weight")')
              label(for='weight') Peso
              md-input#weight(
                name='weight',
                :disabled='!canIEdit',
                type='number',
                v-model='form.Weight')
              span.md-error(v-if='!$v.form.Weight.required') Obrigatório!
              span.md-error(v-else-if='!$v.form.Weight.number') O peso deve ser um número!
              span.md-error(v-else-if='!$v.form.Weight.positive') O peso deve ser maior que 0!
            md-datepicker(
              v-if='canIEdit',
              v-model='form.ExpirationDate'
            )
              label Data final para entrega
            .date-shown(
              v-if='!canIEdit && !!form.ExpirationDate'
            )
              span Data final para entrega:
              span {{form.ExpirationDate | date}}
          div
            router-link(:to='"/teacher/room/"+$route.params.roomCode')
              md-button.md-raised.md-primary.no-margin.float-left Voltar
            md-button.md-raised.md-primary.no-margin.float-right(
              v-if='canIEdit',
              @click.prevent='validate'
            ) {{isEditing ? 'Salvar Alterações' : 'Criar Atividade'}}

        .room-content-activities-item(
            v-for='(activity, index) in searched',
            :key='activity.Id'
          )
            .activities-sub-head
              span.activities-sub-title Entregue {{activity.SentDate | date}} por {{activity.Name}}
            br
            .activities-description-container
              span.activities-description {{activity.Answer}}
            br
            .activities-actions
              .activities-actions-item(@click.prevent='toggleGrade(index)')
                md-icon.md-size note_add
                  md-tooltip(md-direction='top') Avaliar
              .activities-actions-grade(v-if='activity.Open')
                md-field
                  label(for='grade') Nota (máximo: {{form.Weight}})
                  md-input#grade(
                    name='grade',
                    type='number',
                    :max='form.Weight',
                    v-model='activity.Grade'
                  )
                md-field
                  label(for='feedback') Feedback
                  md-input#feedback(
                    name='feedback',
                    v-model='activity.Feedback'
                  )
                .evaluate-actions
                  .activities-actions-item(@click.prevent='evaluate(activity, index)')
                    md-icon.md-size check
                      md-tooltip(md-direction='top') Salvar
                  .activities-actions-item(@click.prevent='toggleGrade(index)')
                    md-icon.md-size close
                      md-tooltip(md-direction='top') Cancelar
</template>

<script>

import TeacherService from '../../services/TeacherService'
import Teacher from './Teacher.vue'

import NewActivity from '../../domain/NewActivity'

import ERoomState from '../../enums/ERoomState'

import { validationMixin } from 'vuelidate'
import { required } from 'vuelidate/lib/validators'
import { number, positive } from '../../services/Utils'

export default {
  components: {
    'sidebar': Teacher
  },
  mixins: [validationMixin],
  data () {
    return {
      loaded: false,
      files: [],
      search: '',
      searched: [],
      original: [],
      activity: {},
      form: new NewActivity(),
      isEditing: !!this.$route.params.activityId,
      canIEdit: true
    }
  },
  validations: {
    form: {
      Title: {
        required
      },
      Description: {
        required
      },
      Weight: {
        required,
        number,
        positive
      }
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    if (this.$route.params.activityId) this.loadActivity()
    else {
      this.loadRoom()
      this.loaded = true
    }
  },
  methods: {
    async loadActivity () {
      try {
        this.search = ''
        this.searched = []
        const params = this.$route.params
        const res = await this.service.loadActivity(params.roomCode, params.activityId)
        this.activity = res.body.entity
        this.canIEdit = this.activity.CurrentState === ERoomState.Building.ordinal
        this.form = {
          Title: this.activity.Name,
          Description: this.activity.Description,
          Weight: this.activity.Weight,
          ExpirationDate: this.activity.ExpirationDate
        }
        this.files = this.activity.Uploads
        this.loaded = true
        this.searched = this.activity.Answers.map(a => {
          a.Open = false
          return a
        })
        this.original = [...this.searched]
      } catch (ex) {
        this.$router.push('/teacher/my-rooms')
      }
    },
    async loadRoom () {
      try {
        const res = await this.service.loadRoom(this.$route.params.roomCode)
        this.activity = res.body.entity
      } catch (ex) {
        this.$router.push('/teacher/my-rooms')
      }
    },
    validate () {
      console.log(this.files)
      this.$v.$touch()
      if (!this.$v.$invalid) {
        this.save()
      }
    },
    save () {
      const params = this.$route.params
      this.form.Id = params.activityId || 0
      this.form.Files = this.files
      this.service.saveOrUpdateActivity(this.form, params.roomCode)
        .then(response => {
          this.$router.push(`/teacher/room/${this.$route.params.roomCode}`)
        })
        .catch(err => {
          console.log(err)
          if (err.body.status === 401) this.$router.push('/teacher/my-rooms')
        })
    },
    getValidationClass (fieldName) {
      const field = this.$v.form[fieldName]
      if (field) {
        return {
          'md-invalid': field.$invalid && field.$dirty
        }
      }
    },
    toggleGrade (index) {
      this.original[index].Open = !this.original[index].Open
      this.searched = [...this.original]
    },
    evaluate (activity, index) {
      this.service.evaluate(activity, this.$route.params.roomCode)
        .then(() => {
          this.toggleGrade(index)
        })
    }
  }
}
</script>

<style lang='scss' scoped>
.input-search {
  width: 50%;
}
.md-inline {
  display: flex;
}

.date-shown {
  width: 100%;
  margin-left: 20px;
  span {
    display: block
  }
}
.md-speed-dial {
  float: right;
  .md-tooltip {
    margin-left: 20px;
  }
  .md-speed-dial-content {
    position: absolute;
    margin: 55px 0px auto 9px;
  }
}
.room-content-activities-item {
  border: 1px solid #ccc;
  min-height: 20px;
    margin: 20px 0 20px;
  &:first-of-type {
    margin: 100px 0 20px;
  }
  div {
    margin: 5px;
    display: block;
  }
  .activities-head {
    .activities-name {
      float: left;
      font-size: 20px;
      font-weight: bold;
    }
    .activities-state {
      float: right;
    }
  }
  .activities-actions {
    width: 50%;
    .activities-actions-item {
      width: 30px;
      height: 30px;
      cursor: pointer;
      i {
        background: #ccc;
        border-radius: 100%;
        padding: 15px;
        color: black;
      }
    }
    .activities-actions-grade {
      display: inline;
      .md-field{
        width: calc(100% - 100px);
        input {
          width: 100%;
        }
      }
      .evaluate-actions {
        display: inline-flex;
        float: right;
        margin-top: -35px;
      }
    }
  }
}
</style>
