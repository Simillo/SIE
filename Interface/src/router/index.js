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
import MyRoomsTeacher from '@/components/teacher/MyRooms'
import RoomTeacher from '@/components/teacher/Room'
import ActivityTeacher from '@/components/teacher/Activity'

// STUDENT
import Student from '@/components/student/Student'
import Rooms from '@/components/student/Rooms'
import MyRoomsStudent from '@/components/student/MyRooms'
import RoomStudent from '@/components/student/Room'
import ActivityStudent from '@/components/student/Activity'

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
      title: 'Minhas salas',
      component: MyRoomsTeacher
    },
    {
      sidebar: EProfile.Teacher,
      path: '/teacher/new-room',
      name: 'Criar nova sala',
      title: 'Criar nova sala',
      icon: 'book',
      component: NewRoom
    },
    {
      sidebar: EProfile.NotShown,
      path: '/teacher/room/:roomCode',
      name: 'Sala',
      component: RoomTeacher
    },
    {
      sidebar: EProfile.NotShown,
      path: '/teacher/room/:roomCode/activity',
      name: 'Nova atividade',
      component: ActivityTeacher
    },
    {
      sidebar: EProfile.NotShown,
      path: '/teacher/room/:roomCode/activity/:activityId',
      name: 'Editar atividade',
      component: ActivityTeacher
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
      title: 'Salas diponíveis',
      component: Rooms
    },
    {
      sidebar: EProfile.Student,
      path: '/student/my-rooms',
      name: 'Minhas salas estudante',
      icon: 'list',
      title: 'Minhas salas',
      component: MyRoomsStudent
    },
    {
      sidebar: EProfile.NotShown,
      path: '/student/room/:roomCode',
      name: 'Sala estudante',
      component: RoomStudent
    },
    {
      sidebar: EProfile.NotShown,
      path: '/student/room/:roomCode/activity/:activityId',
      name: 'Atividade estudante',
      component: ActivityStudent
    }
  ]
})
