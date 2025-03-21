import { useAuthStore } from "@/stores/auth.store";
import type { AxiosInstance } from "axios";

export function setAuthInterceptor(axiosInstance: AxiosInstance) {
  return axiosInstance.interceptors.request.use((config) => {
    console.log("Interceptor running...")
    const auth = useAuthStore()
    const token = auth.token
    if (token) {
      config.headers.Authorization = `Bearer ${token}`
    }
    return config
  })
}
