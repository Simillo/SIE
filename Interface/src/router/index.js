import Vue from 'vue'
import Router from 'vue-router'

// MISC
import EProfile from '../enums/EProfile'

// EXTERN
import Login from '@/components/login/Login'
import Register from '@/components/register/Register'

// TEACHER
import Teacher from '@/components/teacher/Teacher'
import NewRoom from '@/components/teacher/NewRoom'
import MyRooms from '@/components/teacher/MyRooms'
import Room from '@/components/teacher/Room'
import Activity from '@/components/teacher/Activity'

// STUDENT
import Student from '@/components/student/Student'
import Rooms from '@/components/student/Rooms'

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
      name: 'Professor',
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
      component: Activity
    },
    {
      sidebar: EProfile.NotShown,
      path: '/teacher/room/:roomCode/activity/:activityId',
      name: 'Editar atividade',
      component: Activity
    },
    {
      sidebar: EProfile.NotShown,
      path: '/student',
      name: 'Estudante',
      component: Student
    },
    {
      sidebar: EProfile.Student,
      path: '/student/rooms',
      name: 'Salas disponíveis',
      icon: 'book',
      component: Rooms
    }
  ]
})
