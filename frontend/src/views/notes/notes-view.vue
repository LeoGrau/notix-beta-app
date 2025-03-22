<template>
  <div class="p-4 flex flex-col gap-3">
    <div class="flex">
      <pv-button
        severity="success"
        label="New"
        icon="pi pi-plus"
        @click="openCreateNoteDialog()"
      ></pv-button>
    </div>
    <pv-tabs value="0">
      <pv-tab-list>
        <pv-tab value="0">Active</pv-tab>
        <pv-tab value="1">Archive</pv-tab>
      </pv-tab-list>
      <pv-tab-panels>
        <pv-tab-panel value="0">
          <h1>Active Notes</h1>
          <div class="flex items-center gap-2">
            <pv-float-label class="my-3 max-w-[300px] w-full" variant="on">
              <pv-select
                @value-change="getNotesByCategoryIdAndIsArchive(false)"
                class="w-full"
                v-model="selectedCategory"
                :options="categories"
                optionLabel="name"
              ></pv-select>
              <label>Category Filter</label>
            </pv-float-label>
            <div class="flex gap-2">
              <!-- <i class="pi pi-refresh text-black size-6">
                Trying to add a spin animation.
              </i> -->
              <pv-button icon="pi pi-refresh" label="Reset" @click="getActiveNotes()"></pv-button>
            </div>
          </div>
          <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-5 gap-3">
            <notix-note
              @updated-note="setAll()"
              :note="note"
              v-for="(note, index) in activeNotes"
              :key="index"
            ></notix-note>
          </div>
        </pv-tab-panel>
        <pv-tab-panel value="1">
          <h1>Archived Notes</h1>
          <div class="flex items-center gap-2">
            <pv-float-label class="my-3 max-w-[300px] w-full" variant="on">
              <pv-select
                @value-change="getNotesByCategoryIdAndIsArchive(true)"
                class="w-full"
                v-model="selectedCategory"
                :options="categories"
                optionLabel="name"
              ></pv-select>
              <label>Category Filter</label>
            </pv-float-label>
            <div class="flex gap-2">
              <pv-button icon="pi pi-refresh" label="Reset" @click="getArchivedNotes()"></pv-button>
            </div>
          </div>
          <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-5 gap-3">
            <notix-note
              @updated-note="setAll()"
              :note="note"
              v-for="(note, index) in archivedNotes"
              :key="index"
            ></notix-note>
          </div>
        </pv-tab-panel>
      </pv-tab-panels>
    </pv-tabs>
  </div>
</template>
<script setup lang="tsx">
import NotixNote from '@/components/notix-note/notix-note.vue'
import noteService from '@/services/api/note.service'
import { useDialog } from 'primevue'
import { onMounted, ref } from 'vue'
import CreateNoteDialog from './dialog/create-note-dialog.vue'
import type CreateNoteModel from '@/types/models/note/create.note.model'
import categoryService from '@/services/api/category.service'
import { useAuthStore } from '@/stores/auth.store'

const archivedNotes = ref([])
const activeNotes = ref([])

const dialog = useDialog()
const categories = ref([])

const selectedCategory = ref({} as any)
const authStore = useAuthStore()
const userId = ref(authStore.userId);

function openCreateNoteDialog() {
  dialog.open(CreateNoteDialog, {
    props: {
      modal: true,
      style: {
        minWidth: '600px',
      },
    },
    templates: {
      header: () => (
        <div class="flex flex-col">
          <strong>Create Note</strong>
          <small>Add a new note!</small>
        </div>
      ),
    },
    onClose(options) {
      if (options?.data) {
        console.log(userId.value)
        const newNote: CreateNoteModel = {
          userId: userId.value,
          content: options.data.content,
          isArchived: options.data.isArchived,
          title: options.data.title,
          noteCategoriesId: options.data.noteCategories.map((nc: { id: number }) => nc.id),
        }
        console.log(newNote)
        noteService.createNote(newNote).then(() => {
          setAll()
        })
      }
    },
  })
}

function getArchivedNotes() {
  return noteService.getNotesByUserAndStatus(true, userId.value).then((res) => (archivedNotes.value = res.data)).then((res) => console.log(res))
}

function getActiveNotes() {
  return noteService.getNotesByUserAndStatus(false, userId.value).then((res) => (activeNotes.value = res.data)).then((res) => console.log(res))
}

function getCategories() {
  return categoryService.getAllCategories().then((res) => (categories.value = res.data))
}

function getNotesByCategoryIdAndIsArchive(isArchived: boolean = false) {
  console.log(selectedCategory.value.id)
  return noteService.getNotesByCategoryId(selectedCategory.value.id, isArchived).then((res) => {
    if (isArchived) {
      archivedNotes.value = res.data
    } else {
      activeNotes.value = res.data
    }
  })
}

async function setAll() {
  await getArchivedNotes()
  await getActiveNotes()
  await getCategories()
}

onMounted(() => {
  setAll()
})
</script>
<style lang=""></style>
