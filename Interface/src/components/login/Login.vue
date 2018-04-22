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
  },
  methods: {
    async login () {
      const res = await this.service.login(this.envelope)
      console.log(res)
      // this.$router.push('/dashboard')
    }
  }
}
</script>

<style scoped>
</style>
