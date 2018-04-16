<template lang='pug'>
  .app
    router-view
</template>

<script>
export default {
  name: 'App'
}
</script>

<style lang='scss'>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

.box {
  width: 400px;
  min-height: 400px;
  border: 1px solid;
  margin: 60px auto;
}
</style>
