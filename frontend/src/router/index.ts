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
    },
    {
      path: '/login',
      name: 'login-view',
      component: LoginView,
    },
    {
      path: '/notes',
      name: 'notes-view',
      component: NotesView,
    },
  ],
})

export default router
