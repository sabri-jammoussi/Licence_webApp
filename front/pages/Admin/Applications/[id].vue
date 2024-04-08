<template>
  <div class="container">
    <div class="row">
      <div class="col-sm-6">
        <v-card class="mx-left" height="100%" elevation="4">
          <v-card-text>
            <v-container>
              <v-spacer></v-spacer>
              <v-row no-gutters>
                <v-col cols="12">
                  <div>
                    <strong>{{ $t("identifier") }}:</strong> {{ identifiant }}
                  </div>
                  <v-divider></v-divider>
                  <div>
                    <strong>{{ $t("name") }}:</strong> {{ nom }}
                  </div>
                  <v-divider></v-divider>

                  <div><strong>Description:</strong> {{ description }}</div>
                  <v-divider></v-divider>
                  <strong class="text-h6">{{ $t('listATT') }}</strong>
                  <AttributeList v-if="id" :attributeId="id" />
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
        </v-card>
      </div>
      <div class="col-sm-6">
        <AddAttribute  :emitId="id"/>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";

import AddAttribute from "./Attributes/AddAttribute.vue";
import AttributeList from "./Attributes/AttributeList.vue";
const intutile = ref("");
const identifiant = ref("");
const nom = ref("");
const description = ref("");
const id = ref("");
const route = useRoute();
onMounted(async () => {
  id.value = route.params.id;
  getApplicationsById(id.value);
  console.log("iddddddddd  from [id]", id.value);
});
const getApplicationsById = async (id) => {
  const res = await axios.get(`http://localhost:5252/api/appliction/${id}`);
  //console.log("hello", res.data);
  description.value = res.data.description;
  identifiant.value = res.data.identifiant;
  nom.value = res.data.nom;
};
</script>

<style>
</style>