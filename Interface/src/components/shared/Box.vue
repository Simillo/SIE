<template lang='pug'>
  .box
    .box-middle
      .box-inner.md-elevation-1
        .box-logo
        .content-box(
          :class='propClass',
          :style='{"min-height": height}')
            slot
</template>

<script>
export default {
  props: ['hasClass', 'height'],
  data () {
    return {
      propClass: this.hasClass ? 'margin-top' : ''
    }
  }
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
      width: 450px;
      margin-left: auto;
      margin-right: auto;
      padding: 30px 50px 60px;
      .box-logo {
        background: url('../../assets/logo.png') no-repeat;
        width: 145px;
        height: 123px;
      }
    }
  }
  .md-field:not(:first-child) {
    margin-top: -10px!important;
  }
}
.margin-top {
  margin-top: 50px;
}
</style>
