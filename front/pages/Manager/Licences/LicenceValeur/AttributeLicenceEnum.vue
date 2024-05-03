<template>
  <v-select
    v-model="selectedEnumValue"
    :label="props.textDescription"
    base-color="green"
    :items="dataEnum.map((item) => item.valeur)"
    @blur="v$.selectedEnumValue.$touch()"
    @input="emitTextData"
    :error-messages="v$.selectedEnumValue.$errors.map((e) => e.$message)"
    variant="outlined"
  ></v-select>
</template>
<script setup>
import { ref } from "vue";
import axios from "axios";
import { defineProps } from "vue";
import { required, helpers } from "@vuelidate/validators";
import { useVuelidate } from "@vuelidate/core";
const { emit } = getCurrentInstance();
const dataEnum = ref([]);
const selectedEnumValue = ref(null);

const { withMessage } = helpers;

const rules = {
  selectedEnumValue: {
    required: withMessage("Enumeration Valeur  obligatoire", required),
  },
};
const v$ = useVuelidate(rules, {
  selectedEnumValue,
});
const emitTextData = () => {
  // Log the value of Enumeration before emitting
  //console.log("Selected Enumeration:", selectedEnumValue.value);

  // Emit an event named "EnumData" with the value of Enumeration
  emit("EnumData", selectedEnumValue.value);
};
const props = defineProps({
  enumerationId: {
    type: Number,
    required: true,
  },
  textDescription: {
    type: Array,
    required: true,
  },
});
onMounted(async () => {
  await getEnumVal();
  await emitTextData();
});

const getEnumVal = async () => {
  try {
    //console.log(props.enumerationId);
    const response = await axios.get(
      `http://localhost:5252/api/enumerationvaleur/getenumval/${props.enumerationId}`
    );
    //   console.log(response);
    dataEnum.value = response.data;

    //console.log(dataEnum.value);
  } catch (error) {
    //console.error(error);
  }
};
watch(selectedEnumValue, (newValue, oldValue) => {
  // You can perform any actions here when the selectedEnumValue changes
  //console.log("Selected Enumeration changed from", oldValue, "to", newValue);
  // Example: Emitting the new value
  emitTextData();
});
</script>