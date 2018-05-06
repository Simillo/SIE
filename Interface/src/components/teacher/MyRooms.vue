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
          h1.md-title Minhas salas
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
</template>

<script>

import TeacherService from '../../services/TeacherService'
import Teacher from './Teacher.vue'

export default {
  components: {
    'sidebar': Teacher
  },
  data () {
    return {
      searched: [],
      rooms: []
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    const res = await this.service.myRooms()
    this.rooms = res.data.entity
  }
}
</script>

<style lang='scss' scoped>
</style>
