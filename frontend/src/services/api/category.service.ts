import http from '@/../server/core/services/http.common'
import type CreateCategoryModel from '@/types/models/category/create.category.model'

class CategoryService {
  private readonly path = 'categories'

  getAllCategories() {
    return http.get(`${this.path}`)
  }

  createCategory(newCategory: CreateCategoryModel) {
    return http.post(`${this.path}`, newCategory)
  }
}

export default new CategoryService()
