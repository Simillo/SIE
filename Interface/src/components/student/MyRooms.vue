<template lang='pug'>
  sidebar
    .msg-feedback(v-if='rooms.length == 0')
      h3 Você ainda não está cadastrado em nenhuma sala :(
      router-link(to='/student/rooms')
        span Clique aqui para procurar uma sala
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
          h1.md-title Minhas salas
      md-table-row(
        slot='md-table-row',
        slot-scope='{ item }'
      )
        md-table-cell(
          md-label='Nome da sala',
          md-sort-by='Name'
        )
          router-link(:to='"/student/room/"+item.Code') {{item.Name}}
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
      this.service.loadMyRooms()
        .then(res => {
          this.rooms = res.data.entity
        })
    },
    join (room) {
      this.service.join(room.Code)
        .then(() => this.loadRooms())
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
