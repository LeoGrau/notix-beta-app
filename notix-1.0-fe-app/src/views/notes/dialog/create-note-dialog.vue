<template>
  <pv-form :initialValues="formValues" @submit="save($event)" class="mt-2 flex flex-col gap-4">
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
            v-bind="field.bind"
            :is="field.fieldType"
            class="w-full"
            fluid
          ></component>
          <label>{{ field.fieldLabel }}</label>
        </pv-float-label>
      </div>
    </pv-fieldset>
    <div class="flex justify-end gap-2 mt-5">
      <pv-button type="submit" label="Save" severity="success" icon="pi pi-save"></pv-button>
      <pv-button label="Cancel" severity="danger" icon="pi pi-times" @click="cancel()"></pv-button>
    </div>
  </pv-form>
</template>

<script setup lang="ts">
import type { DynamicDialogInstance } from 'primevue/dynamicdialogoptions'
import { inject, computed, onMounted, ref, type Ref } from 'vue'
import categoryService from '@/services/api/category.service'

const dialogRef = inject<Ref<DynamicDialogInstance>>('dialogRef')

const categories = ref([])

console.log(categories)

const formValues = ref({
  title: '',
  content: '',
  noteCategories: [],
  isArchived: false,
})

const fieldSets = computed(() => [
  {
    legend: 'General',
    fields: [
      {
        fieldType: 'pv-input-text',
        fieldLabel: 'Title',
        bind: { name: 'title' },
      },
      {
        fieldType: 'pv-text-area',
        fieldLabel: 'Content',
        bind: { name: 'content' },
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
      },
    ],
  },
])

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const save = (event: any) => {
  console.log(event)
  console.log('formu', formValues.value)
  dialogRef?.value.close(formValues.value)
}

const cancel = () => {
  dialogRef?.value.close()
}

function getCategories() {
  categoryService.getAllCategories().then((res) => (categories.value = res.data))
}

async function setAll() {
  await getCategories()
}

onMounted(() => {
  setAll()
})
</script>
<style lang=""></style>
