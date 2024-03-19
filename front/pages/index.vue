<template>
  <v-data-table :items="users"> </v-data-table>
</template>  
<script setup>
definePageMeta({
  middleware: ["auth"],
});
import { onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
const store = useMyStore();

const headers = computed(() => [
  { title: "lastname", key: "firstName" },
  { title: "firstname", key: "lastName" },
  { title: "Email", key: "email" },
  { title: "Role", key: "role" },
]);
const users = computed(() => store.users);
onMounted(async () => {
  await store.getUsers();
});
</script>
