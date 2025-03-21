import { authGuard } from '@/guards/auth.guard'
import { setAuthInterceptor } from '@/interceptors/auth.interceptor'
import axios from 'axios'

const baseUrl = import.meta.env.VITE_API_URL

console.log(import.meta.env.MODE)

const axiosApi = axios.create({
  baseURL: baseUrl,
  headers: {
    'Content-Type': 'application/json',
  },
})

setAuthInterceptor(axiosApi)

export default axiosApi;
