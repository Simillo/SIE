<template lang='pug'>
  md-app
    md-app-toolbar
      md-button.md-icon-button(
        @click='toggleSidebar',
        v-if='!menuVisible')
        md-icon menu
      span.md-title Seja bem-vindo(a), {{person.Name}}
    md-app-drawer.sidebar(
      :md-active.sync='menuVisible'
    )
      md-toolbar.md-transparent(md-elevation='0')
        .md-toolbar-section-end
          md-button.md-icon-button.md-dense(@click='toggleSidebar')
            md-icon.md-size-2x keyboard_arrow_left
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
      menuVisible: false,
      menus: router.options.routes.filter(m => m.sidebar === EProfile.Teacher),
      person: {}
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    const res = await this.service.load()
    this.person = res.data.entity
  },
  methods: {
    goTo (path) {
      this.$router.push(path)
    },
    toggleSidebar () {
      this.menuVisible = !this.menuVisible
      localStorage.sidebarState = this.menuVisible
    }
  }
}
</script>

<style lang='scss' scoped>
.md-app {
  height: 100%;
}
.md-toolbar-section-end {
  position: absolute;
  top: 20px;
  left: 250px;
  .md-icon {
    color:#0E479E!important;
  }
}
.md-drawer {
  width: 300px;
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
