<template lang='pug'>
  md-app
    md-app-drawer.sidebar(md-permanent='full')
      md-toolbar.md-transparent(md-elevation='0')
        router-link(to='/teacher')
          .logo
      md-list.sidebar-list
        md-list-item.sidebar-item(
          v-for='(menu, index) in menus',
          :key='index'
          @click.prevent='goTo(menu.path)'
          )
          .sidebar-icon
            md-icon.md-size-2x {{menu.icon}}
          span.md-list-item-text {{menu.name}}
    md-app-content
      slot
</template>

<script>

import TeacherService from '../../services/TeacherService'
import router from '../../router'
import EProfile from '../../enums/EProfile'

export default {
  data () {
    return {
      menus: router.options.routes.filter(m => m.sidebar === EProfile.Teacher)
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    await this.service.load()
  },
  methods: {
    goTo (path) {
      this.$router.push(path)
    }
  }
}
</script>

<style lang='scss' scoped>
.md-app {
  height: 100%;
}
.sidebar-list {
  margin-top: 21px;
  padding-bottom: 0;
  .sidebar-item {
    margin-top: -1px;
    & {
      border-bottom: 1px solid #ccc;
    }
    &:first-child {
      border-top: 1px solid #ccc;
    }
    &:hover {
      background: #eee;
      cursor: pointer;
    }
    .sidebar-icon {
      border-radius: 100%;
      padding: 5px;
      background-color: #ddd;
      margin-right: 10px;
      > i {
        color: #333;
      }
    }
  }
}
</style>
