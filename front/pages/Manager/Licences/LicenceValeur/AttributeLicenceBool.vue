<template>
  <v-container fluid>
    <v-checkbox
      v-model="boolValues"
      @input="emitTextData"
    >
      <template v-slot:label>
        <div>{{ props.textDescription }}</div>
      </template>
    </v-checkbox>
  </v-container>
</template>
  <script setup>
import { ref } from "vue";
import { defineProps } from "vue";
const props = defineProps({
  textDescription: {
    type: Array,
    required: true,
  },
});
const { emit } = getCurrentInstance();
const boolValues = ref(false);
watch(boolValues, (newValue, oldValue) => {
  // You can perform any actions here when the selectedEnumValue changes
  console.log("Selected Enumeration changed from", oldValue, "to", newValue);
  // Example: Emitting the new value
  emitTextData();
});
onMounted(async () => {
  await emitTextData();
  //console.log("data recieved from addLicence ", props.textDescription);
});
const emitTextData = () => {
  console.log("Emitting boolData:", boolValues.value);
  emit("boolData", boolValues.value);
};


</script>
  