<template lang='pug'>
  box(
    :hasClass='true',
    :height='"375px"')
    md-field
      label E-mail
      md-input(
        type='email',
        v-model='envelope.Email',
        required)
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
      envelope: new Login()
    }
  },
  async created () {
    this.service = new PersonService(this.$resource, this.$http)
    this.envelope.Email = 'simillonakai@gmail.com'
    this.envelope.Password = '123'
  },
  methods: {
    async login () {
      const res = await this.service.login(this.envelope)
      if (res.status === 200) this.$router.push('/dashboard')
    }
  }
}
</script>

<style scoped>
</style>
