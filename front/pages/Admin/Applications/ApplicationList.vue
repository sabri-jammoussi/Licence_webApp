<template>
  <v-card class="mx-auto my-2" elevation="4"   v-if="isAdmin">
    <ApplicationListAdmin />
  </v-card>
  <v-card class="mx-auto my-2" elevation="4" v-else-if="isManager">
    <ApplicationListManager />
  </v-card>
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
        