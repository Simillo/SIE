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
            v-model='search',
            @keyup.prevent='searchFor'
          )
        .room-content-activities
          .room-content-activities-item(
            v-for='(activity, index) in searched',
            :key='index'
          )
            .activities-head
              span.activities-name {{activity.Name}}
              span.activities-state {{getCurrentStateTitle(activity.CurrentState)}}
            br
            .activities-sub-head
              span.activities-sub-title {{getCurrentStateSubtitle(activity.CurrentState, activity.ExpirationDate)}}
            br
            .activities-description-container
              span.activities-description {{activity.Description}}
            br
            .activities-actions
              .activities-actions-item(
                v-for='(action, index) in getActions(activity.CurrentState)',
                :key='index'
              )
                router-link(:to='action.To')
                  md-tooltip(md-direction='top') {{action.Tooltip}}
                  md-icon.md-size {{action.Icon}}
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
      search: '',
      searched: []
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
        this.searched = this.room.Activities
      } catch (ex) {
        this.$router.push('/teacher/my-rooms')
      }
    },
    getCurrentStateTitle (state) {
      switch (state) {
        case 1:
          return 'Atividade em construção'
        case 2:
          return 'Atividade em andamento'
        case 3:
          return 'Atividade finalizada'
      }
    },
    getCurrentStateSubtitle (state, expirationDate) {
      switch (state) {
        case 1:
          return ''
        case 2:
          if (expirationDate === null) return ''

          return `Entregar até ${this.getFormatedDate(expirationDate)}`
        case 3:
          return `Atividade finalizada em ${this.getFormatedDate(expirationDate)}`
      }
    },
    searchFor () {
      const query = this.search
      if (!query) {
        this.searched = this.room.Activities
        return
      }

      this.searched = this.room.Activities.filter(a => a.Name.includes(query) || (a.Description && a.Description.includes(query)))
    },
    getActions (state) {
      const visualize = {
        Tooltip: 'Visualizar',
        To: '/teacher/1',
        Icon: 'remove_red_eye'
      }
      const finalize = {
        Tooltip: 'Finalizar atividade',
        To: '/teacher/2',
        Icon: 'close'
      }
      const edit = {
        Tooltip: 'Editar atividade',
        To: '/teacher/3',
        Icon: 'build'
      }
      const initate = {
        Tooltip: 'Liberar atividade',
        To: '/teacher/4',
        Icon: 'play_arrow'
      }

      switch (state) {
        case 1:
          return [edit, initate]
        case 2:
          return [visualize, finalize]
        case 3:
          return [visualize]
      }
    },
    getFormatedDate (date) {
      return new Date(date).toLocaleDateString('pt-BR')
    }
  }
}
</script>

<style lang='scss' scoped>
.room-content-activities-item {
  border: 1px solid #ccc;
  margin-bottom: 20px;
  min-height: 20px;
  div {
    margin: 5px;
    display: block;
  }
  .activities-head {
    .activities-name {
      float: left;
      font-size: 20px;
      font-weight: bold;
    }
    .activities-state {
      float: right;
    }
  }
  .activities-actions-item {
    display: inline-block;
    a {
      text-decoration: none;
    }
    i {
      background: #ccc;
      border-radius: 100%;
      padding: 15px;
      color: black;
    }
  }
}
</style>
