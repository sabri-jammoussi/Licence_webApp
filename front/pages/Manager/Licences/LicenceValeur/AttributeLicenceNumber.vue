<!-- ChildComponent.vue -->
<template>
  <v-text-field
    v-model="Number"
    :label="props.textDescription"
    type="number"
    base-color="green"
    variant="outlined"
    @blur="v$.Number.$touch()"
    @input="emitTextData"
    :error-messages="v$.Number.$errors.map((e) => e.$message)"
  ></v-text-field>
</template>
  
  <script setup>
import { ref } from "vue";
import { defineProps } from "vue";
import { required, helpers } from "@vuelidate/validators";
import { useVuelidate } from "@vuelidate/core";
const Number = ref("");

const { withMessage } = helpers;

const rules = {
  Number: {
    required: withMessage("Number obligatoire", required),
  },
};
const v$ = useVuelidate(rules, {
  Number,
});
const props = defineProps({
  textDescription: {
    type: Array,
    required: true,
  },
});

const { emit } = getCurrentInstance();

const emitTextData = () => {
  // Emit an event named "textData" with the value of texte
  //console.log("Selected Enumeration:", Number.value);

  emit("NumberData", Number.value);
};
</script>
  