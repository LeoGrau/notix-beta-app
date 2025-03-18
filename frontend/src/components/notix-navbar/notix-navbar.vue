<template>
  <div class="bg-slate-800 h-[70px] w-full flex justify-between items-center px-4">
    <div class=""><h1 class="tektur-font text-white text-3xl">Notix</h1></div>
    <ul class="flex gap-6 items-center">
      <li v-for="(route, index) in routes" :key="index">
        <router-link :to="{ name: route.name }"
          ><span class="text-gray-300 text-sm">{{ route.label }}</span></router-link
        >
      </li>
    </ul>
  </div>
</template>
<script lang="ts" setup>
import type { Route } from '@/types/route.type'
import { onMounted } from 'vue'
interface NotixNavbarProps {
  routes: Route[]
}

const props = defineProps<NotixNavbarProps>()

onMounted(() => {
  console.log(props)
})
</script>
<style lang=""></style>
