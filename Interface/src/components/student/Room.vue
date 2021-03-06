<template lang='pug'>
  sidebar
    md-dialog-confirm(
      :md-active.sync='active',
      md-title='Atenção',
      :md-content='current.message',
      md-confirm-text='Continuar',
      md-cancel-text='Cancelar',
      @md-confirm='current.fn()'
      )
    .room-container
      .room-head
        .room-head-name
          span.room-name {{room.Name}}
          span.room-code  {{room.Code}}
        .room-head-description(v-if='room.Description')
          span {{room.Description}}
      .room-content
        .btn-new
          md-speed-dial(
            md-event='click',
            md-direction='bottom'
          )
            md-speed-dial-target.md-primary
              md-icon build
              md-tooltip.margin-tooltip(md-direction='left') Ações da sala
            md-speed-dial-content
              md-button.md-icon-button.md-plain.md-accent(@click.prevent='exitRoom')
                md-tooltip.margin-tooltip(md-direction='left') Sair da sala
                md-icon close
        md-field.margin-top-20.input-search
          label Filtrar atividade por nome ou descrição
          md-input(
            v-model='search',
            @keyup.prevent='searchFor'
          )
        .room-content-activities(v-if='searched.length')
          .room-content-activities-item(
            v-for='(activity, index) in searched',
            :key='index'
          )
            .activities-head
              span.activities-name {{activity.Name}}
              span.activity-float-right {{getCurrentStateTitle(activity.CurrentState)}}
            br
            .activities-sub-head
              span.activities-sub-title {{getCurrentStateSubtitle(activity)}}
              span.activity-float-right {{getGrade(activity)}}
            br
            .activities-description-container
              span.activities-description {{activity.Description}}
            br
            .activities-actions
              .activities-actions-item(
                v-for='(action, index) in getActions(activity)',
                :key='index'
              )
                a(
                  v-if='!!action.Dialog',
                  @click.prevent='toggle(action.Dialog, activity.Id)',
                )
                  md-tooltip(md-direction='top') {{action.Tooltip}}
                  md-icon.md-size {{action.Icon}}
                router-link(
                  v-if='!!action.To',
                  :to='action.To'
                )
                  md-tooltip(md-direction='top') {{action.Tooltip}}
                  md-icon.md-size {{action.Icon}}
                span.margin-left-20(v-if='activity.Answer.Feedback') Feedback do professor:
                  b &nbsp;{{activity.Answer.Feedback}}
        .room-content-activities(v-if='!searched.length && room.Activities && room.Activities.length')
          p Nenhuma atividade foi encontrada com o filtro informado!
        .room-content-activities(v-else-if='!room.Activities || !room.Activities.length')
          p Nenhuma atividade foi adicionada ainda. Aguarde o professor da sala adicionar!
</template>

<script>
import Student from './Student.vue'
import ERoomState from '../../enums/ERoomState'
import EActivityState from '../../enums/EActivityState'

import StudentSevice from '../../services/StudentService'

export default {
  components: {
    'sidebar': Student
  },
  data () {
    return {
      room: {},
      search: '',
      searched: [],
      active: false,
      ERoomState,
      current: {
        fn: Function,
        message: '',
        title: '',
        id: 0
      }
    }
  },
  async created () {
    this.service = new StudentSevice(this.$http)
    this.loadRoom()
  },
  methods: {
    loadRoom () {
      this.search = ''
      this.searched = []
      this.service.loadRoom(this.$route.params.roomCode)
        .then(res => {
          this.room = res.body.entity
          this.searched = this.room.Activities
        })
        .catch(() => this.$router.push('/student/my-rooms'))
    },
    getCurrentStateTitle (state) {
      switch (state) {
        case EActivityState.InProgress.ordinal:
          return 'Atividade em andamento'
        case EActivityState.Done.ordinal:
          return 'Atividade finalizada'
      }
    },
    getCurrentStateSubtitle (activity) {
      const state = activity.CurrentState
      const expirationDate = activity.ExpirationDate
      const endDate = activity.EndDate

      switch (state) {
        case EActivityState.InProgress.ordinal:
          if (expirationDate === null) return 'Sem data final para entrega'

          return `Entregar até ${this.getFormatedDate(expirationDate)}`
        case EActivityState.Done.ordinal:
          return `Atividade finalizada em ${this.getFormatedDate(endDate)}`
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
    getFormatedDate (date) {
      return new Date(date).toLocaleDateString('pt-BR')
    },
    getActions (activity) {
      const alreadyAnswered = !!activity.Answer.Answer
      const answer = {
        Tooltip: 'Responder',
        To: `/student/room/${this.room.Code}/activity/${activity.Id}`,
        Icon: 'play_arrow'
      }

      const visualize = {
        Tooltip: 'Visualizar',
        To: `/student/room/${this.room.Code}/activity/${activity.Id}`,
        Icon: 'remove_red_eye'
      }

      switch (activity.CurrentState) {
        case EActivityState.InProgress.ordinal:
          if (alreadyAnswered) return [visualize]
          return [answer]
        case EActivityState.Done.ordinal:
          return [visualize]
      }
    },
    getGrade (activity) {
      const grade = activity.Answer.Grade
      const weight = activity.Weight
      if (!grade) {
        if (activity.CurrentState === EActivityState.Done.ordinal) {
          if (!activity.Answer.Answer) return 'Atividade finalizada sem você ter respondido'
          else return 'Atividade foi finalizada sem ser avaliada'
        } else if (activity.Answer.Answer) return 'Atividade ainda não foi avaliada'
      }

      return grade ? `${(grade / weight * 100).toLocaleString('pt-BR', { maximumFractionDigits: 2 })}%` : 'Você ainda não respondeu'
    },
    exitRoom () {
      this.service.exitRoom(this.room.Code)
        .then(() => {
          this.$router.push('/student/my-rooms')
        })
    }
  }
}
</script>

<style lang='scss' scoped>
.md-sie {
  background: #41c300!important;
  i {
    color: #fff!important;
  }
}
.md-speed-dial {
  float: right;
  .md-tooltip {
    margin-left: 20px;
  }
  .md-speed-dial-content {
    position: absolute;
    margin: 55px 0px auto 9px;
  }
}
.room-content-activities-item {
  border: 1px solid #ccc;
  margin-bottom: 20px;
  min-height: 20px;
  div {
    margin: 5px;
    display: block;
  }
  .activity-float-right {
    float: right;
  }
  .activities-sub-title {
    float: left;
  }
  .activities-head {
    .activities-name {
      float: left;
      font-size: 20px;
      font-weight: bold;
    }
  }
  .activities-actions-item {
    display: inline-block;
    a {
      text-decoration: none;
      cursor: pointer;
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
