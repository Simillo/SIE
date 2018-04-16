import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/Login'
import Auth from '@/components/Auth'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Login',
      component: Login
    },
    {
      path: '/auth',
      name: 'Auth',
      component: Auth
    }
  ]
})
