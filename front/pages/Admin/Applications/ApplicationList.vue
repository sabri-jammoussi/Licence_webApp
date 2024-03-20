<template>
  <v-layout v-if="isAdmin">
    <ApplicationListAdmin />
  </v-layout>
  <v-layout v-else-if="isManager">
    <ApplicationListManager />
  </v-layout>
</template>  
    <script setup>
import { useMyStore } from "@/store/index.js";
import ApplicationListAdmin from "./ApplicationListAdmin.vue";
import ApplicationListManager from "./ApplicationListManager.vue";
const store = useMyStore();
const userrole = computed(() => store.user?.role);
const isAdmin = computed(() => userrole.value === "Admin");
const isManager = computed(() => userrole.value === "Manager");
</script>
        