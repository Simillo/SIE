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

html, body {
  padding: 0;
  margin: 0;
  height: 100%;
}
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

.box {
  display: table;
  position: absolute;
  height: 100%;
  width: 100%;
  .middle-box {
    display: table-cell;
    vertical-align: middle;
    .inner-box {
      border: 1px solid #000;
      width: 400px;
      min-height: 500px;
      margin-left: auto;
      margin-right: auto;
    }
  }
}
</style>
