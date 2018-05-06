import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/login/Login'
import Register from '@/components/register/Register'
import Teacher from '@/components/teacher/Teacher'
import NewRoom from '@/components/teacher/NewRoom'
import MyRooms from '@/components/teacher/MyRooms'

import EProfile from '../enums/EProfile'

Vue.use(Router)

export default new Router({
  routes: [
    {
      sidebar: EProfile.Annon,
      path: '/',
      name: 'Login',
      component: Login
    },
    {
      sidebar: EProfile.Annon,
      path: '/register',
      name: 'Register',
      component: Register
    },
    {
      sidebar: EProfile.Annon,
      path: '/teacher',
      name: 'Teacher',
      component: Teacher
    },
    {
      sidebar: EProfile.Teacher,
      path: '/teacher/my-rooms',
      name: 'Minhas salas',
      icon: 'list',
      component: MyRooms
    },
    {
      sidebar: EProfile.Teacher,
      path: '/teacher/new-room',
      name: 'Criar nova sala',
      icon: 'book',
      component: NewRoom
    }
  ]
})
