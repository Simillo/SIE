<template lang='pug'>
  main
    div teste
</template>

<script>

import PersonService from '../../services/PersonService'

export default {
  data () {
    return {
    }
  },
  async created () {
    this.service = new PersonService(this.$resource)
  }
}
</script>

<style scoped>
</style>
