<template lang='pug'>
  sidebar
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
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    await this.service.load()
  }
}
</script>

<style lang='scss' scoped>

</style>
