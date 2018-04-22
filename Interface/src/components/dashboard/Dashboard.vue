<template lang='pug'>
  main
    div teste
</template>

<script>

import DashboardService from '../../services/DashboardService'

export default {
  data () {
    return {
    }
  },
  async created () {
    this.service = new DashboardService(this.$resource, this.$http)
    const res = this.service.load()
    console.log(res)
  }
}
</script>

<style scoped>
</style>
