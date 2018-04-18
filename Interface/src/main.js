// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import VueMaterial from 'vue-material'
import VueResource from 'vue-resource'
import VueTheMask from 'vue-the-mask'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
import router from './router'

Vue.config.productionTip = false

Vue.use(VueMaterial)
Vue.use(VueResource)
Vue.use(VueTheMask)

Vue.http.options.root = 'http://localhost/SIE/api'

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
