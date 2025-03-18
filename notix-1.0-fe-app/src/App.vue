<template>
  <div></div>
  <pv-confirm-dialog></pv-confirm-dialog>
  <pv-toast></pv-toast>
  <pv-dynamic-dialog></pv-dynamic-dialog>
  <notix-navbar :routes="routes"></notix-navbar>
  <router-view></router-view>
</template>
<script lang="ts" setup>
import NotixNavbar from './components/notix-navbar/notix-navbar.vue'
import type { Route } from './types/route.type'
import { ref } from 'vue'

const routes = ref([
  { label: 'Home', name: 'home-view', path: '/' },
  { label: 'Notes', name: 'notes-view', path: '/notes' },
] as Route[])
</script>
<style scoped></style>
