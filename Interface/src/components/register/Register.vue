<template lang='pug'>
  box(:height='"500px"')
    form(
      novalidate,
      @submit.prevent='validate'
    )
      md-switch.md-primary(
        v-model='profileNumber',
        value='1',
        @change='toggleIWannaBe') Cadastrar como {{profile}}
      md-field(:class='getValidationClass("Name")')
        label(for='name') Nome
        md-input#name(
          name='name',
          v-model='form.Name')
        span.md-error(v-if='!$v.form.Name.required') Obrigatório!
      md-field(:class='getValidationClass("Email")')
        label(for='email') E-mail
        md-input#email(
          name='email',
          @keyup.prevent='resetCustomError("Email")'
          v-model='form.Email')
        span.md-error(v-if='!$v.form.Email.required') Obrigatório!
        span.md-error(v-else-if='!$v.form.Email.email') E-mail inválido!
        span.md-error(v-else-if='customErrors.Email.HasError') {{customErrors.Email.MessageError}}
      md-field(:class='getValidationClass("Cpf")')
        label(for='cpf') CPF
        md-input#cpf(
          name='cpf',
          v-mask='"###.###.###-##"',
          v-model='form.Cpf')
        span.md-error(v-if='!$v.form.Cpf.required') Obrigatório!
        span.md-error(v-else-if='!$v.form.Cpf.validarCpf') CPF inválido!
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
      md-datepicker(
          v-model='form.BirthDate',
          required)
        label Data de nascimento
      md-field(:class='getValidationClass("Sex")')
        label(for='sex') Eu sou...
        md-select#sex(
          v-model='form.Sex',
          name='sex')
          md-option(value='1') Homem
          md-option(value='2') Mulher
          md-option(value='3') Outro
      md-field(:class='getValidationClass("Password")')
        label(for='password') Senha
        md-input#password(
          type='password',
          v-model='form.Password')
        span.md-error(v-if='!$v.form.Password.required') Obrigatório!
        span.md-error(v-else-if='!$v.form.Password.minLength') A senha deve ter no mínimo 6 caracteres!
        span.md-error(v-else-if='!$v.form.Password.validPassword') A senha deve contém ao letra(s) e número(s)!
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
  minLength
} from 'vuelidate/lib/validators'
import { validarCpf, validPassword } from '../../services/Utils'

import PersonService from '../../services/PersonService'
import InstitutionService from '../../services/InstitutionService'

export default {
  components: {
    'box': Box
  },
  mixins: [validationMixin],
  data () {
    return {
      profileNumber: null,
      form: new Person(),
      profile: 'professor',
      customErrors: {
        Cpf: {},
        Email: {}
      },
      institutions: [],
      viewInstitution: null
    }
  },
  validations: {
    form: {
      Name: {
        required
      },
      Email: {
        required,
        email
      },
      Cpf: {
        required,
        validarCpf
      },
      Sex: {
        required
      },
      Password: {
        required,
        validPassword,
        minLength: minLength(6)
      }
    }
  },
  async created () {
    this.service = new PersonService(this.$http)
    this.institutionService = new InstitutionService(this.$resource)
  },
  methods: {
    save () {
      if (this.viewInstitution) this.form.Institution.Name = this.viewInstitution
      this.form.Profile = Number(this.profileNumber)
      this.service.savePerson(this.form)
        .then(() => this.$router.push('/'))
        .catch(err => this.handleCustomError(err.body.entity))
    },
    toggleIWannaBe () {
      this.profile = this.profileNumber ? 'estudante' : 'professor'
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
.md-switch.md-theme-default:not(.md-checked) {
  .md-switch-container {
    background-color: rgb(162, 219, 157)
  }
  .md-switch-thumb, .md-ripple-wave {
    background-color: rgb(87, 194, 87);
  }
}
</style>
