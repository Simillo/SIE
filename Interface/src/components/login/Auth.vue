<template lang='pug'>
  box(:hasClass='true')
    md-field
      label Senha
      md-input(
        type='password',
        v-model='password'
        required)
    div
      a Esqueceu a senha?
    div
      router-link(to='/')
        md-button.md-raised.md-primary.no-margin.float-left.pull-bottom Voltar
      md-button.md-raised.md-primary.no-margin.float-right.pull-bottom Entrar
</template>

<script>

import Box from '../shared/Box.vue'

export default {
  components: {
    'box': Box
  },
  data () {
    return {
      password: ''
    }
  },
  methods: {
  }
}
</script>

<style scoped>
</style>
