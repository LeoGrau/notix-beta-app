<template>
  <pv-confirm-dialog></pv-confirm-dialog>
  <pv-toast></pv-toast>
  <pv-dynamic-dialog></pv-dynamic-dialog>
  <div class="h-full">
    <notix-navbar v-if="!route.meta.hideNavbar" :routes="routes"></notix-navbar>
    <router-view class="min-h-[calc(100%-70px)]"></router-view>
  </div>
</template>
<script lang="ts" setup>
import { useRoute } from 'vue-router'
import NotixNavbar from './components/notix-navbar/notix-navbar.vue'
import type { Route } from './types/route.type'
import { ref } from 'vue'

// const route = useRoute();

const route = useRoute()
const routes = ref([
  { label: 'Home', name: 'home-view', path: '/' },
  { label: 'Notes', name: 'notes-view', path: '/notes' },
] as Route[])
</script>
<style scoped></style>
