<template lang='pug'>
  .box
    .box-middle
      .box-inner.md-elevation-1
        .box-logo(v-if='!title')
        .box-title(v-if='title')
          span {{title}}
        .content-box(
          :class='propClass',
          :style='{"min-height": height}')
            slot
</template>

<script>
export default {
  props: ['hasClass', 'height', 'titleProp'],
  data () {
    return {
      propClass: this.hasClass ? 'margin-top' : '',
      title: this.titleProp
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
        background-size: 145px 123px;
        width: 145px;
        height: 123px;
      }
      .box-title {
        text-align: center;
        span {
          font-size: 23px;
        }
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
