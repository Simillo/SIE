<template lang='pug'>
  sidebar
    div(v-if='loaded')
      dashboard-my-rooms(:data='data', :options='options')
</template>

<script>
import Teacher from './Teacher.vue'
import TeacherService from '../../services/TeacherService.js'

import DashboardMyRooms from './DashboardMyRooms.js'

export default {
  components: {
    'dashboard-my-rooms': DashboardMyRooms,
    'sidebar': Teacher
  },
  data () {
    return {
      loaded: false,
      options: {responsive: false, maintainAspectRatio: false},
      data: {}
    }
  },
  created () {
    const service = new TeacherService(this.$http)
    service.loadDashboard()
      .then(res => {
        this.data = res.body.entity
        this.loaded = true
        console.log(this.data)
      })
  }
}
</script>

<style lang='scss' scoped>
</style>
