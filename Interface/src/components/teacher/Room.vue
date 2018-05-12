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
        .btn-new(v-if='room.CurrentState !== ERoomState.Closed.ordinal')
          router-link(:to='"/teacher/room/"+room.Code+"/activity"')
            md-button.md-raised.md-primary.no-margin Criar Atividade
          md-speed-dial(
            md-event='click',
            md-direction='bottom'
          )
            md-speed-dial-target.md-primary
              md-icon build
              md-tooltip.margin-tooltip(md-direction='left') Ações da sala
            md-speed-dial-content
              md-button.md-icon-button.md-plain.md-sie(
                @click.prevent='openRoom',
                v-if='room.CurrentState !== ERoomState.Open.ordinal'
              )
                md-tooltip.margin-tooltip(md-direction='left') Abrir a sala
                md-icon lock_open
              md-button.md-icon-button.md-plain.md-accent(@click.prevent='closeRoom')
                md-tooltip.margin-tooltip(md-direction='left') Encerrar a sala
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
              span.activities-state {{getCurrentStateTitle(activity.CurrentState)}}
            br
            .activities-sub-head
              span.activities-sub-title {{getCurrentStateSubtitle(activity)}}
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
        .room-content-activities(v-if='!searched.length && room.Activities && room.Activities.length')
          p Nenhuma atividade foi encontrada com o filtro informado!
        .room-content-activities(v-else-if='!room.Activities || !room.Activities.length')
          p Nenhuma atividade foi adicionada ainda. Clique em "CRIAR ATIVIDADE" para criar uma!
</template>

<script>

import TeacherService from '../../services/TeacherService'
import Teacher from './Teacher.vue'
import ERoomState from '../../enums/ERoomState'

export default {
  components: {
    'sidebar': Teacher
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
    this.service = new TeacherService(this.$http)
    this.loadData()
  },
  methods: {
    async loadData () {
      this.search = ''
      this.searched = []
      this.service.loadRoom(this.$route.params.roomCode)
        .then(res => {
          this.room = res.body.entity
          this.searched = this.room.Activities
        })
        .catch(() => this.$router.push('/teacher/my-rooms'))
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
    getCurrentStateSubtitle (activity) {
      const state = activity.CurretnState
      const expirationDate = activity.ExpirationDate
      const endDate = activity.EndDate

      switch (state) {
        case 1:
          return ''
        case 2:
          if (expirationDate === null) return ''

          return `Entregar até ${this.getFormatedDate(expirationDate)}`
        case 3:
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
    getActions (activity) {
      const visualize = {
        Tooltip: 'Visualizar',
        To: `/teacher/room/${this.room.Code}/activity/${activity.Id}`,
        Icon: 'remove_red_eye'
      }
      const finalize = {
        Tooltip: 'Finalizar atividade',
        Icon: 'close',
        Dialog: {
          fn: this.finalize,
          message: 'Deseja realmente encerrar a atividade?<br/>Após o encerramento de uma atividade, não será possível <b>receber</b> ou <b>avaliar</b> mais respostas.'
        }
      }
      const edit = {
        Tooltip: 'Editar atividade',
        To: `/teacher/room/${this.room.Code}/activity/${activity.Id}`,
        Icon: 'build'
      }
      const initate = {
        Tooltip: 'Liberar atividade',
        Icon: 'play_arrow',
        Dialog: {
          fn: this.initiate,
          message: 'Deseja realmente liberar a atividade?<br/>Durante o período de entregas da atividade não será possível <b>editar</b> a atividade.'
        }
      }

      switch (activity.CurrentState) {
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
    },
    activeModal () {
      this.active = true
    },
    initiate () {
      this.service.initiateActivity(this.current.id)
        .then(() => this.loadData())
    },
    finalize () {
      this.service.finalizeActivity(this.current.id)
        .then(() => this.loadData())
    },
    toggle (context, activityId) {
      this.current = context
      this.current.id = activityId
      this.active = true
    },
    openRoom () {
      this.current = {
        fn: this.$_openRoom,
        message: 'Deseja realmente abrir essa sala?<br/>Após aberto uma sala, a mesma estará presente na lista de salas disponíveis e qualquer pessoa pode solicitar acesso.'
      }
      this.active = true
    },
    closeRoom () {
      this.current = {
        fn: this.$_closeRoom,
        message: 'Deseja realmente fechar essa sala?<br/>Após o fechamento de uma sala, ela não poderá ser aberta novamente, nenhum aluno terá acesso e não será possível <b>criar</b> ou <b>avaliar</b> atividades.'
      }
      this.active = true
    },
    $_openRoom () {
      this.service.openRoom(this.room.Code)
        .then(() => this.loadData())
    },
    $_closeRoom () {
      this.service.closeRoom(this.room.Code)
        .then(() => this.loadData())
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
