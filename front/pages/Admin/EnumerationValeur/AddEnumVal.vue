<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template v-slot:activator="{ props }">
      <v-btn icon class="mb-2" v-bind="props">
        <v-icon color="blue">mdi-plus</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-text>
        <v-container>
          <v-row no-gutters>
            <v-col cols="12" sm="4" md="12">
              <v-text-field
                variant="outlined"
                base-color="green"
                label="Valeur"
                v-model="valeur"
                @blur="v$.valeur.touch"
                @input="v$.valeur.$touch"
                :error-messages="v$.valeur.$errors.map((e) => e.$message)"
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
          @click="addEnumVAl"
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
</template>
<script setup>
const { emit } = getCurrentInstance();

import axios from "axios";
import { ref } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
const valeur = ref("");
const dialog = ref(false);
const loading = ref(false);
const { withMessage } = helpers;
const props = defineProps({
  enumId: {
    type: Number,
    required: true,
  },
});
const rules = {
  valeur: {
    required: withMessage("valeur obligatoire", required),
    min: withMessage("Min 13 caractères", minLength(3)),
  },
};
const v$ = useVuelidate(
  rules,
  {
    valeur,
  },
  { $stopPropagation: true }
);
//stopPropagation bech thot rules 7ad page heki
watch(dialog, (val) => {
  val || close();
});

const addEnumVAl = () => {
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
          valeur: valeur.value,
          enumerationId: props.enumId,
        };
        const token = window.localStorage.getItem("token");

        if (token) {
          // If there is a token, set the authorization header
          axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
          //  console.log("Token checked:", axios.defaults.headers.common);
        } else {
          console.log("unauthorized");
          alert("unauthorized");
        }
        const response = await axios.post(
          "http://localhost:5252/api/enumerationvaleur",
          data
        );
        // await store.getApplications();
        if (response.status >= 200 && response.status < 300) {
          // router.push('/users/');
        } else {
          alert("erorrooor", response.message);
        }
        emit("dataChanged");
        loading.value = false;
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        close();
      }
    }, 1500);
  }
};
const close = () => {
  // Reset form fields and validation
  (valeur.value = ""), v$.value.$reset();
  dialog.value = false;
  loading.value = false;
};

onMounted(async () => {
  //console.log('enumid',props.enumId);
});
</script> 