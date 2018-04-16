<template lang='pug'>
  box
    md-field
      label Email ou CPF
      md-input(
        type='text',
        v-model='emailOrCpf'
        required)
    div
      a(to='/register') Não possui conta?
    div
      md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
        @click.prevent='next()') Próximo
</template>

<script>
import Box from './shared/Box.vue'

export default {
  components: {
    'box': Box
  },
  data () {
    return {
      emailOrCpf: ''
    }
  },
  methods: {
    next () {
      console.log('ok')
      this.$router.push('auth')
    }
  }
}
</script>

<style scoped>
</style>
