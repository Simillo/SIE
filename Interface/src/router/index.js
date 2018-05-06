import Vue from 'vue'
import Router from 'vue-router'
import Login from '@/components/login/Login'
import Register from '@/components/register/Register'
import Teacher from '@/components/teacher/Teacher'
import NewRoom from '@/components/teacher/NewRoom'
import MyRooms from '@/components/teacher/MyRooms'
import Room from '@/components/teacher/Room'
import NewActivity from '@/components/teacher/NewActivity'

import EProfile from '../enums/EProfile'

Vue.use(Router)

export default new Router({
  routes: [
    {
      sidebar: EProfile.NotShown,
      path: '/',
      name: 'Login',
      component: Login
    },
    {
      sidebar: EProfile.NotShown,
      path: '/register',
      name: 'Register',
      component: Register
    },
    {
      sidebar: EProfile.NotShown,
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
    },
    {
      sidebar: EProfile.NotShown,
      path: '/teacher/room/:roomCode',
      name: 'Sala',
      component: Room
    },
    {
      sidebar: EProfile.NotShown,
      path: '/teacher/room/:roomCode/activity',
      name: 'Nova atividade',
      component: NewActivity
    },
    {
      sidebar: EProfile.NotShown,
      path: '/teacher/room/:roomCode/activity/:activityId',
      name: 'Nova atividade',
      component: NewActivity
    }
  ]
})
