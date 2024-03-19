<template>
  <v-dialog v-model="dialog" max-width="600px">
    <template v-slot:activator="{ props }">
      <v-btn color="primary" dark class="mb-2" v-bind="props">
        <v-icon color="green" style="margin: auto">mdi-plus</v-icon>
        {{ $t("new") }}
      </v-btn>
    </template>
    <v-card>
      <v-card-text>
        <v-container>
          <v-row class="" >
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                base-color="green"
                density="compact"
                variant="outlined"
                :label="$t('Social reason')"
                v-model="raisonSocial"
                @blur="v$.raisonSocial.touch"
                @input="v$.raisonSocial.$touch"
                :error-messages="v$.raisonSocial.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                base-color="green"
                density="compact"
                variant="outlined"
                :label="$t('identifier')"
                v-model="identifiant"
                @blur="v$.identifiant.touch"
                @input="v$.identifiant.$touch"
                :error-messages="v$.identifiant.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                base-color="green"
                density="compact"
                :label="$t('phone')"
                variant="outlined"
                type="number"
                v-model="telephone"
                @blur="v$.telephone.touch"
                @input="v$.telephone.$touch"
                :error-messages="v$.telephone.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                density="compact"
                variant="outlined"
                base-color="green"
                label="Email"
                v-model="email"
                @blur="v$.email.touch"
                @input="v$.email.$touch"
                :error-messages="v$.email.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                density="compact"
                variant="outlined"
                base-color="green"
                :label="$t('city')"
                v-model="ville"
                @blur="v$.ville.touch"
                @input="v$.ville.$touch"
                :error-messages="v$.ville.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                base-color="green"
                density="compact"
                variant="outlined"
                :label="$t('address')"
                v-model="adresse"
                @blur="v$.adresse.touch"
                @input="v$.adresse.$touch"
                :error-messages="v$.adresse.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                v-model="codePostal"
                :label="$t('Postal code')"
                density="compact"
                variant="outlined"
                base-color="green"
                @blur="v$.codePostal.touch"
                @input="v$.codePostal.$touch"
                :error-messages="v$.codePostal.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-select
                v-model="pays"
                :label="$t('country')"
                density="compact"
                base-color="green"
                variant="outlined"
                :items="countryNames"
                @blur="v$.pays.touch"
                @input="v$.pays.$touch"
                :error-messages="v$.pays.$errors.map((e) => e.$message)"
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
          @click="addClient"
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
import { ref, onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
const identifiant = ref("");
const raisonSocial = ref("");
const telephone = ref("");
const email = ref("");
const ville = ref("");
const adresse = ref("");
const codePostal = ref("");
const pays = ref("");
const dialog = ref(false);
const loading = ref(false);
const store = useMyStore();
const countryNames = ref([]);
const { withMessage } = helpers;
let { t } = useI18n();

const rules = {
  raisonSocial: {
    required: withMessage("Identifiant obligatoire", required),
  },
  identifiant: {},
  telephone: {
    numeric: withMessage(t("numberPhone", { field: t("numberPhone") }), (v) =>
      /^[0-9]+$/.test(v)
    ),
    required: withMessage("Telephone obligatoire", required),
  },
  email: {
    required: withMessage("E-mail obligatoire", required),
    emailRule: withMessage("email doit avoir  @ and .domain", (v) =>
      /.+@.+\..+/.test(v)
    ),
    InvalidEmail: withMessage("Email invalid", (v) =>
      /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(v)
    ),
  },
  ville: {
    required: withMessage("Ville obligatoire", required),
  },
  adresse: {
    required: withMessage("Adresse obligatoire", required),
  },
  codePostal: {
    required: withMessage("Identifiant obligatoire", required),
  },
  pays: {
    required: withMessage("Identifiant obligatoire", required),
  },
};
const v$ = useVuelidate(
  rules,
  {
    identifiant,
    raisonSocial,
    telephone,
    ville,
    codePostal,
    adresse,
    pays,
    email,
  },
  { $stopPropagation: true }
);
//stopPropagation bech thot rules 7ad page heki
watch(dialog, (val) => {
  val || close();
});
onMounted(async () => {
  getCountries();
});
const getCountries = async () => {
  try {
    const res = await axios.get(
      "https://countriesnow.space/api/v0.1/countries/info?returns=name"
    );
    // Extract country names from the response and update countryNames
    countryNames.value = res.data.data.map((country) => country.name);
    const contryordred = countryNames.value;
    contryordred.sort();
    console.log("Countries data:", countryNames.value);
  } catch (error) {
    console.error("Error fetching countries data:", error);
  }
};

const addClient = () => {
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
          raisonSocial: raisonSocial.value,
          identifiant: identifiant.value,
          telephone: telephone.value,
          email: email.value,
          ville: ville.value,
          adresse: adresse.value,
          codePostal: codePostal.value,
          pays: pays.value,
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
          "http://localhost:5252/api/client",
          data
        );
        await store.getClients();
        if (response.status >= 200 && response.status < 300) {
          // router.push('/users/');
        } else {
          alert("erorrooor", response.message);
        }
        await store.getClients();
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
  (identifiant.value = ""),
    (raisonSocial.value = ""),
    (telephone.value = ""),
    (email.value = ""),
    (ville.value = ""),
    (adresse.value = ""),
    (codePostal.value = ""),
    (pays.value = ""),
    v$.value.$reset();
  dialog.value = false;
  loading.value = false;
};
</script> 