<template lang='pug'>
  md-app
    md-app-drawer.sidebar(md-permanent='full')
      md-toolbar.md-transparent(md-elevation='0')
        .logo
      md-list.sidebar-list
        md-list-item.sidebar-item(@click.prevent='changeState("my-rooms")')
          .sidebar-icon
            md-icon.md-size-2x list
          span.md-list-item-text Minhas salas
        md-list-item.sidebar-item(@click.prevent='changeState("new-room")')
          .sidebar-icon
            md-icon.md-size-2x book
          span.md-list-item-text Criar nova sala
    md-app-content(v-if='currentState === "my-rooms"')
      new-room
    md-app-content(v-else-if='currentState === "new-room"')
      .asd teste
  </div>
</template>

<script>

import TeacherService from '../../services/TeacherService'
import NewRoom from '../new-room/NewRoom.vue'

export default {
  components: {
    'new-room': NewRoom
  },
  data () {
    return {
      currentState: ''
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    await this.service.load()
  },
  methods: {
    changeState (newState) {
      this.currentState = newState
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
    &:first-child {
      border-top: 1px solid #ccc;
    }
    &:not(:last-child) {
      border-bottom: 1px solid #ccc;
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
