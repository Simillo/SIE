<template lang='pug'>
  sidebar
    box(
      :hasClass='true',
      :height='"375px"',
      :titleProp='"Nova sala"')
      form(
      novalidate,
      @submit.prevent='validate'
      @keypress.enter='validate'
      )
      md-field(:class='getValidationClass("Name")')
        label(for='name') Nome da sala
        md-input#name(
          name='name',
          @keyup.prevent='resetCustomError("Name")',
          v-model='form.Name')
        span.md-error(v-if='!$v.form.Name.required') Obrigatório!
        span.md-error(v-else-if='customErrors.Name.HasError') {{customErrors.Name.MessageError}}
      md-field(:class='getValidationClass("Code")')
        label(for='code') Código da sala
        md-input#code(
          name='code',
          @keyup.prevent='resetCustomError("Code")',
          v-model='form.Code')
        span.md-error(v-if='!$v.form.Code.required') Obrigatório!
        span.md-error(v-else-if='customErrors.Code.HasError') {{customErrors.Code.MessageError}}
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
      form: new NewRoom(),
      customErrors: {
        Name: {},
        Code: {}
      }
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
  },
  methods: {
    async save () {
      this.service.createRoom(this.form)
        .then()
        .catch(err => this.handleCustomError(err.body.entity))
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
          'md-invalid': (field.$invalid && field.$dirty) || (this.customErrors[fieldName] && this.customErrors[fieldName].HasError)
        }
      }
    },
    handleCustomError (data) {
      if (!data) return

      data.forEach(d => {
        this.customErrors[d.Property] = d
      })
    },
    resetCustomError (field) {
      if (this.customErrors[field]) this.customErrors[field].HasError = false
    }
  }
}
</script>

<style lang='scss' scoped>

</style>
