<template lang='pug'>
  box(:height='"450px"')
    md-switch.md-primary(
      v-model='envelope.Profile',
      @change='toggleIWannaBe') Quero ser {{profile}}
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
      },
      profile: 'estudante'
    }
  },
  methods: {
    next () {
      console.log(this.envelope)
      this.$router.push('password')
    },
    toggleIWannaBe () {
      this.profile = this.envelope.Profile ? 'professor' : 'estudante'
    }
  }
}
</script>

<style lang='scss'>
.md-switch.md-theme-default:not(.md-checked) {
  .md-switch-container {
    background-color: rgb(227, 238, 23);
  }
  .md-switch-thumb {
    background-color: rgb(168, 177, 3)!important;
  }
  .md-ripple-wave {
    background-color: rgb(168, 177, 3)!important;
  }
}
</style>
