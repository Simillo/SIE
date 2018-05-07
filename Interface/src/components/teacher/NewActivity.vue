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
              v-model='form.Title')
            span.md-error(v-if='!$v.form.Title.required') Obrigatório!
          md-field(:class='getValidationClass("Description")')
            label(for='description') Descrição da atividade
            md-textarea#description(
              name='description',
              v-model='form.Description'
            )
            span.md-error(v-if='!$v.form.Description.required') Obrigatório!
          .md-inline
            md-field(:class='getValidationClass("Weight")')
              label(for='weight') Peso
              md-input#weight(
                name='weight',
                type='number',
                v-model='form.Weight')
              span.md-error(v-if='!$v.form.Weight.required') Obrigatório!
              span.md-error(v-else-if='!$v.form.Weight.number') O peso deve ser um número!
              span.md-error(v-else-if='!$v.form.Weight.positive') O peso deve ser maior que 0!
            md-datepicker(v-model='form.ExpirationDate')
              label Data final para entrega
          div
            router-link(:to='"/teacher/room/"+$route.params.roomCode')
              md-button.md-raised.md-primary.no-margin.float-left Voltar
            md-button.md-raised.md-primary.no-margin.float-right(
              @click.prevent='validate') {{isEditing ? 'Salvar Alterações' : 'Criar Atividade'}}
</template>

<script>

import TeacherService from '../../services/TeacherService'
import Teacher from './Teacher.vue'

import NewActivity from '../../domain/NewActivity'

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
      activity: {},
      form: new NewActivity(),
      isEditing: !!this.$route.params.activityId
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
    else this.loadRoom()
  },
  methods: {
    async loadActivity () {
      try {
        const params = this.$route.params
        const res = await this.service.loadActivity(params.roomCode, params.activityId)
        this.activity = res.body.entity
        this.form = {
          Title: this.activity.Name,
          Description: this.activity.Description,
          Weight: this.activity.Weight,
          ExpirationDate: this.activity.ExpirationDate
        }
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
      this.$v.$touch()
      if (!this.$v.$invalid) {
        this.save()
      }
    },
    async save () {
      const params = this.$route.params
      this.form.Id = params.activityId || 0
      await this.service.saveOrUpdateActivity(this.form, params.roomCode)
        .then(() => this.$router.push(`/teacher/room/${this.$route.params.roomCode}`))
        .catch(err => {
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

</style>
