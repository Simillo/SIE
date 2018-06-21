<template lang='pug'>
  sidebar
    md-dialog-confirm(
      :md-active.sync='active',
      md-title='Atenção',
      md-content='Deseja responder agora a pergunta?<br/>Após o envio <b>não</b> será mais possível alterá-la.',
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
          span {{getGrade(activity.Answer.Grade, activity.Weight)}}
          br
          span {{activity.Description}}
      .room-content
        div(v-if='loadedFiles && files.length > 0')
          upload(
            @update:files='files = $event',
            :fileName='"atividade"',
            :canUpload='false',
            :files='files',
            :title='"Arquivo(s) anexado(s) pelo professor"'
          )
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
          div(v-if='loadedAttachments')
            upload(
              @update:files='attachments = $event',
              :fileName='"anexo"',
              :canUpload='canIAnswer',
              :files='attachments',
              :title='labelAttachments'
            )
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

import EActivityState from '../../enums/EActivityState'

import { validationMixin } from 'vuelidate'
import { required } from 'vuelidate/lib/validators'

export default {
  components: {
    'sidebar': Student
  },
  mixins: [validationMixin],
  data () {
    return {
      files: [],
      attachments: [],
      loadedAttachments: false,
      loadedFiles: false,
      activity: {},
      active: false,
      labelAttachments: 'Anexe seus arquivos aqui',
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
    else this.loadedAttachments = true
  },
  methods: {
    loadActivity () {
      const params = this.$route.params
      this.service.loadActivity(params.roomCode, params.activityId)
        .then(res => {
          this.activity = res.body.entity
          this.files = this.activity.Uploads
          this.loadedFiles = this.files.length > 0
          this.form = res.body.entity.Answer
          this.attachments = this.form.Attachments
          this.loadedAttachments = this.attachments.length > 0
          this.canIAnswer = !res.body.entity.Answer.Answer && res.body.entity.CurrentState === EActivityState.InProgress.ordinal
          this.labelAttachments = this.loadedAttachments ? 'Arquivo(s) anexado por você' : ''
        })
        .catch(() => {
          this.$router.push('/student/my-rooms')
        })
    },
    async save () {
      const params = this.$route.params
      await this.service.answer(params.roomCode, params.activityId, this.form.Answer, this.attachments)
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
    },
    getGrade (grade, weight) {
      if (!grade) {
        if (this.activity.CurrentState === EActivityState.Done.ordinal) {
          if (!this.activity.Answer.Answer) return 'Atividade finalizada sem você ter respondido'
          else return 'Atividade foi finalizada sem ser avaliada'
        } else if (this.activity.Answer.Answer) return 'Atividade ainda não foi avaliada'
      }

      return grade ? `nota: ${(grade / weight * 100).toLocaleString('pt-BR', { maximumFractionDigits: 2 })}%` : 'Você ainda não respondeu'
    }
  }
}
</script>

<style lang='scss' scoped>
span {
  display: block;
}
</style>
