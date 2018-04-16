<template lang='pug'>
  .box
    .box-middle
      .box-inner.md-elevation-1
        .box-logo
        .content-box
            slot
</template>

<script>
export default {
}
</script>

<style scoped lang='scss'>
.box {
  display: table;
  position: absolute;
  height: 100%;
  width: 100%;
  .box-middle {
    display: table-cell;
    vertical-align: middle;
    .box-inner {
      position: relative;
      background: #fff;
      border: 1px solid #ccc;
      width: 350px;
      min-height: 450px;
      margin-left: auto;
      margin-right: auto;
      padding: 50px;
      .box-logo {
        background: url('http://via.placeholder.com/100x50');
        width: 100px;
        height: 50px;
      }
      .content-box {
        margin-top: 10px;
      }
    }
  }
}
</style>
