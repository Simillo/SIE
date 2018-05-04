<template lang='pug'>
  .page-container
    md-app
      md-app-drawer(md-permanent='full')
        md-toolbar.md-transparent(md-elevation='0')
          .logo
        md-list
          md-list-item.sidebar-item
            .sidebar-icon
              md-icon.md-size-2x book
            span.md-list-item-text Criar nova sala
      md-app-content
  </div>
</template>

<script>

import TeacherService from '../../services/TeacherService'

export default {
  data () {
    return {
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    await this.service.load()
  }
}
</script>

<style lang='scss' scoped>
.sidebar-item {
  margin-top: 20px;
  span {
    font-weight: bold;
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
</style>
