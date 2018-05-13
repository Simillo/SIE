<template lang='pug'>
  sidebar
    //- .room-container
    //-   .room-head
    //-     .room-head-name
    //-       span.room-name {{activity.Name}}
    //-       span.room-code  {{activity.Code}}
    //-     .room-head-description(v-if='activity.Description')
    //-       span {{activity.Description}}
    //-   .room-content
    //-     form(
    //-       novalidate,
    //-       @submit.prevent='validate'
    //-     )
    //-       md-field(:class='getValidationClass("Title")')
    //-         label(for='title') Título da atividade
    //-         md-input#title(
    //-           name='title',
    //-           :disabled='!canIEdit',
    //-           v-model='form.Title')
    //-         span.md-error(v-if='!$v.form.Title.required') Obrigatório!
    //-       md-field(:class='getValidationClass("Description")')
    //-         label(for='description') Descrição da atividade
    //-         md-textarea#description(
    //-           name='description',
    //-           :disabled='!canIEdit',
    //-           v-model='form.Description'
    //-         )
    //-         span.md-error(v-if='!$v.form.Description.required') Obrigatório!
    //-       .md-inline
    //-         md-field(:class='getValidationClass("Weight")')
    //-           label(for='weight') Peso
    //-           md-input#weight(
    //-             name='weight',
    //-             :disabled='!canIEdit',
    //-             type='number',
    //-             v-model='form.Weight')
    //-           span.md-error(v-if='!$v.form.Weight.required') Obrigatório!
    //-           span.md-error(v-else-if='!$v.form.Weight.number') O peso deve ser um número!
    //-           span.md-error(v-else-if='!$v.form.Weight.positive') O peso deve ser maior que 0!
    //-         md-datepicker(
    //-           v-if='canIEdit',
    //-           v-model='form.ExpirationDate'
    //-         )
    //-           label Data final para entrega
    //-         .date-shown(
    //-           v-if='!canIEdit && !!form.ExpirationDate'
    //-         )
    //-           span Data final para entrega:
    //-           span {{form.ExpirationDate | date}}
    //-       div
    //-         router-link(:to='"/teacher/room/"+$route.params.roomCode')
    //-           md-button.md-raised.md-primary.no-margin.float-left Voltar
    //-         md-button.md-raised.md-primary.no-margin.float-right(
    //-           v-if='canIEdit',
    //-           @click.prevent='validate'
    //-         ) {{isEditing ? 'Salvar Alterações' : 'Criar Atividade'}}
</template>

<script>

import Teacher from './Teacher.vue'

export default {
  components: {
    'sidebar': Teacher
  },
  data () {
    return {
    }
  },
  async created () {
  },
  methods: {
  }
}
</script>

<style lang='scss' scoped>
// .input-search {
//   width: 50%;
// }
// .md-inline {
//   display: flex;
// }

// .date-shown {
//   width: 100%;
//   margin-left: 20px;
//   span {
//     display: block
//   }
// }

</style>
