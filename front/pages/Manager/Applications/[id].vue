<template>
  <v-card class="mx-left" max-width="425">
    <v-card-text>
      <v-container>
        <v-spacer></v-spacer>
        <v-row no-gutters>
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
              v-model="nom"
              :disabled="true"
              :label="$t('name')"
              base-color="green"
            ></v-text-field>
          </v-col>
          <v-col cols="12" sm="6" md="12">
            <v-text-field
              v-model="description"
              :disabled="true"
              label="Description"
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
const nom = ref("");
const description = ref("");
const route = useRoute();
onMounted(async () => {
  const id = route.params.id;
  getApplicationsById(id);
  //console.log("iddddddddd", id);
});
const getApplicationsById = (id) => {
  axios.get(`http://localhost:5252/api/appliction/${id}`).then((res) => {
    identifiant.value = res.data.identifiant;
    nom.value = res.data.nom;
    description.value = res.data.description;
  });
};
</script>

<style>
</style>