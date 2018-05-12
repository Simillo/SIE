<template lang='pug'>
  box(
    :hasClass='true',
    :height='"375px"')
    form(
      novalidate,
      @submit.prevent='validate'
      @keypress.enter='validate'
      )
      md-field(:class='getValidationClass("Email")')
        label(for='email') E-mail
        md-input#email(
          name='email',
          v-model='form.Email')
        span.md-error(v-if='!$v.form.Email.required') Obrigatório!
        span.md-error(v-else-if='!$v.form.Email.email') E-mail inválido!
      md-field(:class='getValidationClass("Password")')
        label(for='password') Senha
        md-input#password(
          type='password',
          v-model='form.Password')
        span.md-error(v-if='!$v.form.Password.required') Obrigatório!
      .float-left
        router-link(to='/register')
          a Não possui conta?
      .float-right
        a Esqueceu a senha?
      div
        md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
          @click.prevent='validate') Entrar
</template>

<script>
import Box from '../shared/Box.vue'
import Login from '../../domain/Login'
import { validationMixin } from 'vuelidate'
import {
  required,
  email
} from 'vuelidate/lib/validators'
import PersonService from '../../services/PersonService'

export default {
  components: {
    'box': Box
  },
  mixins: [validationMixin],
  data () {
    return {
      form: new Login(),
      hasError: false
    }
  },
  validations: {
    form: {
      Email: {
        required,
        email
      },
      Password: {
        required
      }
    }
  },
  async created () {
    this.service = new PersonService(this.$http)
    this.form.Email = 'simillonakai@gmail.com'
    this.form.Password = '123123a'
  },
  methods: {
    async login () {
      try {
        const res = await this.service.login(this.form)
        this.hasError = false
        if (res.status === 200) this.$router.push(res.entity)
      } catch (ex) {
        this.hasError = true
      }
    },
    validate () {
      this.$v.$touch()
      if (!this.$v.$invalid) {
        this.login()
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
  },
  computed: {
    messageClass () {
      return {
        'md-invalid': this.hasError
      }
    }
  }
}
</script>

<style scoped>
</style>
