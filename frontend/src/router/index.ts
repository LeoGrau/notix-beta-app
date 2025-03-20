import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/home/home-view.vue'
import LoginView from '@/views/login/login-view.vue'
import NotesView from '@/views/notes/notes-view.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home-view',
      component: HomeView,
      meta: {
        hideNavbar: false
      }
    },
    {
      path: '/login',
      name: 'login-view',
      component: LoginView,
      meta: {
        hideNavbar: true
      }
    },
    {
      path: '/notes',
      name: 'notes-view',
      component: NotesView,
      meta: {
        hideNavbar: false
      }
    },
  ],
})

export default router
