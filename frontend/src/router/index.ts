import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/home/home-view.vue'
import LoginView from '@/views/login/login-view.vue'
import NotesView from '@/views/notes/notes-view.vue'
import RegisterView from '@/views/register-view/register-view.vue'
import { authGuard } from '@/guards/auth.guard'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home-view',
      component: HomeView,
      meta: {
        hideNavbar: false,
        requiresAuth: true,
      },
    },
    {
      path: '/login',
      name: 'login-view',
      component: LoginView,
      meta: {
        hideNavbar: true,
        requiresAuth: false,
      },
    },
    {
      path: '/notes',
      name: 'notes-view',
      component: NotesView,
      meta: {
        hideNavbar: false,
        requiresAuth: true,
      },
    },
    {
      path: '/register',
      name: 'register-view',
      component: RegisterView,
      meta: {
        hideNavbar: true,
        requiresAuth: false,
      },
    },
  ],
})

// You add here the function
router.beforeEach(authGuard)

export default router
