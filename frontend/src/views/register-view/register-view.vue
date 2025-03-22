<template>
  <div class="h-full flex items-center justify-center flex-col">
    <div class="relative">
      <div
        class="flex flex-col items-center justify-start gap-2 pb-4 absolute top-[-25%] inset-x-0"
      >
        <i class="text-4xl animate-bounce">🚀</i>
        <h1 class="text-4xl tektur-font">Notix</h1>
      </div>
      <div class="bg-surface-900 p-9 rounded-md border border-surface-700 flex flex-col">
        <h1 class="text-center text-2xl montserrat-font mb-5">Register</h1>
        <pv-form
          v-slot="$form"
          @submit="registerUser"
          :resolver="zResolver"
          :initialValues
          class="min-w-[400px]"
        >
          <div class="w-full flex flex-col gap-5">
            <div :key="index" v-for="(field, index) in inputFields">
              <pv-float-label class="w-full" variant="on">
                <component
                  v-model="initialValues[field.fieldName as keyof typeof initialValues]"
                  :is="field.inputType"
                  class="w-full"
                  :name="field.fieldName"
                ></component>
                <label>{{ field.fieldLabel }}</label>
              </pv-float-label>
              <small v-if="$form[field.fieldName]?.invalid">{{ $form[field.fieldName]?.error.message }}</small>
            </div>
          </div>
          <div class="mt-5 flex justify-center">
            <pv-button label="Register" type="submit"></pv-button>
          </div>
        </pv-form>
        <div class="flex justify-center mt-6">
          <span class="text-sm"
            >Already have an account?
            <router-link class="text-indigo-600" :to="{ name: 'login-view' }"
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
import { z } from 'zod'
import { zodResolver } from '@primevue/forms/resolvers/zod'
import type RegisterModel from '@/types/models/auth/register.model'
import registerService from '@/services/api/register.service'
import { useRouter } from 'vue-router'
import { useToast } from 'primevue'

const router = useRouter()
const toast = useToast()

const zResolver = ref(
  zodResolver(
    z
      .object({
        firstName: z.string().min(3, { message: 'Minimun 3 characters.' }),
        lastName: z.string().min(3, { message: 'Minimun 3 characters.' }),
        email: z.string().email(),
        password: z.string().min(8, { message: 'Minimun 3 characters.' }),
        confirmPassword: z.string().min(8, { message: 'Minimun 3 characters.' }),
      })
      .refine(
        (data) => {
          console.log(data)
          return data.password == data.confirmPassword
        },
        {
          message: 'Password does not match',
          path: ['confirmPassword'],
        },
      ),
  ),
)

const initialValues = ref({
  firstName: '',
  lastName: '',
  email: '',
  password: '',
  confirmPassword: '',
})

const inputFields = ref([
  {
    fieldName: 'firstName',
    fieldLabel: 'First Name',
    inputType: 'pv-input-text',
  },
  {
    fieldName: 'lastName',
    fieldLabel: 'Last Name',
    inputType: 'pv-input-text',
  },
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
  {
    fieldName: 'confirmPassword',
    fieldLabel: 'Confirm Password',
    inputType: 'pv-input-password',
  },
])

function registerUser({ valid }: { valid: boolean }) {
  if (valid) {
    const registerRequest: RegisterModel = {
      firstName: initialValues.value['firstName'],
      lastName: initialValues.value['lastName'],
      email: initialValues.value['email'],
      password: initialValues.value['password'],
      confirmPassword: initialValues.value['confirmPassword'],
    }
    registerService
      .register(registerRequest)
      .catch((error) => console.log(error))
      .then(() => router.push({ name: 'login-view' }))
  } else toast.add({ life: 2000, detail: 'Form is in invalid', summary: 'Invalid', severity: 'error' })
}
</script>
<style lang=""></style>
