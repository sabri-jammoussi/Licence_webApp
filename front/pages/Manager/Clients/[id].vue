<template>
  <v-card>
    <v-card-text>
      <v-container>
        <v-spacer></v-spacer>
        <v-row no-gutters>
          <v-col cols="12" sm="6" md="12">
            <v-text-field
              base-color="green"
              :label="$t('raisonSocial')"
              :disabled="true"
              v-model="raisonSocial"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="12">
            <v-text-field
              base-color="green"
              :label="$t('identifier')"
              :disabled="true"
              v-model="identifiant"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="12">
            <v-text-field
              base-color="green"
              :label="$t('telephone')"
              :disabled="true"
              v-model="telephone"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="12">
            <v-text-field
              base-color="green"
              :label="$t('email')"
              :disabled="true"
              v-model="email"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="12">
            <v-text-field
              base-color="green"
              :label="$t('ville')"
              :disabled="true"
              v-model="ville"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="12">
            <v-text-field
              base-color="green"
              :label="$t('adresse')"
              :disabled="true"
              v-model="adresse"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="12">
            <v-text-field
              v-model="codePostal"
              :disabled="true"
              :label="$t('codePostal')"
              base-color="green"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="12">
            <v-text-field
              v-model="pays"
              :disabled="true"
              label="pays"
              base-color="green"
            ></v-text-field>
          </v-col>
        </v-row>
      </v-container>
    </v-card-text>
  </v-card>
</template>
  
  <script setup>
import { ref, onMounted } from "vue";
import axios from "axios";

const identifiant = ref("");
const adresse = ref("");
const raisonSocial = ref("");
const pays = ref("");
const ville = ref("");
const email = ref("");
const telephone = ref("");
const codePostal = ref("");

const route = useRoute();
onMounted(async () => {
  const id = route.params.id;
  getClientById(id);
  //console.log("iddddddddd", id);
});
const getClientById = (id) => {
  axios.get(`http://localhost:5252/api/client/${id}`).then((res) => {
    identifiant.value = res.data.identifiant;
    adresse.value = res.data.adresse;
    raisonSocial.value = res.data.raisonSocial;
    ville.value = res.data.ville;
    pays.value = res.data.pays;
    email.value = res.data.email;
    codePostal.value = res.data.codePostal;
    telephone.value = res.data.telephone;
  });
};
</script>
  
  <style>
</style>