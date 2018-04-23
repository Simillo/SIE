<template lang='pug'>
  box(:height='"500px"')
    form(
      novalidate,
      @submit.prevent='validate'
    )
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
          v-mask='"###.###.###-##"',
          v-model='envelope.Cpf',
          required)
      md-field
        label Instituição
        md-input(
          type='text',
          v-model='envelope.Institution')
      md-datepicker(
          v-model='envelope.BirthDate',
          required)
        label Data de nascimento
      md-field
        label Eu sou...
        md-select#sex(
          v-model='envelope.Sex',
          name='sex',
          required)
          md-option(value='1') Homem
          md-option(value='2') Mulher
          md-option(value='3') Outro
      md-field
        label Senha
        md-input(
          type='password',
          v-model='envelope.Password',
          required)
      div
        router-link(to='/')
          md-button.md-raised.md-primary.no-margin.float-left.pull-bottom Voltar
        md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
          @click.prevent='validate') Cadastrar
</template>

<script>

import Box from '../shared/Box.vue'
import Person from '../../domain/Person'
import { validationMixin } from 'vuelidate'
import {
  required,
  email,
  minLength,
  maxLength
} from 'vuelidate/lib/validators'

import PersonService from '../../services/PersonService'

export default {
  components: {
    'box': Box
  },
  mixins: [validationMixin],
  data () {
    return {
      envelope: new Person(),
      profile: 'estudante',
      isValid: new Person()
    }
  },
  validations: {
    envelope: {
      firstName: {
        required,
        minLength: minLength(3)
      },
      lastName: {
        required,
        minLength: minLength(3)
      },
      age: {
        required,
        maxLength: maxLength(3)
      },
      gender: {
        required
      },
      email: {
        required,
        email
      }
    }
  },
  async created () {
    this.service = new PersonService(this.$resource, this.$http)
  },
  methods: {
    async save () {
      await this.service.savePerson(this.envelope)
    },
    toggleIWannaBe () {
      this.profile = this.envelope.Profile ? 'professor' : 'estudante'
    },
    validate () {
      this.$v.$touch()
      if (!this.$v.$invalid) {
        this.save()
      }
    }
  }
}
</script>

<style lang='scss'>
.md-switch.md-theme-default:not(.md-checked) {
  .md-switch-container {
    background-color: #B39DDB
  }
  .md-switch-thumb, .md-ripple-wave {
    background-color: #7E57C2;
  }
}
</style>
