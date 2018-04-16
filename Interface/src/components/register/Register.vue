<template lang='pug'>
  box
    md-field
      label Nome
      md-input(
        type='text',
        v-model='envelope.Name',
        required)
    md-field
      label E-mail
      md-input(
        type='email',
        v-model='envelope.Email',
        required)
    md-field
      label CPF
      md-input(
        type='text',
        v-model='envelope.Cpf',
        required)
    md-field
      label Instituição
      md-input(
        type='text',
        v-model='envelope.Instituition')
    md-datepicker(
        v-model='envelope.BirthDate',
        required)
      label Data de nascimento
    md-field
      label Eu sou...
      md-select#sex(
        v-model='envelope.Sex',
        name='sex')
        md-option(value='1') Homem
        md-option(value='2') Mulher
        md-option(value='3') Outro
    div
      router-link(to='/')
        md-button.md-raised.md-primary.no-margin.float-left.pull-bottom Voltar
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
      envelope: {
        BirthDate: null
      }
    }
  },
  methods: {
    next () {
      console.log(this.envelope)
      this.$router.push('password')
    }
  }
}
</script>

<style scoped>
</style>
