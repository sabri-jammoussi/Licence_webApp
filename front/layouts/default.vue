<template>
  <div v-if="isLoading" class="progress_circular">
    <v-progress-circular
      :size="60"
      color="success"
      indeterminate
    ></v-progress-circular>
  </div>
  <div v-else>
    <v-layout v-if="isAdmin">
      <UsersDrawerAdminDrawer />
    </v-layout>
    <v-layout v-else-if="isClient">
      <UsersDrawerClientDrawer />
    </v-layout>
    <v-layout v-else-if="isManager">
      <UsersDrawerManagerDrawer />
    </v-layout>
    <v-layout v-else-if="isPartenaire">
      <UsersDrawerPartenaireDrawer />
    </v-layout>
  </div>
</template>
  
<script setup>

import { ref, onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
const isLoading = ref(true);
const store = useMyStore();
const userrole = computed(() => store.user?.role);
const isAdmin = computed(() => userrole.value === "Admin");
const isManager = computed(() => userrole.value === "Manager");
const isPartenaire = computed(() => userrole.value === "Partenaire");
const isClient = computed(() => userrole.value === "Client");
onMounted(async () => {
  await setTimeout(() => {
    isLoading.value = false;
  }, 1500);
  await store.loadTokenFromLocalStorage();
  console.log("roleeeeee", userrole.value);
});
</script>
<style scoped>
.progress_circular {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>
  