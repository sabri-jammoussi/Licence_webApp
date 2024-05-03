<template>
  <v-card class="mx-auto my-2 card" elevation="" v-if="isAdmin">
    <ApplicationListAdmin />
  </v-card>
  <v-card class="mx-auto my-2 card" elevation="" v-else-if="isManager">
    <ApplicationListManager />
  </v-card>
</template>  
    <script setup>
import { useMyStore } from "@/store/index.js";
import ApplicationListAdmin from "./ApplicationListAdmin.vue";
import ApplicationListManager from "./ApplicationListManager.vue";
const store = useMyStore();
const userrole = computed(() => store.user?.role);
onMounted(() => {
  console.log("useers", userrole);
});
const isAdmin = computed(() => userrole.value === "Admin");
const isManager = computed(() => userrole.value === "Manager");
</script>
<style>
.card {
  border-radius: 10px;
  box-shadow: 0px 15px 15px #84848733;
  justify-content: center;
  align-items: center;
  margin: 1% 0% 1% 0%;
}
</style>