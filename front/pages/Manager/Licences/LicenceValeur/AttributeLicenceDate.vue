<template>
  <v-menu v-model="isMenuOpenValeur" :close-on-content-click="false">
    <template v-slot:activator="{ props }">
      <v-text-field
        variant="outlined"
        base-color="green"
        :label="propss.textDescription"
        :model-value="formattedDateValeur"
        readonly
        v-bind="props"
        @input="emitTextData"
      ></v-text-field>
    </template>
    <v-date-picker
      v-model="selectedDateValeur"
      :min="minDateValeur"
    ></v-date-picker>
  </v-menu>
</template>

<script setup>
const propss = defineProps({
  textDescription: {
    type: Array,
    required: true,
  },
});
const selectedDateValeur = ref(new Date());
const minDateValeur = new Date();
const isMenuOpenValeur = ref(false);
const { emit } = getCurrentInstance();
import { ref } from "vue";
const formattedDateValeur = computed(() => {
  const date = selectedDateValeur.value;
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
});
watch(selectedDateValeur, (newValue, oldValue) => {
  // You can perform any actions here when the selectedDateValeur changes
  console.log("Selected Enumeration changed from", oldValue, "to", newValue);
  // Example: Emitting the new value
  emitTextData();
});
onMounted(async () => {
  await emitTextData();
  //console.log("data recieved from addLicence ", props.textDescription);
});

const emitTextData = () => {
  // Emit an event named "textData" with the value of texte
  emit("DateData", formattedDateValeur.value);
};
</script>