<template lang='pug'>
  sidebar
    .room-container
      .room-head
        .room-head-name
          span.room-name {{activity.Name}}
          span.room-code  {{activity.Code}}
        .room-head-description(v-if='activity.Description')
          span {{activity.Description}}
      .room-content
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
      activity: {}
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    this.loadData()
  },
  methods: {
    async loadData () {
      try {
        const res = await this.service.loadRoom(this.$route.params.roomCode)
        this.activity = res.body.entity
      } catch (ex) {
        this.$router.push('/teacher/my-rooms')
      }
    }
  }
}
</script>

<style lang='scss' scoped>
.input-search {
  width: 50%;
}
</style>
