<template lang='pug'>
  #app
    md-snackbar(
      :md-position='position',
      :md-duration='duration',
      :md-active.sync='showPopup',
      md-persistent
    )
      span {{msg}}
    router-view
</template>

<script>
import bus from './bus'

export default {
  name: 'App',
  data () {
    return {
      showPopup: false,
      position: 'center',
      duration: 4000,
      isInfinity: false,
      msg: ''
    }
  },
  created () {
    bus.$on('popup', this.togglePopup)
  },
  methods: {
    togglePopup (msg) {
      this.msg = msg
      this.showPopup = true
    }
  }
}
</script>

<style lang='scss'>
@import url('https://fonts.googleapis.com/css?family=Open+Sans');
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
  font-family: 'Open Sans', 'Roboto', sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
  height: 100%;
}
.md-app-content {
  padding: 0!important;
}
.md-app-container {
  overflow-x: hidden;
}
.margin-top-20 {
  margin-top: 20px!important;
}
.no-margin {
  margin: 0 !important;
}
.float-left {
  float: left!important;
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
.hide {
  display: none;
}

.spinner {
  position: absolute;
  left: 50%;
  top: 50%;
  z-index: 999;
}

.margin-left-20 {
  margin-left: 20px;
}

.md-toolbar {
  a {
    width: 100%;
  }
}
.logo {
  margin: 20px auto 0px;
  background: url('assets/logo.png') no-repeat;
  background-size: 145px 123px;
  width: 145px;
  height: 123px;
}
.room-container {
  padding: 40px;
  @media(max-width: 996px) {
  }
  .room-head {
    width: 50%;
    margin: 0 auto;
    min-height: 20px;
    font-weight: bold;
    @media (max-width: 1600px) {
      width: 100%;
    }
    .room-head-name {
      .room-name {
        font-size: 25px;
      }
      .room-code {
        font-size: 17px;
      }
    }
    .room-head-description {
      margin-top: 10px;
      font-size: 17px;
    }
  }
  .room-content {
    margin: 40px auto;
    width: 50%;
    @media (max-width: 1600px) {
      width: 100%;
    }
  }
}
.msg-feedback {
  margin-top: 100px;
  text-align: center;
}
</style>
