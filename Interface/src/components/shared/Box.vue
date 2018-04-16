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
      padding: 50px;
      .box-logo {
        background: url('http://via.placeholder.com/100x50');
        width: 100px;
        height: 50px;
      }
    }
  }
  .md-field:not(:first-child) {
    margin-top: -18px!important;
  }
}
.margin-top {
  margin-top: 50px;
}
</style>
