<template>
  <v-data-table :items="data"> </v-data-table>
</template>  
<script setup>
definePageMeta({
  middleware: ["auth"],
});
import axios from "axios";
import { onMounted } from "vue";
const data = ref([]);
const getUsers = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/Users");

    data.value = response.data;
    console.log(data);
  } catch (error) {
    console.error(error);
  }
};
onMounted(async () => {
  await getUsers();
});
</script>
