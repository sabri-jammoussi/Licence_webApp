<template>
  <div class="container">
    <div class="row">
      <div class="col-sm-6">
        <v-card class="mx-left" height="100%" elevation="">
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
                </v-col>
              </v-row>
            </v-container>
          </v-card-text>
        </v-card>
      </div>
      <div class="col-sm-6" v-if="isAdmin">
        <AttributeList v-if="id" :attributeId="id" />
      </div>
      <div class="col-sm-6" v-if="isManager">
        <AttributeListManager v-if="id" :attributeId="id" />
      </div>
      <v-container fluid>
        <v-row>
          <v-col cols="12" md="12" v-if="isManager">
            <SummaryAttribute />
          </v-col>
        </v-row>
      </v-container>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import AttributeList from "./Attributes/AttributeList.vue";
import AttributeListManager from "./Attributes/AttributeListManager.vue";
import SummaryAttribute from "@/components/Card/SummaryAttributeListManager.vue";
import { useMyStore } from "@/store/index.js";

const identifiant = ref("");
const nom = ref("");
const description = ref("");
const id = ref("");
const route = useRoute();
const store = useMyStore();

const userrole = computed(() => store.user?.role);
const isAdmin = computed(() => userrole.value === "Admin");
const isManager = computed(() => userrole.value === "Manager");

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