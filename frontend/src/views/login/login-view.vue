<template>
  <div class="h-full flex items-center justify-center">
    <pv-tabs value="0">
      <pv-tab-list class="flex">
        <pv-tab class="w-full" value="0">Login</pv-tab>
        <!-- <pv-tab class="w-1/2" value="1">Register</pv-tab> -->
      </pv-tab-list>
      <pv-tab-panels>
        <pv-tab-panel value="0" class="p-5">
          <pv-form :initialValues class="min-w-[400px]">
            <div class="w-full flex flex-col gap-5">
              <pv-float-label class="w-full" variant="on" v-for="(field, index) in inputFields" :key="index">
                <pv-input-text class="w-full" name=""></pv-input-text>
                <label>{{ field.fieldLabel }}</label>
              </pv-float-label>
            </div>
          </pv-form>
        </pv-tab-panel>
        <!-- <pv-tab-panel value="1">
          <pv-form :initialValues>
            <div class="w-full flex flex-col gap-5">
              <pv-float-label variant="on" v-for="(field, index) in inputFields" :key="index">
                <pv-input-text name=""></pv-input-text>
                <label>{{ field.fieldLabel }}</label>
              </pv-float-label>
            </div>
          </pv-form>
        </pv-tab-panel> -->
      </pv-tab-panels>
    </pv-tabs>
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
    fieldName: 'email',
    fieldLabel: 'Email',
    fi
  },
  {
    fieldName: 'password',
    fieldLabel: 'Password',
  },
])
</script>
<style lang=""></style>
