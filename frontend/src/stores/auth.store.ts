import authService from '@/services/api/auth.service'
import type AuthModel from '@/types/models/auth/auth.model'
import axios from 'axios'
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

export const useAuthStore = defineStore('auth', () => {
  const token = ref(localStorage.getItem('token')! || null)
  const userId = ref(JSON.parse(localStorage.getItem('userId')! ?? 'null') || null)
  const isAuthenticated = computed(() => !!token.value)

  function login(request: AuthModel) {
    return authService
      .login(request)
      .then((res) => {
        token.value = res.data.token
        userId.value = res.data.userId

        // Save on local storage
        localStorage.setItem('token', token.value!)
        localStorage.setItem('userId', userId.value)

        return res.data
      })
      .catch((error) => {
        console.error('❌ Error en login:', error.response?.data || error.message)
        throw error
      })
  }

  function logout() {
    token.value = null
    userId.value = null
    localStorage.removeItem('token')
    localStorage.removeItem('userId')
    delete axios.defaults.headers.common['Authorization'];
  }
})
