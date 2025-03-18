import http from '@/../server/core/services/http.common'
import type CreateNoteModel from '@/types/models/note/create.note.model'
import type UpdateNoteModel from '@/types/models/note/update.note.model'

class NoteService {
  private readonly path = 'notes'

  getAllNotes() {
    return http.get(`${this.path}`)
  }

  getNoteById(noteId: number) {
    return http.get(`${this.path}/${noteId}`)
  }

  getNotesByIsArchiveStatus(isArchive: boolean) {
    console.log(`${this.path}/status?is_archive=${isArchive}`)
    return http.get(`${this.path}/status?is_archive=${isArchive}`)
  }

  createNote(createNote: CreateNoteModel) {
    return http.post(`${this.path}`, createNote)
  }

  updateNote(noteId: number, updateNote: UpdateNoteModel) {
    return http.put(`${this.path}/${noteId}`, updateNote)
  }

  setArchiveStatusFromNote(noteId: number, isArchive: boolean) {
    return http.patch(`${this.path}/archive/${noteId}`, isArchive)
  }

  deleteNote(noteId: number) {
    return http.delete(`${this.path}/${noteId}`)
  }

  getNotesByCategoryId(categoryId: number, isArchived: boolean) {
    return http.get(`${this.path}/category/${categoryId}?is_archived=${isArchived}`)
  }
}

export default new NoteService()
