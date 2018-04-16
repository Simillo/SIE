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
  background: #f6f8f9;
  background: -moz-linear-gradient(-45deg, #f6f8f9 0%, #f6f8f9 52%, #e5ebee 63%, #e5ebee 71%, #e5ebee 78%, #d7dee3 85%, #e5ebee 93%, #e5ebee 93%, #f5f7f9 100%); /* FF3.6-15 */
  background: -webkit-linear-gradient(-45deg, #f6f8f9 0%,#f6f8f9 52%,#e5ebee 63%,#e5ebee 71%,#e5ebee 78%,#d7dee3 85%,#e5ebee 93%,#e5ebee 93%,#f5f7f9 100%); /* Chrome10-25,Safari5.1-6 */
  background: linear-gradient(135deg, #f6f8f9 0%,#f6f8f9 52%,#e5ebee 63%,#e5ebee 71%,#e5ebee 78%,#d7dee3 85%,#e5ebee 93%,#e5ebee 93%,#f5f7f9 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
  filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#f6f8f9', endColorstr='#f5f7f9',GradientType=1 ); /* IE6-9 fallback on horizontal gradient */
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
  .box-middle {
    display: table-cell;
    vertical-align: middle;
    .box-inner {
      position: relative;
      -webkit-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
      -moz-box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
      box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.75);
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
.no-margin {
  margin: 0 !important;
}
.float-right {
  float: right!important;
}
.pull-bottom {
  position: absolute;
  bottom: 50px;
  &.float-right {
    right: 50px;
  }
}
</style>
