<template lang='pug'>
  box(
    :hasClass='true',
    :height='"375px"',
    :noLogo='true')
</template>

<script>

import TeacherService from '../../services/TeacherService'
import Box from '../shared/Box.vue'

export default {
  components: {
    'box': Box
  },
  data () {
    return {
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
  },
  methods: {
  }
}
</script>

<style lang='scss' scoped>

</style>
