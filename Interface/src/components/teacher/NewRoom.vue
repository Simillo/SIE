<template lang='pug'>
  sidebar
    box(
      :hasClass='true',
      :height='"375px"',
      :noLogo='true')
</template>

<script>

import TeacherService from '../../services/TeacherService'
import Teacher from './Teacher.vue'
import Box from '../shared/Box.vue'

export default {
  components: {
    'sidebar': Teacher,
    'box': Box
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
