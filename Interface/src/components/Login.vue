<template lang='pug'>
  .box
    .box-middle
      .box-inner
        .box-logo
        .content-box
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
export default {
  name: 'Login',
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
