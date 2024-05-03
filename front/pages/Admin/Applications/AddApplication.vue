<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template v-slot:activator="{ props }">
      <v-btn color="primary" dark class="mb-2" v-bind="props">
        <v-tooltip location="bottom" activator="parent">{{$t("newApp")}}</v-tooltip>

        <v-icon color="green" style="margin: auto">mdi-plus</v-icon>
        {{ $t("new") }}
      </v-btn>
    </template>
    <v-card>
      <div  class="grey--text text-h6 text-lg-h6 mt-2">
                <v-icon left color="green" size="35" class="ml-2">mdi-apps  </v-icon>
                 {{ $t("newApp") }}
              </div>
              <v-divider></v-divider>
      <v-card-text>
        <v-container>
          
          <v-row no-gutters>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                base-color="green"
                :label="$t('identifier')"
                v-model="identifiant"
                @blur="v$.identifiant.touch"
                @input="v$.identifiant.$touch"
                :error-messages="v$.identifiant.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                v-model="nom"
                :label="$t('name')"
                base-color="green"
                @blur="v$.nom.touch"
                @input="v$.nom.$touch"
                :error-messages="v$.nom.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                v-model="description"
                label="Description"
                base-color="green"
                @blur="v$.description.touch"
                @input="v$.description.$touch"
                :error-messages="v$.description.$errors.map((e) => e.$message)"
              ></v-text-field>
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
          @click="addAplication"
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
    v-if="showSnackbar"
    :message="snackbarMessage"
    :showSnackBar="showSnackbar"
  />
  <SnackBarError
    v-if="showSnackbarError"
    :key="keyToastError"
    :message="snackbarMessageError"
    :showSnackbarError="showSnackbarError"
  />
</template>
  <script setup>
import axios from "axios";
import { ref } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import SnackBar from "~/components/SnackBar.vue";
import SnackBarError from "~/components/SnackBarError.vue";
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const showSnackbarError = ref(false);
const snackbarMessageError = ref("");
const keyToastError = ref(0);
const identifiant = ref("");
const nom = ref("");
const description = ref("");
const dialog = ref(false);
const loading = ref(false);
const { emit } = getCurrentInstance();

const { withMessage } = helpers;

const rules = {
  identifiant: {
    required: withMessage("Identifiant obligatoire", required),
    min: withMessage("Min 13 caractères", minLength(3)),
  },
  nom: {
    required: withMessage("Nom obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  description: {
    required: withMessage("description obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
};
const v$ = useVuelidate(
  rules,
  {
    identifiant,
    nom,
    description,
  },
  { $stopPropagation: true }
);
//stopPropagation bech thot rules 7ad page heki
watch(dialog, (val) => {
  val || close();
});

const addAplication = () => {
  loading.value = true;
  // Clear previous validation errors
  v$.value.$reset();
  // Validate the form
  v$.value.$touch();

  if (v$.value.$invalid) {
    loading.value = false;
  } else {
    setTimeout(async () => {
      try {
        const data = {
          identifiant: identifiant.value,
          nom: nom.value,
          description: description.value,
        };
        const response = await axios.post(
          "http://localhost:5252/api/appliction",
          data
        );

        if (response.status >= 200 && response.status < 300) {
          showSnackbar.value = true;
          keyToast.value++;
          snackbarMessage.value = "Added successfully.";
          // router.push('/users/');
        } else {
          throw new Error("Request failed with status code " + response.status);
        }

        loading.value = false;
      } catch (error) {
        console.error("Error fetching data:", error);
        if (error.response && error.response.data && error.response.data.message) {
          // If there's an error message in the response data, use that
          snackbarMessageError.value = "An error occurred.";
          showSnackbarError.value = true;
        keyToastError.value++;
        } else {
          // Otherwise, use a generic error message
          snackbarMessageError.value = "An error occurred.";
        }
        showSnackbarError.value = true;
        keyToastError.value++;
      } finally {
        close();
        await new Promise((resolve) => setTimeout(resolve, 2510)); // Adjust time as needed
        await emit("dataChanged");
      }
    }, 1500);
  }
};

const close = () => {
  // Reset form fields and validation
  (identifiant.value = ""),
    (nom.value = ""),
    (description.value = ""),
    v$.value.$reset();
  dialog.value = false;
  loading.value = false;
};
</script> 