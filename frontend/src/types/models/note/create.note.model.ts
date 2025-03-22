export default interface CreateNoteModel {
  userId: number,
  title: string
  content: string
  isArchived: boolean
  noteCategoriesId: number[]
}
