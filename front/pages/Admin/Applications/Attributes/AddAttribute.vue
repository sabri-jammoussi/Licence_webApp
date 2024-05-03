<template>
  <v-dialog v-model="addDialog" max-width="500px">
    <v-card class="mx-left" max-width="" height="100%" elevation="">
      <v-card-text>
        <v-container>
          <v-spacer></v-spacer>
          <v-row no-gutters>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                base-color="green"
                :label="$t('intutile')"
                variant="outlined"
                v-model="intutile"
                @blur="v$.intutile.touch"
                @input="v$.intutile.$touch"
                :error-messages="v$.intutile.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                base-color="green"
                variant="outlined"
                label="Description"
                v-model="description"
                @blur="v$.description.touch"
                @input="v$.description.$touch"
                :error-messages="v$.description.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-select
                variant="outlined"
                v-model="type"
                label="Type"
                base-color="green"
                :items="TypeOptions"
                @blur="v$.type.touch"
                @input="v$.type.$touch"
                :error-messages="v$.type.$errors.map((e) => e.$message)"
              ></v-select>
            </v-col>
            <v-col cols="12" sm="6" md="12" v-if="type === 'Enumeration'">
              <v-select
                variant="outlined"
                v-model="EnumerationVal"
                label="Enumeration"
                base-color="green"
                :items="dataEnum"
                item-value="id"
                item-title="nom"
              ></v-select>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-checkbox v-model="ObligationCheckbox">
                <template v-slot:label>
                  <div>L'attribut est Obligatoire</div>
                </template>
              </v-checkbox>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
      <v-divider class="my-2"></v-divider>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="green"
          variant="text"
          @click="addAttribute"
          :loading="loading"
        >
          {{ $t("add") }}
        </v-btn>
        <v-btn color="grey" variant="text" @click="close">
          {{ $t("cancel") }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <SnackBar
    :key="keyToast"
    :message="snackbarMessage"
    :showSnackBar="showSnackbar"
    v-if="showSnackbar"
  />
  <SnackBarError
    v-if="showSnackbarError"
    :key="keyToastError"
    :message="snackbarMessageError"
    :showSnackbarError="showSnackbarError"
  />
</template>

<script setup>
import { ref, watch, getCurrentInstance, defineProps, defineEmits } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import axios from "axios";
import SnackBar from "~/components/SnackBar.vue";
import SnackBarError from "~/components/SnackBarError.vue";
const dataEnum = ref([]);
const EnumerationVal = ref("");
const { emit } = getCurrentInstance();
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const showSnackbarError = ref(false);
const snackbarMessageError = ref("");
const keyToastError = ref(0);
const addDialog = ref(false);
const loading = ref(false);
const intutile = ref("");
const type = ref("");
const description = ref("");
const TypeOptions = ref([]);
const typeOptionEnumeration = ref("");
const { withMessage } = helpers;
const ObligationCheckbox = ref(false);
defineEmits(["dataChanged", "close-dialog"]);

const props = defineProps({
  emitId: {
    type: Number,
    required: true,
  },
});

const rules = {
  intutile: { required: required && minLength(3) },
  description: { required: required && minLength(3) },
  type: { required: withMessage("Type obligatoire", required) },
};

const v$ = useVuelidate(
  rules,
  { intutile, description, type },
  { $stopPropagation: true }
);

watch(addDialog, (val) => {
  if (!val) close();
});
const addAttribute = async () => {
  loading.value = true;
  v$.value.$touch();

  if (v$.value.$invalid) {
    loading.value = false;
    return;
  }

  try {
    const id = parseInt(props.emitId);
    console.log("id of enum ", EnumerationVal.value);
    const data = {
      intutile: intutile.value,
      description: description.value,
      type: type.value,
      obligations: ObligationCheckbox.value,
      applicationId: id,
      enumerationId: EnumerationVal.value === "" ? null : EnumerationVal.value,
    };
    console.log("data sended to attributelicence ", data);
    const response = await axios.post(
      "http://localhost:5252/api/attributelicence",
      data
    );

    if (response.status >= 200 && response.status < 300) {
      showSnackbar.value = true;
      keyToast.value++;
      snackbarMessage.value = "Added successfully.";
      console.log("GGGG");
    } else {
      throw new Error("Request failed with status nom " + response.status);
    }
  } catch (error) {
    console.error("Error fetching data:", error);
    snackbarMessageError.value = "An error occurred.";
    showSnackbarError.value = true;
    keyToastError.value++;
  } finally {
    await new Promise((resolve) => setTimeout(resolve, 1510));
    emit("dataChanged");
    addDialog.value = false;
  }
};

const close = () => {
  intutile.value = "";
  description.value = "";
  type.value = "";
  v$.value.$reset();
  emit("close-dialog");
  addDialog.value = false;
  loading.value = false;
};
const getEnumerations = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/enumeration");
    dataEnum.value = response.data.map((i) => ({
      nom: i.nom,
      id: i.id,
    }));
    // dataEnum.value = response.data.map((i) => ({
    //   nom: i.nom,
    // }));
    console.log("Data enumeration ", dataEnum.value);
  } catch (error) {
    console.error(error);
  }
};
const getAttributeType = async () => {
  try {
    const response = await axios.get(
      "http://localhost:5252/api/attributelicence/type"
    );
    console.log("data of types", response.data);

    TypeOptions.value = response.data.types;
    typeOptionEnumeration.value = response.data.types[4];
    console.log("Enumerationnnnnn", typeOptionEnumeration.value);
    console.log("data of types", TypeOptions.value[4]);
  } catch (error) {
    console.error(error);
  }
};

onMounted(async () => {
  await getAttributeType();
  await getEnumerations();
  addDialog.value = true;
});
</script>