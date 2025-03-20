import http from '@/../server/core/services/http.common'
import type AuthModel from '@/types/models/auth/auth.model'
import type RegisterModel from '@/types/models/auth/register.model'

class AuthService {
  private readonly path = 'authentication'

  login(request: AuthModel) {
    return http.post(`${this.path}/login`, request)
  }

  register(request: RegisterModel) {
    return http.post(`${this.path}/register`, request)
  }
}

export default new AuthService()
