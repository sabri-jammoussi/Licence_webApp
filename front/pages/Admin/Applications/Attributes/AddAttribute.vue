<template>
  <v-card class="mx-left" max-width="" height="100%" elevation="4">
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
import { ref, onMounted } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import SnackBar from "~/components/SnackBar.vue";
import SnackBarError from "~/components/SnackBarError.vue";
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
import axios from "axios";
const intutile = ref("");
const type = ref("");
const description = ref("");
const { withMessage } = helpers;
const data = ref([]);
const TypeOptions = ref([]);
const loading = ref(false);
const props = defineProps({
  emitId: {
    type: Number,
    required: true,
  },
});
const rules = {
  intutile: {
    required: withMessage("valeur obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  description: {
    required: withMessage("valeur obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  type: {
    required: withMessage("type obligatoire", required),
  },
};
const v$ = useVuelidate(
  rules,
  {
    intutile,
    description,
    type,
  },
  { $stopPropagation: true }
);
const addAttribute = () => {
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
        const id = parseInt(props.emitId);
        const data = {
          intutile: intutile.value,
          description: description.value,
          type: type.value,
          applicationId: id,
        };
        const response = await axios.post(
          "http://localhost:5252/api/attributelicence",
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
        if (
          error.response &&
          error.response.data &&
          error.response.data.message
        ) {
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
        await new Promise((resolve) => setTimeout(resolve, 1510)); // Adjust time as needed
        location.reload();
      }
    }, 1500);
  }
};
const close = () => {
  // Reset form fields and validation
  (intutile.value = ""),
    (description.value = ""),
    (type.value = ""),
    v$.value.$reset();

  loading.value = false;
};
onMounted(async () => {
  await getAttributeType();
  console.log("type ", data.value);
});
const getAttributeType = async () => {
  try {
    const response = await axios.get(
      "http://localhost:5252/api/attributelicence/type"
    );
    TypeOptions.value = response.data.types;
    console.log("type ", TypeOptions.value);
  } catch (error) {
    //console.error(error);
  }
};
</script>