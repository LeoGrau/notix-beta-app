export default interface CreateNoteModel {
  title: string
  content: string
  isArchived: boolean
  noteCategoriesId: number[]
}
