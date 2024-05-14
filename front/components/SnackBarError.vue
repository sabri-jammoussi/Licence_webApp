<template>
  <v-snackbar
    v-model="snackbarError"
    :timeout="timeout"
    color="error"
    :location="location"
  >
    {{ message }}
  </v-snackbar>
</template>
      
    <script setup>
const props = defineProps({
  message: { type: String, required: true },
  showSnackbarError: { type: Boolean, required: true },
  timeout: { type: Number, default: 2500 },
  location: { type: String, default: "top" },
});
const snackbarError = ref(false);
onMounted(() => {
  console.log("show snack bar errror", props.showSnackbarError);
  console.log("show snack bar Message", props.message);
  snackbarError.value = props.showSnackbarError;
});
</script>
    