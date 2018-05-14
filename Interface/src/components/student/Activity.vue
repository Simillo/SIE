<template lang='pug'>
  sidebar
    .room-container
      .room-head
        .room-head-name
          span.room-name {{activity.Name}}
          span.room-code  {{activity.Code}}
        .room-head-description(v-if='activity.Description')
          span {{activity.Description}}
          span peso: {{activity.Weight}}
      .room-content
        form(
          novalidate,
          @submit.prevent='validate'
        )
          md-field
            label(for='answer') Resposta
            md-textarea#answer(
              name='answer',
              v-model='form.Answer'
            )
            //- span.md-error(v-if='!$v.form.Answer.required') Obrigatório!
          div
            router-link(:to='"/student/room/"+$route.params.roomCode')
              md-button.md-raised.md-primary.no-margin.float-left Voltar
            md-button.md-raised.md-primary.no-margin.float-right(@click.prevent='validate') Salvar Resposta
</template>

<script>
import StudentService from '../../services/StudentService'
import Student from './Student.vue'

export default {
  components: {
    'sidebar': Student
  },
  data () {
    return {
      activity: {},
      form: {
        Answer: ''
      }
    }
  },
  async created () {
    this.service = new StudentService(this.$http)
    if (this.$route.params.activityId) this.loadActivity()
  },
  methods: {
    loadActivity () {
      const params = this.$route.params
      this.service.loadActivity(params.roomCode, params.activityId)
        .then(res => {
          this.activity = res.body.entity
        })
        .catch(() => {
          this.$router.push('/student/my-rooms')
        })
    }
    // },
    // async save () {
    //   const params = this.$route.params
    //   this.form.Id = params.activityId || 0
    //   await this.service.saveOrUpdateActivity(this.form, params.roomCode)
    //     .then(() => this.$router.push(`/student/room/${this.$route.params.roomCode}`))
    //     .catch(err => {
    //       if (err.body.status === 401) this.$router.push('/student/my-rooms')
    //     })
  }
}
</script>

<style lang='scss' scoped>
span {
  display: block;
}
</style>
