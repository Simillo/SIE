<template lang='pug'>
  sidebar
    .room-container
      .room-head
        .room-head-name
          span.room-name {{room.Name}}
          span.room-code  {{room.Code}}
        .room-head-description(v-if='room.Description')
          span {{room.Description}}
      .room-content
        md-button.md-raised.md-primary.no-margin Criar Atividade

</template>

<script>

import TeacherService from '../../services/TeacherService'
import Teacher from './Teacher.vue'

export default {
  components: {
    'sidebar': Teacher
  },
  data () {
    return {
      room: {}
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    try {
      const res = await this.service.loadRoom(this.$route.params.roomCode)
      this.room = res.body.entity
    } catch (ex) {
      this.$router.push('/teacher/my-rooms')
    }
  }
}
</script>

<style lang='scss' scoped>
.room-container {
  padding: 40px 0 0 40px;
  .room-head {
    width: 400px;
    min-height: 20px;
    font-weight: bold;
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
    margin-top: 40px;
  }
}
</style>
