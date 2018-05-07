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
        .btn-new
          router-link(:to='"/teacher/room/"+room.Code+"/activity"')
            md-button.md-raised.md-primary.no-margin Criar Atividade
        md-field.margin-top-20.input-search
          label Filtrar atividade por nome ou descrição
          md-input(
            v-model='search'
          )
        .room-content-activities
          .room-content-activities-item(
            v-for='(activity, index) in room.Activities',
            :key='index'
          )
            pre {{activity}}
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
      room: {},
      search: ''
    }
  },
  async created () {
    this.service = new TeacherService(this.$http)
    this.loadData()
  },
  methods: {
    async loadData () {
      try {
        const res = await this.service.loadRoom(this.$route.params.roomCode)
        this.room = res.body.entity
      } catch (ex) {
        this.$router.push('/teacher/my-rooms')
      }
    }
  }
}
</script>

<style lang='scss' scoped>
.input-search {
  width: 50%;
}
</style>
