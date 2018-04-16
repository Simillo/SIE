import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/login/Login'
import Auth from '@/components/login/Auth'
import Register from '@/components/register/Register'
import Password from '@/components/register/Password'

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
    },
    {
      path: '/register',
      name: 'Register',
      component: Register
    },
    {
      path: '/password',
      name: 'Password',
      component: Password
    }
  ]
})
