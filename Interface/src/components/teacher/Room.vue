<template lang='pug'>
  sidebar
    | room
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
      rooms: []
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
  }
}
</script>

<style lang='scss' scoped>
</style>
