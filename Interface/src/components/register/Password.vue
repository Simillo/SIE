<template lang='pug'>
  box(:hasClass='true',
      :height='"375px"')
    md-field
      label Senha
      md-input(
        type='password',
        v-model='envelope.Password',
        required)
    md-field
      label Confirmar senha
      md-input(
        type='password',
        v-model='envelope.ConfirmPassword',
        required)
    div
      router-link(to='/register')
        md-button.md-raised.md-primary.no-margin.float-left.pull-bottom Voltar
      md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
        @click.prevent='next()') Cadastrar
</template>

<script>

import Box from '../shared/Box.vue'

export default {
  components: {
    'box': Box
  },
  data () {
    return {
      envelope: {}
    }
  },
  methods: {
    next () {
      console.log(this.envelope)
      this.$router.push('password')
    }
  },
  created () {
    console.log(this.$store)
  }
}
</script>

<style scoped>
</style>
