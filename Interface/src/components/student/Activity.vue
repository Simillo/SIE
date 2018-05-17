<template lang='pug'>
  sidebar
    md-dialog-confirm(
      :md-active.sync='active',
      md-title='Atenção',
      md-content='Deseja responder agora a pergunta?<br/>Após o envio <b>não</b> será mais possível altera-la.',
      md-confirm-text='Continuar',
      md-cancel-text='Cancelar',
      @md-confirm='validate'
    )
    .room-container
      .room-head
        .room-head-name
          span.room-name {{activity.Name}}
          span.room-code  {{activity.Code}}
        .room-head-description(v-if='activity.Description')
          span peso: {{activity.Weight}}
          span {{activity.Description}}
      .room-content
        form(
          novalidate,
          @submit.prevent='active = true'
        )
          md-field(:class='getValidationClass("Answer")')
            label(for='answer') Resposta
            md-textarea#answer(
              name='answer',
              v-model='form.Answer',
              :disabled='!canIAnswer'
            )
            span.md-error(v-if='!$v.form.Answer.required') Obrigatório!
          div
            router-link(:to='"/student/room/"+$route.params.roomCode')
              md-button.md-raised.md-primary.no-margin.float-left Voltar
            md-button.md-raised.md-primary.no-margin.float-right(
              type='submit',
              v-if='canIAnswer'
            ) Salvar Resposta
</template>

<script>
import StudentService from '../../services/StudentService'
import Student from './Student.vue'

import { validationMixin } from 'vuelidate'
import { required } from 'vuelidate/lib/validators'

export default {
  components: {
    'sidebar': Student
  },
  mixins: [validationMixin],
  data () {
    return {
      activity: {},
      active: false,
      form: {
        Answer: ''
      },
      canIAnswer: true
    }
  },
  validations: {
    form: {
      Answer: {
        required
      }
    }
  },
  async created () {
    this.service = new StudentService(this.$http)
    if (this.$route.params.activityId) this.loadActivity()
  },
  methods: {
    loadActivity () {
      const params = this.$route.params
      this.service.loadActivity(params.roomCode, params.activityId)
        .then(res => {
          this.activity = res.body.entity
          this.form = res.body.entity.Answer
          this.canIAnswer = !res.body.entity.Answer.Answer
        })
        .catch(() => {
          this.$router.push('/student/my-rooms')
        })
    },
    async save () {
      const params = this.$route.params
      await this.service.answer(params.roomCode, params.activityId, this.form.Answer)
        .then(() => this.$router.push(`/student/room/${this.$route.params.roomCode}`))
        .catch(err => {
          if (err.body.status === 401) this.$router.push('/student/my-rooms')
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
    validate () {
      this.$v.$touch()
      if (!this.$v.$invalid) {
        this.save()
      }
    }
  }
}
</script>

<style lang='scss' scoped>
span {
  display: block;
}
</style>
