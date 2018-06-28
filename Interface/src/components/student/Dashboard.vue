<template lang='pug'>
  sidebar
    .graphs
      md-card(v-if='loaded.answers')
        md-card-header
          .md-title Minhas respostas
        md-card-content
          dashboard-pie(:data='data.answers', :options='options')
</template>

<script>
import Teacher from './Student.vue'
import StudentService from '../../services/StudentService.js'

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
        answers: false
      },
      options: {responsive: false, maintainAspectRatio: false},
      data: {
        answers: {}
      }
    }
  },
  created () {
    const service = new StudentService(this.$http)
    service.loadDashboard()
      .then(res => {
        const dashboards = res.body.entity
        this.data.answers = dashboards.answers
        this.loaded.answers = !!dashboards.answers.labels.length
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
