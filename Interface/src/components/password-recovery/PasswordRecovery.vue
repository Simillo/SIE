<template lang='pug'>
  box(
    :hasClass='true',
    :height='"200px"')
    form(
      v-if='!hasToken',
      novalidate,
      @submit.prevent='validate',
      @keypress.enter='validate'
    )
      md-field(:class='getValidationClass("Email")')
        label(for='email') E-mail de recuperação
        md-input#email(
          name='email',
          v-model='form.Email')
        span.md-error(v-if='!$v.form.Email.required') Obrigatório!
        span.md-error(v-else-if='!$v.form.Email.email') E-mail inválido!
      div
        h4 Ao clicar em 'SOLICITAR' será enviado um link de recuperação para o e-mail cadastrado.
      div
        router-link(to='/')
          md-button.md-raised.md-primary.no-margin.float-left.pull-bottom Voltar
        md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
          @click.prevent='validate') Solicitar
    form(
      v-else
      novalidate,
      @submit.prevent='validatePassword',
      @keypress.enter='validatePassword'
    )
      md-field(:class='getValidationClass("Password")')
        label(for='password') Nova senha
        md-input#password(
          name='password',
          type='password',
          v-model='form.Password')
        span.md-error(v-if='!$v.form.Password.required') Obrigatório!
        span.md-error(v-else-if='!$v.form.Password.minLength') A senha deve ter no mínimo 6 caracteres!
        span.md-error(v-else-if='!$v.form.Password.validPassword') A senha deve contém ao letra(s) e número(s)!
      div
        router-link(to='/')
          md-button.md-raised.md-primary.no-margin.float-left.pull-bottom Voltar
        md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
          @click.prevent='validatePassword') Alterar a senha
</template>

<script>
import Box from '../shared/Box.vue'
import { validationMixin } from 'vuelidate'
import {
  minLength,
  required,
  email
} from 'vuelidate/lib/validators'
import { validPassword } from '../../services/Utils'
import PersonService from '../../services/PersonService'

export default {
  components: {
    'box': Box
  },
  mixins: [validationMixin],
  data () {
    return {
      token: this.$route.params.token,
      form: {
        Email: '',
        Password: ''
      },
      hasToken: false
    }
  },
  validations: {
    form: {
      Email: {
        required,
        email
      },
      Password: {
        required,
        validPassword,
        minLength: minLength(6)
      }
    }
  },
  async created () {
    this.hasToken = !!this.token
    this.service = new PersonService(this.$http)
    if (this.hasToken) {
      this.service.getInfoByToken(this.token)
        .then(res => {
          this.form.Email = res.data.entity.Email
        })
        .catch(() => {
          this.$router.push('/')
        })
    }
  },
  methods: {
    recovery () {
      this.service.recovery(this.form.Email)
        .then(() => {
          this.$router.push('/')
        })
    },
    updatePassword () {
      const obj = {
        Token: this.token,
        Password: this.form.Password
      }
      this.service.updatePassword(obj)
        .then(() => {
          this.$router.push('/')
        })
    },
    validate () {
      this.$v.$touch()
      if (!this.$v.form.Email.$invalid) {
        this.recovery()
      }
    },
    validatePassword () {
      this.$v.$touch()
      if (!this.$v.$invalid) {
        this.updatePassword()
      }
    },
    getValidationClass (fieldName) {
      const field = this.$v.form[fieldName]
      if (field) {
        return {
          'md-invalid': (field.$invalid && field.$dirty)
        }
      }
    }
  }
}
</script>

<style scoped>
</style>
