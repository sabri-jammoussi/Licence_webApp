<template>
    <v-dialog v-model="dialog" max-width="500px">
      <template v-slot:activator="{ props }">
        <v-btn color="primary" dark class="mb-2" v-bind="props">
          <v-icon color="green" style="margin: auto">mdi-plus</v-icon>
          {{ $t("new") }}
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
                  label="Code"
                  v-model="code"
                  @blur="v$.code.touch"
                  @input="v$.code.$touch"
                  :error-messages="v$.code.$errors.map((e) => e.$message)"
                ></v-text-field>
              </v-col>
              <v-col cols="12" sm="4" md="12">
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
            </v-row>
          </v-container>
        </v-card-text>
        <v-divider class="my-2"></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn
            color="green"
            variant="text"
            @click="addEnumeration"
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
  import axios from "axios";
  import { ref } from "vue";
  import { useMyStore } from "@/store/index.js";
  import { useVuelidate } from "@vuelidate/core";
  import { required, helpers, minLength } from "@vuelidate/validators";
  const code = ref("");
  const nom = ref("");
  const dialog = ref(false);
  const loading = ref(false);
  const store = useMyStore();
  const { withMessage } = helpers;
  
  const rules = {
    code: {
      required: withMessage("code obligatoire", required),
      min: withMessage("Min 13 caractères", minLength(3)),
    },
    nom: {
      required: withMessage("Nom obligatoire", required),
      min: withMessage("Min 3 caractères", minLength(3)),
    },
  };
  const v$ = useVuelidate(
    rules,
    {
      code,
      nom,
  
    },
    { $stopPropagation: true }
  );
  //stopPropagation bech thot rules 7ad page heki
  watch(dialog, (val) => {
    val || close();
  });
  
  const addEnumeration = () => {
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
            code: code.value,
            nom: nom.value,
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
          const response = await axios.post("http://localhost:5252/api/enumeration", data);
         // await store.getApplications();
          if (response.status >= 200 && response.status < 300) {
            // router.push('/users/');
          } else {
            alert("erorrooor", response.message);
          }
          await store.getEnumerations();
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
    (code.value = ""),
      (nom.value = ""),
      v$.value.$reset();
    dialog.value = false;
    loading.value = false;
  };
  </script> 