<template lang='pug'>
  box(
    :hasClass='true',
    :height='"375px"')
    md-field
      label Email ou CPF
      md-input(
        type='text',
        v-model='envelope.emailOrCpf',
        v-mask:emailOrCpf='',
        required)
    md-field
      label Senha
      md-input(
        type='password',
        v-model='envelope.password',
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
import mask from '../../directives/mask'

export default {
  components: {
    'box': Box
  },
  directives: {
    'mask': mask
  },
  data () {
    return {
      envelope: {}
    }
  },
  methods: {
    next () {
      console.log('ok')
    },
    validate () {
      console.log(this.envelope.emailOrCpf)
    }
  }
}
</script>

<style scoped>
</style>
