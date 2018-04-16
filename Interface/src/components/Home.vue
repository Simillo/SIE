<template lang='pug'>
  .box
    .box-middle
      .box-inner
        .box-logo
        .content-box
          md-field
            label Email ou CPF
            md-input(
              v-model='emailOrCpf'
              required)
</template>

<script>
export default {
  name: 'Home',
  data () {
    return {
      emailOrCpf: ''
    }
  }
}
</script>

<style scoped>
</style>
