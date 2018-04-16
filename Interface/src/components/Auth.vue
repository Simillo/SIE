<template lang='pug'>
  .box
    .box-middle
      .box-inner
        .box-logo
        .content-box
          md-field
            label Senha
            md-input(
              type='password',
              v-model='password'
              required)
          div
            a Não possui conta?
          div
            md-button.md-raised.md-primary.no-margin.float-right.pull-bottom Entrar
</template>

<script>
export default {
  name: 'Auth',
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
