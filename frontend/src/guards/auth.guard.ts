import { useAuthStore } from '@/stores/auth.store'
import type {
  NavigationGuardNext,
  RouteLocationNormalizedGeneric,
  RouteLocationNormalizedLoadedGeneric,
} from 'vue-router'

export function authGuard(
  to: RouteLocationNormalizedGeneric,
  from: RouteLocationNormalizedLoadedGeneric,
  next: NavigationGuardNext,
) {
  const authStore = useAuthStore()
  // If is not authenticated and the page requires it so...
  if (to.meta.requiresAuth && !authStore.isAuthenticated) next({ name: 'login-view' })
  else if ((to.name == 'login-view' || to.name == 'register-view') && authStore.isAuthenticated) {
    next({ name: 'home-view' })
  } else {
    next()
  }
}
