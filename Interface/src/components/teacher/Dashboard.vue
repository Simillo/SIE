<template lang='pug'>
  sidebar
    .graphs
      md-card(v-if='loaded.rooms')
        md-card-header
          .md-title Minhas salas
        md-card-content
          dashboard-pie(:data='data.rooms', :options='options')
      md-card(v-if='loaded.activities')
        md-card-header
          .md-title Atividades
        md-card-content
          dashboard-pie(:data='data.activities', :options='options')
      md-card(v-if='loaded.studentsXRoom')
        md-card-header
          .md-title Alunos x Sala
        md-card-content
          dashboard-bar(:data='data.studentsXRoom', :options='options')
</template>

<script>
import Teacher from './Teacher.vue'
import TeacherService from '../../services/TeacherService.js'

import DashboardPie from '../shared/DashboardPie.js'
import DashboardBar from '../shared/DashboardBar.js'

export default {
  components: {
    'dashboard-pie': DashboardPie,
    'dashboard-bar': DashboardBar,
    'sidebar': Teacher
  },
  data () {
    return {
      loaded: {
        rooms: false,
        activities: false,
        studentsXRoom: false
      },
      options: {responsive: false, maintainAspectRatio: false},
      data: {
        rooms: {},
        activities: {},
        studentsXRoom: {}
      }
    }
  },
  created () {
    const service = new TeacherService(this.$http)
    service.loadDashboard()
      .then(res => {
        const dashboards = res.body.entity
        this.data.rooms = dashboards.rooms
        this.data.activities = dashboards.activities
        this.data.studentsXRoom = dashboards.studentsXRoom
        this.loaded.rooms = !!dashboards.rooms.labels.length
        this.loaded.activities = !!dashboards.activities.labels.length
        this.loaded.studentsXRoom = !!dashboards.studentsXRoom.labels.length
      })
  }
}
</script>

<style lang='scss' scoped>
.graphs {
  width: 100%;
  display: inline-flex;
  flex-wrap: wrap;
  justify-content: center;
}
.md-card {
  width: 450px;
  margin: 40px;
  .md-title{
    text-align: center;
  }
}
</style>
