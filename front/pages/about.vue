<template>
  <v-container>
    <p style="color: #16df17" class="text-uppercase , font-weight-bold">
      AP Licence
    </p>
    <p class="font-weight-bold">Version : v1.0.0-beta</p>
    <p class="font-weight-bold">Date version : 2024-05-13</p>

    <v-row class="text-left">
      <v-col col="12" md="12">
        <v-row>
          <v-col cols="12" md="8">
            <v-card height="100%" class="card">
              <v-card-title tag="h2">Détail Application </v-card-title>
              <v-card-subtitle>Détail globale</v-card-subtitle>
              <v-card-text>
                <div>
                  <v-row class="my-1">
                    <v-col tag="section">
                      <p>
                        Nombre des clients :
                        <span style="color: #16df17"
                          >{{ countedClients }} / 20</span
                        >
                      </p>
                    </v-col>
                  </v-row>

                  <v-row class="my-1">
                    <v-col tag="section">
                      <p>
                        Nombre des applications :
                        <span style="color: #16df17"
                          >{{ countedApplications }} / 50</span
                        >
                      </p>
                    </v-col>
                  </v-row>

                  <v-row class="my-1">
                    <v-col tag="section">
                      <p>
                        Nombre des licences :
                        <span style="color: #16df17"
                          >{{ countedLicences }} / 50</span
                        >
                      </p>
                    </v-col>
                  </v-row>
                  <v-row class="my-1">
                    <v-col tag="section">
                      <p>
                        Nombre des partenaires :
                        <span style="color: #16df17"
                          >{{ countedPartenaires }} / /20</span
                        >
                      </p>
                    </v-col>
                  </v-row>
                </div>
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
      </v-col>
    </v-row>
  </v-container>
</template>
<script setup>
import axios from "axios";
const dataClients = ref([]);
const dataApplications = ref([]);
const dataLicences = ref([]);
const dataPartenaires = ref([]);

const countedClients = computed(() => dataClients.value.length);
const countedApplications = computed(() => dataApplications.value.length);
const countedLicences = computed(() => dataLicences.value.length);
const countedPartenaires = computed(() => dataPartenaires.value.length);
onMounted(async () => {
  await getClients();
  await getApplications();
  await getLicences();
  await getPartenaire();
  //console.log("ddddd", ActifLicences.value);
});
const getClients = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/client");

    dataClients.value = response.data;
    //console.log(dataClients);
  } catch (error) {
    console.error(error);
  }
};
const getApplications = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/appliction");

    dataApplications.value = response.data;
    // console.log(data);
  } catch (error) {
    console.error(error);
  }
};
const getLicences = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/licence");
    dataLicences.value = response.data;
  } catch (error) {
    console.error(error);
  }
};
const getPartenaire = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/partenaire");
    dataPartenaires.value = response.data;
  } catch (error) {
    console.error(error);
  }
};
</script>
