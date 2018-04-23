// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import VueMaterial from 'vue-material'
import VueResource from 'vue-resource'
import VueTheMask from 'vue-the-mask'
import NProgress from 'nprogress'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
import 'nprogress/nprogress.css'
import router from './router'
import bus from './bus'

Vue.config.productionTip = false

Vue.use(VueMaterial)
Vue.use(VueResource)
Vue.use(VueTheMask)

Vue.material.locale = {
  startYear: 1910,
  endYear: 2018,
  dateFormat: 'DD/MMM/YYYY',
  days: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sabádo'],
  shortDays: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'],
  shorterDays: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S'],
  months: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
  shortMonths: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Aug', 'Set', 'Out', 'Nov', 'Dez'],
  shorterMonths: ['J', 'F', 'M', 'A', 'M', 'Ju', 'Ju', 'A', 'Se', 'O', 'N', 'D'],
  firstDayOfAWeek: 0
}

Vue.http.interceptors.push((request, next) => {
  request.credentials = 'same-origin'
  bus.$emit('start-spinner')
  NProgress.start()
  next(response => {
    bus.$emit('end-spinner')
    NProgress.done()
    if (response.status !== 200) {
      bus.$emit('popup', response)
    }
    if (response.status === 401) {
      router.push('/')
    }
  })
})

Vue.http.options.root = 'http://localhost/SIE/api'

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
