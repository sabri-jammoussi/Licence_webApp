<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" md="8" lg="6">
        <v-card>
          <div class="grey--text text-h6 text-lg-h6 mt-2">
            <v-icon left color="green" size="35" class="ml-2"
              >mdi-account
            </v-icon>
            {{ $t("consultingClient") }}
          </div>
          <v-divider></v-divider>
          <v-sheet class="mx-auto">
            <v-card-text>
              <v-text-field
                base-color="green"
                :label="$t('raisonSocial')"
                readonly
                variant="outlined"
                v-model="raisonSocial"
              ></v-text-field>
              <v-text-field
                base-color="green"
                :label="$t('identifier')"
                readonly
                variant="outlined"
                v-model="identifiant"
              ></v-text-field>
              <v-text-field
                base-color="green"
                :label="$t('telephone')"
                readonly
                variant="outlined"
                v-model="telephone"
              ></v-text-field>

              <v-text-field
                base-color="green"
                :label="$t('email')"
                readonly
                variant="outlined"
                v-model="email"
              ></v-text-field>
              <v-text-field
                base-color="green"
                :label="$t('ville')"
                readonly
                variant="outlined"
                v-model="ville"
              ></v-text-field>

              <v-text-field
                base-color="green"
                :label="$t('adresse')"
                readonly
                variant="outlined"
                v-model="adresse"
              ></v-text-field>

              <v-text-field
                v-model="codePostal"
                readonly
                :label="$t('codePostal')"
                base-color="green"
                variant="outlined"
              ></v-text-field>

              <v-text-field
                v-model="pays"
                readonly
                variant="outlined"
                label="pays"
                base-color="green"
              ></v-text-field>
            </v-card-text>
            <v-divider class="my-1"></v-divider>
            <v-card-actions>
              <v-spacer></v-spacer>

              <Nuxt-link to="/Manager/Clients/ClientList">
                <v-btn type="submit" color="grey">{{ $t("cancel") }}</v-btn>
              </Nuxt-link>
            </v-card-actions>
          </v-sheet>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
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
  console.log("iddddddddd", id);
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