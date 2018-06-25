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
</template>

<script>
import Teacher from './Teacher.vue'
import TeacherService from '../../services/TeacherService.js'

import DashboardPie from '../shared/DashboardPie.js'

export default {
  components: {
    'dashboard-pie': DashboardPie,
    'sidebar': Teacher
  },
  data () {
    return {
      loaded: {
        rooms: false,
        activities: false
      },
      options: {responsive: false, maintainAspectRatio: false},
      data: {
        rooms: {},
        activities: {}
      }
    }
  },
  created () {
    const service = new TeacherService(this.$http)
    service.loadDashboard()
      .then(res => {
        this.data.rooms = res.body.entity.rooms
        this.data.activities = res.body.entity.activities
        this.loaded.rooms = true
        this.loaded.activities = true
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
