<template lang='pug'>
  box(
    :hasClass='true',
    :height='"375px"')
    md-field
      label Email ou CPF
      md-input(
        type='text',
        v-model='envelope.EmailOrCpf',
        required)
    md-field
      label Senha
      md-input(
        type='password',
        v-model='envelope.Password',
        @change='validate',
        required)
    .float-left
      router-link(to='/register')
        a Não possui conta?
    .float-right
      a Esqueceu a senha?
    div
      md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
        @click.prevent='next()') Entrar
</template>

<script>
import Box from '../shared/Box.vue'
import Login from '../../domain/Login'
import MPerson from '../../domain/MPerson'

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
    this.service = new PersonService(this.$resource)
  },
  methods: {
    next () {
      console.log(new MPerson())
    },
    validate () {
      console.log(this.envelope.emailOrCpf)
    }
  }
}
</script>

<style scoped>
</style>
