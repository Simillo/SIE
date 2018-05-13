<template lang='pug'>
  sidebar
    md-table(
      v-model='rooms',
      md-sort='Name',
      md-sort-order='asc',
      md-card,
      md-fixed-header
    )
      md-table-toolbar
        .md-toolbar-section-start
          h1.md-title Salas disponíveis
      md-table-row(
        slot='md-table-row',
        slot-scope='{ item }'
      )
        md-table-cell(
          md-label='Nome da sala',
          md-sort-by='Name'
        ) {{item.Name}}
        md-table-cell(
          md-label='Código da sala',
          md-sort-by='Code'
        ) {{item.Code}}
        md-table-cell(
          md-label='Quantidade de alunos',
          md-sort-by='NumberOfStudents',
          md-numeric
        ) {{item.NumberOfStudents}}
        md-table-cell(
          md-label='Estado',
          md-sort-by='CurrentState'
        ) {{item.CurrentState}}
        md-table-cell(
          md-label='Ações'
        )
          .join(
            v-if='item.CanIJoin',
            @click.prevent='join(item)'
          )
            md-tooltip(md-direction='top') Entrar
            md-icon exit_to_app
</template>

<script>

import Student from './Student.vue'

import StudentService from '../../services/StudentService'

export default {
  components: {
    'sidebar': Student
  },
  data () {
    return {
      rooms: []
    }
  },
  created () {
    this.service = new StudentService(this.$http)
    this.loadRooms()
  },
  methods: {
    loadRooms () {
      this.service.loadRooms()
        .then(res => {
          this.rooms = res.data.entity
        })
    },
    join (room) {
      console.log(room)
    }
  }
}
</script>

<style lang='scss' scoped>
.join {
  cursor: pointer;
  width: 25px;
}
</style>
