<template lang='pug'>
  .page-container
    md-app
      md-app-drawer(
        md-permanent='full'
      )
        md-toolbar.md-transparent(
          md-elevation='0'
        )
          | SIE Logo
        md-list
          md-list-item
            span.md-list-item-text Inbox
      md-app-content
        p Lorem ipsum dolor sit amet, consectetur adipisicing elit. Error quibusdam, non molestias et! Earum magnam, similique, quo recusandae placeat dicta asperiores modi sint ea repudiandae maxime? Quae non explicabo, neque.
        p Lorem ipsum dolor sit amet, consectetur adipisicing elit. Error quibusdam, non molestias et! Earum magnam, similique, quo recusandae placeat dicta asperiores modi sint ea repudiandae maxime? Quae non explicabo, neque.
        p Lorem ipsum dolor sit amet, consectetur adipisicing elit. Error quibusdam, non molestias et! Earum magnam, similique, quo recusandae placeat dicta asperiores modi sint ea repudiandae maxime? Quae non explicabo, neque.
        p Lorem ipsum dolor sit amet, consectetur adipisicing elit. Error quibusdam, non molestias et! Earum magnam, similique, quo recusandae placeat dicta asperiores modi sint ea repudiandae maxime? Quae non explicabo, neque.
        p Lorem ipsum dolor sit amet, consectetur adipisicing elit. Error quibusdam, non molestias et! Earum magnam, similique, quo recusandae placeat dicta asperiores modi sint ea repudiandae maxime? Quae non explicabo, neque.
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

<style scoped>
</style>
