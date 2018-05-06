<template lang='pug'>
  sidebar
    pre {{room}}
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
      room: {}
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    try {
      const res = await this.service.loadRoom(this.$route.params.roomCode)
      this.room = res.body.entity
    } catch (ex) {
      this.$router.push('/teacher/my-rooms')
    }
  }
}
</script>

<style lang='scss' scoped>
</style>
