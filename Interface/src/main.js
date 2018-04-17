// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import VueMaterial from 'vue-material'
import VueResource from 'vue-resource'
import 'vue-material/dist/vue-material.min.css'
import 'vue-material/dist/theme/default.css'
import router from './router'

Vue.config.productionTip = false

Vue.use(VueMaterial)
Vue.use(VueResource)

Vue.http.options.root = 'http://localhost/SIE/api'

Vue.directive('mask', {
  bind (el, binding, vNode) {
    console.log(el)
  }
})

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})
