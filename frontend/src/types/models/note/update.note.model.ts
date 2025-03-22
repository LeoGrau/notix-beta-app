export default interface UpdateNoteModel {
  userId: number,
  title: string
  content: string
  isArchived: boolean
  noteCategoriesId: number[]
}
