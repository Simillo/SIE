<template lang='pug'>
  sidebar
    .msg-feedback(v-if='rooms.length == 0')
      h3 Não existem salas disponíveis ainda :(
    md-table(
      v-else,
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
        )
          span(v-if='item.CanIJoin') {{item.Name}}
          router-link(
            v-else,
            :to='"/student/room/"+item.Code') {{item.Name}}
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
          .action(
            v-if='item.CanIJoin',
            @click.prevent='join(item)'
          )
            md-tooltip(md-direction='left') Entrar
            md-icon exit_to_app
          .action(
            v-else,
            @click.prevent='exit(item)'
          )
            md-tooltip(md-direction='left') Sair
            md-icon close
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
      this.service.join(room.Code)
        .then(() => this.loadRooms())
    },
    exit (room) {
      this.service.exitRoom(room.Code)
        .then(res => this.loadRooms())
    }
  }
}
</script>

<style lang='scss' scoped>
.action {
  cursor: pointer;
  width: 25px;
}
</style>
