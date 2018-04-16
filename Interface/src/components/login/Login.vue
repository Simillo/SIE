<template lang='pug'>
  box(
    :hasClass='true',
    :height='"375px"')
    md-field
      label Email ou CPF
      md-input(
        type='text',
        v-model='emailOrCpf'
        required)
    div
      router-link(to='/register')
        a Não possui conta?
    div
      md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
        @click.prevent='next()') Próximo
</template>

<script>
import Box from '../shared/Box.vue'

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
