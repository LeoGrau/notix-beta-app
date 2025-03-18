<template>
  <div
    class="p-4 border rounded-md border-surface-400 flex flex-col justify-between min-h-[150px] bg-surface-100"
  >
    <div class="flex justify-between">
      <h1 class="text-xl">{{ props.note.title }}</h1>
      <pv-button
        :icon="props.note.isArchived ? 'pi pi-check' : 'pi pi-folder-plus'"
        rounded
        size="small"
        severity="info"
        @click="openConfirmIsArchiveDialog(note)"
      ></pv-button>
    </div>
    <div class="flex gap-2 justify-end">
      <pv-button
        @click="openEditNoteDialog(note)"
        icon="pi pi-pencil"
        label="Edit"
        severity="help"
        rounded
      ></pv-button>
      <pv-button
        @click="openConfirmDeleteDialog(note)"
        icon="pi pi-trash"
        label="Delete"
        severity="danger"
        rounded
      ></pv-button>
    </div>
  </div>
</template>
<script setup lang="tsx">
import type { Note } from '@/types/note.type'
import { useConfirm, useDialog, useToast } from 'primevue'
import EditNoteDialog from './dialog/edit-note-dialog.vue'
import noteService from '@/services/api/note.service'
import type UpdateNoteModel from '@/types/models/note/update.note.model'

const dialog = useDialog()
const confirm = useConfirm()

interface NotixNoteProps {
  note: Note
}

const props = defineProps<NotixNoteProps>()
const emits = defineEmits(['updated-note'])

const toast = useToast()

function openEditNoteDialog(noteData: Note) {
  dialog.open(EditNoteDialog, {
    props: {
      modal: true,
      style: {
        minWidth: '300px',
        width: '50vw',
      },
    },
    data: {
      id: noteData.id,
    },
    templates: {
      header: () => (
        <div class="flex flex-col">
          <strong>Edit note</strong>
          <small>ID {noteData.id}</small>
        </div>
      ),
    },
    onClose(options) {
      console.log(options)
      if (options?.data) {
        // To Submit
        console.log('Submit', options.data)
        const toSubmitNote: UpdateNoteModel = {
          content: options.data.content,
          isArchived: options.data.isArchived,
          noteCategoriesId: options.data.noteCategories.map((nc: { id: number }) => nc.id),
          title: options.data.title,
        }
        noteService.updateNote(noteData.id, toSubmitNote).then(() => {
          toast.add({
            severity: 'success',
            life: 2000,
            summary: 'New Note',
            detail: 'Successfully Updated',
          })
          emits('updated-note')
        })
      }
    },
  })
}

function openConfirmDeleteDialog(note: Note) {
  confirm.require({
    accept() {
      noteService.deleteNote(note.id).then((res) => {
        console.log(res.data)
        toast.add({
          life: 2000,
          detail: 'Successfully deleted.',
          summary: 'Success',
          severity: 'success',
        })
        emits('updated-note')
      })
    },
    reject() {},
    header: 'Confirm Deletion',
    icon: 'pi pi-trash',
    message: `Are you sure you want to delete note with ID ${note.id}?`,
    acceptLabel: 'Confirm',
    rejectLabel: 'Cancel',
    acceptIcon: 'pi pi-check',
    rejectIcon: 'pi pi-times',
    acceptProps: {
      severity: 'success',
    },
    rejectProps: {
      severity: 'danger',
    },
  })
}

function openConfirmIsArchiveDialog(note: Note) {
  confirm.require({
    accept() {
      noteService.setArchiveStatusFromNote(note.id, !note.isArchived).then(() => {
        toast.add({
          severity: 'success',
          summary: 'Success',
          life: 2000,
          detail: `Successfully ${note.isArchived ? 'actived' : 'archived'}`,
        })
        emits('updated-note')
      })
    },
    reject() {},
    header: `Confirm ${note.isArchived ? 'Active' : 'Archive'}?`,
    icon: 'pi pi-trash',
    message: `Are you sure you want to ${note.isArchived ? 'active' : 'archive'} note with ID ${note.id}?`,
    acceptLabel: 'Confirm',
    rejectLabel: 'Cancel',
    acceptIcon: 'pi pi-check',
    rejectIcon: 'pi pi-times',
    acceptProps: {
      severity: 'success',
    },
    rejectProps: {
      severity: 'danger',
    },
  })
}
</script>
<style lang=""></style>
