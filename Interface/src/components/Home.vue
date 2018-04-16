<template lang='pug'>
  .box
    .box-middle
      .box-inner
        .box-logo
        .content-box
          md-field
            label Email ou CPF
            md-input(
              v-model='emailOrCpf'
              required)
          div
            a Não possui conta?
          div
            md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(@click.prevent='next') Próximo
</template>

<script>
export default {
  name: 'Home',
  data () {
    return {
      emailOrCpf: ''
    }
  },
  methods: {
    next () {
      console.log(this.emailOrCpf)
    }
  }
}
</script>

<style scoped>
</style>
