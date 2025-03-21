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
        <pv-form :initialValues class="min-w-[400px]">
          <div class="w-full flex flex-col gap-5">
            <pv-float-label
              class="w-full"
              variant="on"
              v-for="(field, index) in inputFields"
              :key="index"
            >
              <pv-input-text class="w-full" name=""></pv-input-text>
              <label>{{ field.fieldLabel }}</label>
            </pv-float-label>
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

const initialValues = ref({
  email: '',
  password: '',
})

const inputFields = ref([
  {
    fieldName: 'firstName',
    fieldLabel: 'First Name',
  },
  {
    fieldName: 'lastName',
    fieldLabel: 'Last Name',
  },
  {
    fieldName: 'email',
    fieldLabel: 'Email',
  },
  {
    fieldName: 'password',
    fieldLabel: 'Password',
  },
  {
    fieldName: 'confirmPassword',
    fieldLabel: 'Confirm Password',
  },
])
</script>
<style lang=""></style>
