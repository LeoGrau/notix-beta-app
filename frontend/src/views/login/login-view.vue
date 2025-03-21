<template>
  <div class="h-full flex items-center justify-center flex-col">
    <div class="relative">
      <div
        class="flex flex-col items-center justify-start gap-2 pb-4 absolute top-[-40%] inset-x-0"
      >
        <i class="text-4xl">🚀</i>
        <h1 class="text-4xl tektur-font">Notix</h1>
      </div>
      <div class="bg-surface-900 p-9 rounded-md border border-surface-700 flex flex-col">
        <h1 class="text-center text-2xl montserrat-font mb-5">Log In</h1>
        <pv-form @submit="submit" :initialValues class="min-w-[400px]">
          <div class="w-full flex flex-col gap-5">
            <pv-float-label
              class="w-full"
              variant="on"
              v-for="(field, index) in inputFields"
              :key="index"
            >
              <component
                v-model="initialValues[field.fieldName as keyof typeof initialValues]"
                :is="field.inputType"
                class="w-full"
                name=""
              ></component>
              <label>{{ field.fieldLabel }}</label>
            </pv-float-label>
          </div>
          <div class="mt-4 flex gap-2 justify-center">
            <pv-button type="submit" label="Log In"></pv-button>
          </div>
        </pv-form>
        <div class="flex justify-center mt-6">
          <span class="text-sm"
            >Don't have an account?
            <router-link class="text-indigo-600" :to="{ name: 'register-view' }"
              >Click here.</router-link
            ></span
          >
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref } from 'vue'
import { useAuthStore } from '@/stores/auth.store'
import type AuthModel from '@/types/models/auth/auth.model'
import { useToast } from 'primevue/usetoast'
import { useRouter } from 'vue-router'
import { AxiosError } from 'axios'

const initialValues = ref({
  email: '',
  password: '',
})

const router = useRouter()
const authStore = useAuthStore()
const toast = useToast()

const inputFields = ref([
  {
    fieldName: 'email',
    fieldLabel: 'Email',
    inputType: 'pv-input-text',
  },
  {
    fieldName: 'password',
    fieldLabel: 'Password',
    inputType: 'pv-input-password',
  },
])

function submit() {
  const authModel: AuthModel = {
    email: initialValues.value['email'],
    password: initialValues.value['password'],
  }

  authStore.login(authModel).then((res) => {
    console.log(res.data)
    toast.add({ life: 2000, detail: 'Logged in', summary: 'Success', severity: 'success' })
    router.push({ name: 'home-view' })
  }).catch((res: AxiosError<any>) => {
    console.log(typeof res, res)
    toast.add({ life: 2000, detail: `${res.response!.data.message}`, summary: 'Error', severity: 'error' })
  })
}
</script>
<style scoped></style>
