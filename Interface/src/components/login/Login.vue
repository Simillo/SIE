<template lang='pug'>
  box(
    :hasClass='true',
    :height='"375px"')
    form(
      novalidate,
      @submit.prevent='login',
      @keypress.enter='login'
      )
      md-field(:class='messageClass')
        label E-mail
        md-input(
          type='email',
          v-model='envelope.Email',
          required)
        span.md-error E-mail inválido!
      md-field
        label Senha
        md-input(
          type='password',
          v-model='envelope.Password',
          required)
      .float-left
        router-link(to='/register')
          a Não possui conta?
      .float-right
        a Esqueceu a senha?
      div
        md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
          @click.prevent='login()') Entrar
</template>

<script>
import Box from '../shared/Box.vue'
import Login from '../../domain/Login'

import PersonService from '../../services/PersonService'

export default {
  components: {
    'box': Box
  },
  data () {
    return {
      envelope: new Login(),
      hasError: false
    }
  },
  async created () {
    this.service = new PersonService(this.$resource, this.$http)
    this.envelope.Email = 'simillonakai@gmail.com'
    this.envelope.Password = '123123a'
  },
  methods: {
    async login () {
      try {
        const res = await this.service.login(this.envelope)
        this.hasError = false
        if (res.status === 200) this.$router.push(res.entity)
      } catch (ex) {
        this.hasError = true
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
