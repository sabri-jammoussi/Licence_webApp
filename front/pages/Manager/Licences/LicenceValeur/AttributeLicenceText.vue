<!-- ChildComponent.vue -->
<template>
  <v-text-field
    v-model="texte"
    :label="props.textDescription"
    base-color="green"
    variant="outlined"
    @blur="v$.texte.$touch()"
    @input="emitTextData"
    :error-messages="v$.texte.$errors.map((e) => e.$message)"
  ></v-text-field>
</template>

<script setup>
import { ref } from "vue";
import { defineProps } from "vue";
import { required, helpers } from "@vuelidate/validators";
import { useVuelidate } from "@vuelidate/core";
const texte = ref("");

const { withMessage } = helpers;

const rules = {
  texte: {
    required: withMessage("Texte obligatoire", required),
  },
};
const v$ = useVuelidate(
  rules,
  {
    texte,
  },

);
const props = defineProps({
  textDescription: {
    type: Array,
    required: true,
  },
});
const { emit } = getCurrentInstance();


const emitTextData = () => {
  // Emit an event named "textData" with the value of texte
  emit("textData", texte.value);
};
</script>
