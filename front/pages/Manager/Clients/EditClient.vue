<template>
  <v-dialog v-model="editDialog" max-width="500px">
    <v-card>
      <v-card-text>
        <v-container>
          <v-spacer></v-spacer>
          <v-row>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                variant="outlined"
                base-color="green"
                :label="$t('Social reason')"
                v-model="raisonSocial"
                @blur="v$.raisonSocial.touch"
                @input="v$.raisonSocial.$touch"
                :error-messages="v$.raisonSocial.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
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
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                variant="outlined"
                base-color="green"
                :label="$t('phone')"
                type="number"
                v-model="telephone"
                @blur="v$.telephone.touch"
                @input="v$.telephone.$touch"
                :error-messages="v$.telephone.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
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
                variant="outlined"
                base-color="green"
                :label="$t('address')"
                v-model="adresse"
                @blur="v$.adresse.touch"
                @input="v$.adresse.$touch"
                :error-messages="v$.adresse.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                variant="outlined"
                v-model="codePostal"
                :label="$t('Postal code')"
                base-color="green"
                @blur="v$.codePostal.touch"
                @input="v$.codePostal.$touch"
                :error-messages="v$.codePostal.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-select
                variant="outlined"
                v-model="pays"
                :label="$t('country')"
                base-color="green"
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
          color="blue-darken-1"
          variant="text"
          @click="updateClient"
          :loading="loading"
        >
          {{ $t("edit") }}
        </v-btn>
        <v-btn color="grey" variant="text" @click="close">
          {{ $t("cancel") }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
    <script setup>
import { ref, onMounted } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import axios from "axios";

const { emit } = getCurrentInstance();
const { withMessage } = helpers;
const editDialog = ref(false);

const loading = ref(false);
const identifiant = ref("");
const raisonSocial = ref("");
const telephone = ref("");
const email = ref("");
const ville = ref("");
const adresse = ref("");
const codePostal = ref("");
const pays = ref("");
const countryNames = ref([]);


const id = ref("");
const props = defineProps(["user"]);

const rules = {
  raisonSocial: {
    required: withMessage("Identifiant obligatoire", required),
  },
  identifiant: {},
  telephone: {
    required: withMessage("Identifiant obligatoire", required),
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
    required: withMessage("Identifiant obligatoire", required),
  },
  adresse: {
    required: withMessage("Identifiant obligatoire", required),
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
watch(editDialog, (val) => {
  val || close();
});

const getCountries = async () => {
  try {
    const res = await axios.get(
      "https://countriesnow.space/api/v0.1/countries/info?returns=name"
    );
    // Extract country names from the response and update countryNames
    countryNames.value = res.data.data.map((country) => country.name);
    console.log("Countries data:", countryNames.value);
  } catch (error) {
    console.error("Error fetching countries data:", error);
  }
};
const updateClient = async () => {
  loading.value = true;
  v$.value.$touch();
  if (v$.value.$pending || v$.value.$error) {
    loading.value = false;
  } else {
    setTimeout(async () => {
      try {
        const userId = id.value;
        console.log("idddddd", userId);
        const data = {
          id: userId,
          raisonSocial: raisonSocial.value,
          identifiant: identifiant.value,
          telephone: telephone.value,
          email: email.value,
          ville: ville.value,
          adresse: adresse.value,
          codePostal: codePostal.value,
          pays: pays.value,
        };
        const response = await axios.post(
          `http://localhost:5252/api/client/update`,
          data
        );
        if (response.status >= 200 && response.status < 300) {
          // router.push('/users/');
        } else {
          alert("erorrooor", response.message);
        }
        loading.value = false;
        emit("dataChanged");

      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        close();
      }
    }, 1500);
  }
};
onMounted(async () => {
  //console.log("edit dialog",props)
  getCountries();

  if (props.user) {
    raisonSocial.value = props.user.raisonSocial;
    identifiant.value = props.user.identifiant;
    (telephone.value = props.user.telephone),
      (email.value = props.user.email),
      (ville.value = props.user.ville),
      (adresse.value = props.user.adresse),
      (codePostal.value = props.user.codePostal),
      (pays.value = props.user.pays),
      (id.value = props.user.id);
    editDialog.value = true;
    console.log("props.user.id", props.user.id);
  }
  await store.ReadRoles();
});
const close = () => {
  v$.value.$reset();
  editDialog.value = false;
  emit("close-dialog");
};
</script>