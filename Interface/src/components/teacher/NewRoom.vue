<template lang='pug'>
  sidebar
    box(
      :hasClass='true',
      :height='"375px"',
      :noLogo='true')
      form(
      novalidate,
      @submit.prevent='validate'
      @keypress.enter='validate'
      )
      md-field(:class='getValidationClass("Name")')
        label(for='name') Nome da sala
        md-input#name(
          name='name',
          v-model='form.Name')
        span.md-error(v-if='!$v.form.Name.required') Obrigatório!
      md-field(:class='getValidationClass("Code")')
        label(for='code') Código da sala
        md-input#code(
          name='code',
          v-model='form.Code')
        span.md-error(v-if='!$v.form.Code.required') Obrigatório!
      md-datepicker(v-model='form.ExpirationDate')
        label Data do fim da sala
      md-field
        label Descrição da sala
        md-textarea(v-model='form.Description')
      div
        md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
          @click.prevent='validate') Criar
</template>

<script>

import TeacherService from '../../services/TeacherService'
import Teacher from './Teacher.vue'
import Box from '../shared/Box.vue'
import NewRoom from '../../domain/NewRoom'

import { validationMixin } from 'vuelidate'
import { required } from 'vuelidate/lib/validators'

export default {
  components: {
    'sidebar': Teacher,
    'box': Box
  },
  mixins: [validationMixin],
  data () {
    return {
      form: new NewRoom()
    }
  },
  validations: {
    form: {
      Name: {
        required
      },
      Code: {
        required
      }
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    await this.service.load()
  },
  methods: {
    async save () {
      const res = await this.service.createRoom(this.form)
      console.log(res)
    },
    validate () {
      this.$v.$touch()
      if (!this.$v.$invalid) {
        this.save()
      }
    },
    getValidationClass (fieldName) {
      const field = this.$v.form[fieldName]
      if (field) {
        return {
          'md-invalid': field.$invalid && field.$dirty
        }
      }
    }
  }
}
</script>

<style lang='scss' scoped>

</style>
