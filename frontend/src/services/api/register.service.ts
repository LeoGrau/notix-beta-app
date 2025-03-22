import http from "@/../server/core/services/http.common"
import type RegisterModel from "@/types/models/auth/register.model"

class RegisterService {
  private readonly path = 'authentication'
  register(request: RegisterModel) {
    return http.post(`${this.path}/register`, request)
  }
}

export default new RegisterService()
