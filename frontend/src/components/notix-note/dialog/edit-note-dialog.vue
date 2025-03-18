<template>
  <pv-form
    ref="formRef"
    :resolver="resolver"
    :initialValues="formValues"
    @submit="save($event)"
    class="mt-2 flex flex-col gap-4"
  >
    <div class="flex">
      <pv-toggle-button
        v-model="formValues.isArchived"
        name="isArchived"
        onIcon="pi pi-folder-plus"
        onLabel="Archived"
        offIcon="pi pi-check"
        offLabel="Active"
      ></pv-toggle-button>
    </div>
    <pv-fieldset v-for="(fieldSet, index) in fieldSets" :key="index" :legend="fieldSet.legend">
      <div class="grid grid-cols-2 w-full gap-4">
        <pv-float-label
          v-for="(field, index) in fieldSet.fields"
          :key="index"
          variant="on"
          class="col-span-2"
        >
          <component
            v-model="formValues[field.bind.name as keyof typeof formValues]"
            v-on="field.on"
            v-bind="field.bind"
            :is="field.fieldType"
            class="w-full"
            fluid
          >
            <template #footer v-if="field.fieldLabel">
              <div class="p-3 flex justify-between">
                <pv-button
                  label="Add New"
                  severity="secondary"
                  text
                  size="small"
                  icon="pi pi-plus"
                  @click="openConfirmAddCategory()"
                />
                <pv-button
                  label="Remove All"
                  severity="danger"
                  text
                  size="small"
                  icon="pi pi-times"
                  @click="formValues.noteCategories = []"
                />
              </div>
            </template>
          </component>
          <label>{{ field.fieldLabel }}</label>
        </pv-float-label>
      </div>
    </pv-fieldset>
    <!-- <pv-fieldset legend="Categories">
      <pv-multi-select
        display="chip"
        filter
        class="w-full"
        :options="categories"
        optionLabel="name"
        v-model="selectedCategories"
      ></pv-multi-select>
    </pv-fieldset> -->
    <div class="flex justify-end gap-2 mt-5">
      <pv-button type="submit" label="Save" severity="success" icon="pi pi-save"></pv-button>
      <pv-button label="Cancel" severity="danger" icon="pi pi-times" @click="cancel()"></pv-button>
    </div>
  </pv-form>
</template>

<script setup lang="ts">
import noteService from '@/services/api/note.service'
import type { DynamicDialogInstance } from 'primevue/dynamicdialogoptions'
import { inject, computed, onMounted, ref, type Ref } from 'vue'
// import type { Category } from '@/types/category.type'
import categoryService from '@/services/api/category.service'

import { zodResolver } from '@primevue/forms/resolvers/zod'
import { z } from 'zod'
import { useConfirm, useToast } from 'primevue'
import type CreateCategoryModel from '@/types/models/category/create.category.model'

const dialogRef = inject<Ref<DynamicDialogInstance>>('dialogRef')

const confirm = useConfirm()
const toast = useToast()

const noteId = dialogRef?.value.data.id
const note = ref({})
const formRef = ref()
const categories = ref([])

console.log(categories)

const resolver = ref(
  zodResolver(
    z.object({
      title: z.string(),
      content: z.string(),
    }),
  ),
)

const categoryFilter = ref('')

const formValues = ref({
  title: '',
  content: '',
  noteCategories: [],
  isArchived: false,
})

function openConfirmAddCategory() {
  confirm.require({
    async accept() {
      const newCategory: CreateCategoryModel = {
        name: categoryFilter.value,
      }
      await categoryService.createCategory(newCategory)
      const res = await categoryService.getAllCategories()
      toast.add({
        severity: 'success',
        summary: 'Successful',
        detail: 'Successfully added.',
        life: 2000,
      })
      categories.value = res.data
    },
    reject() {
      categoryFilter.value = ''
    },
    header: 'Confirm Addition',
    icon: 'pi pi-trash',
    message: `You want to add a new category called '${categoryFilter.value}'? 🤔`,
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

const fieldSets = computed(() => [
  {
    legend: 'General',
    fields: [
      {
        fieldType: 'pv-input-text',
        fieldLabel: 'Title',
        bind: { name: 'title' },
        on: {},
      },
      {
        fieldType: 'pv-text-area',
        fieldLabel: 'Content',
        bind: { name: 'content' },
        on: {},
      },
    ],
  },
  {
    legend: 'Categories',
    fields: [
      {
        fieldType: 'pv-multi-select',
        fieldLabel: 'Categories',
        bind: {
          name: 'noteCategories',
          options: categories.value,
          optionLabel: 'name',
          filter: true,
          display: 'chip',
        },
        on: {
          filter: (event: any) => {
            categoryFilter.value = event.value
          },
        },
      },
    ],
  },
])

const save = (event: any) => {
  console.log(event)
  console.log('formu', formValues.value)
  dialogRef?.value.close(formValues.value)
}

const cancel = () => {
  dialogRef?.value.close()
}

async function setNote() {
  console.log(noteId)
  noteService.getNoteById(noteId).then(async (res) => {
    console.log(res.data)

    // Assign properties from right to left.
    Object.assign(formValues.value, res.data)

    note.value = res.data
  })
}

function getCategories() {
  categoryService.getAllCategories().then((res) => (categories.value = res.data))
}

async function setAll() {
  await setNote()
  await getCategories()
}

onMounted(() => {
  setAll()
})
</script>
<style lang=""></style>
