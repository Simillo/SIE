<template lang='pug'>
  box(:height='"350px"')
    form(
      novalidate,
      @submit.prevent='validate'
    )
      md-field(:class='getValidationClass("Name")')
        label(for='name') Nome
        md-input#name(
          name='name',
          v-model='form.Name')
        span.md-error(v-if='!$v.form.Name.required') Obrigatório!
      md-field
        md-tooltip.margin-tooltip Não é possível editar esse campo!
        label(for='email') E-mail
        md-input#email(
          name='email',
          v-model='form.Email',
          disabled)
      md-field
        md-tooltip.margin-tooltip Não é possível editar esse campo!
        label(for='cpf') CPF
        md-input#cpf(
          name='cpf',
          v-mask='"###.###.###-##"',
          v-model='form.Cpf',
          disabled)
      md-autocomplete(
        v-model='viewInstitution',
        :md-options='institutions',
        @md-changed='getInstitutions',
        @md-opened='getInstitutions'
      )
        label Instituição
        template(
          slot='md-autocomplete-item',
          slot-scope='{ item }'
        ) {{item.Name}}
      md-field
        label(for='password') Senha
        md-input#password(
          type='password',
          v-model='form.Password')
      div
        router-link(:to='backDirection')
          md-button.md-raised.md-primary.no-margin.float-left.pull-bottom Voltar
        md-button.md-raised.md-primary.no-margin.float-right.pull-bottom(
          @click.prevent='validate') Editar
</template>

<script>

import Box from './Box.vue'
import EProfile from '../../enums/EProfile.js'
import Person from '../../domain/Person'
import { validationMixin } from 'vuelidate'
import {
  required
} from 'vuelidate/lib/validators'

import PersonService from '../../services/PersonService'
import InstitutionService from '../../services/InstitutionService'

export default {
  components: {
    'box': Box
  },
  mixins: [validationMixin],
  data () {
    return {
      form: new Person(),
      institutions: [],
      viewInstitution: null,
      backDirection: ''
    }
  },
  validations: {
    form: {
      Name: {
        required
      }
    }
  },
  async created () {
    this.service = new PersonService(this.$http)
    this.institutionService = new InstitutionService(this.$resource)
    this.loadData()
  },
  methods: {
    async loadData () {
      const res = await this.service.me()
      this.form = res.body.entity
      this.backDirection = this.form.Profile === EProfile.Teacher.ordinal ? '/teacher' : '/student'
      if (this.form.Institution.Id !== 0) this.viewInstitution = this.form.Institution.Name
    },
    save () {
      if (this.viewInstitution) this.form.Institution.Name = this.viewInstitution
      this.form.Profile = Number(this.profileNumber)
      this.service.updatePerson(this.form)
        .then(() => this.$router.push(this.backDirection))
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
    },
    async getInstitutions (query) {
      if (!query) query = ''
      else if (query.Name) query = query.Name
      const res = await this.institutionService.get(query)
      this.institutions = new Promise(resolve => {
        resolve(res.entity)
      })
      if (typeof this.viewInstitution === 'object') {
        if (this.viewInstitution && this.viewInstitution.Name) {
          this.viewInstitution = this.viewInstitution.Name
        }
      }
    }
  }
}
</script>

<style lang='scss'>

</style>
