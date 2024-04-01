<template>
  <v-snackbar
    v-model="snackbar"
    :timeout="timeout"
    color="success"
    :location="location"
  >
    {{ message }}
    <v-progress-linear v-if="showSnackBar" indeterminate color="white" />
  </v-snackbar>
</template>
    
  <script setup>
import { defineProps } from "vue";

const props = defineProps({
  message: { type: String, required: true },
  showSnackBar: { type: Boolean, required: true },
  timeout: { type: Number, default: 2500 },
  location: { type: String, default: "top" },
});
const snackbar = ref();
onMounted(() => {
  console.log("show snack bar",  props.showSnackBar);
  console.log("show snack bar Message",  props.message);
  snackbar.value = props.showSnackBar;
});
</script>
  