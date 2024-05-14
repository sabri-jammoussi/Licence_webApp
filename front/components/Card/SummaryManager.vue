<template>
  <v-row>
    <v-col cols="6" sm="4">
      <nuxt-link to="/Manager/Clients/ClientList" class="no-link-style">
        <v-card height="100%" class="card">
          <v-container fluid>
            <h4>Clients</h4>
            <v-row height="100%">
              <v-col>
                <h4
                  class="grey--text text-h4 text-lg-h4 font-weight-bold lh-normal"
                >
                  {{ countedClients }}
                </h4>
                <h6 class="font-weight-normal grey--text">+23% Up</h6>
              </v-col>
              <v-col align-self="center" align="center">
                <v-icon size="50" color="green">mdi-account-outline</v-icon>
              </v-col>
            </v-row>
          </v-container>
        </v-card>
      </nuxt-link>
    </v-col>

    <v-col cols="6" sm="4">
      <nuxt-link to="/Manager/Licences/LicenceList" class="no-link-style">
        <v-card height="100%" class="card">
          <v-container fluid>
            <h4>{{ $t("licenses") }}</h4>
            <v-row height="100%">
              <v-col>
                <h4
                  class="grey--text text-h4 text-lg-h5 font-weight-bold lh-normal"
                >
                  {{ countedLicences }}
                </h4>
                <h6 class="font-weight-normal grey--text">
                  ({{ ActifLicences }} Actif)
                </h6>
              </v-col>
              <v-col align-self="center" align="center">
                <v-icon size="50" color="#1E74FF">mdi-key-outline</v-icon>
              </v-col>
            </v-row>
          </v-container>
        </v-card>
      </nuxt-link>
    </v-col>
    <v-col cols="6" sm="4">
      <nuxt-link to="/Manager/Partenaires/PartenaireList" class="no-link-style">
        <v-card height="100%" class="card">
          <v-container fluid>
            <h4>{{ $t("partner") }}</h4>
            <v-row height="100%">
              <v-col>
                <h4
                  class="grey--text text-h4 text-lg-h4 font-weight-bold lh-normal"
                >
                  {{ countedPartenaires }}
                </h4>
                <h6 class="font-weight-normal grey--text">+5% Up</h6>
              </v-col>
              <v-col align-self="center" align="center">
                <v-icon size="50" color="#26A6AA">mdi-handshake-outline</v-icon>
              </v-col>
            </v-row>
          </v-container>
        </v-card>
      </nuxt-link>
    </v-col>
    <v-col cols="6" sm="4">
      <nuxt-link to="/Admin/Applications/ApplicationList" class="no-link-style">
        <v-card height="100%" class="card">
          <v-container fluid>
            <h4>Applications</h4>
            <v-row height="100%">
              <v-col>
                <h4
                  class="grey--text text-h4 text-lg-h4 font-weight-bold lh-normal"
                >
                  {{ countedApplications }}
                </h4>
                <h6 class="font-weight-normal grey--text">-3% Down</h6>
              </v-col>
              <v-col align-self="center" align="center">
                <v-icon size="50" color="#FFD065"
                  >mdi-view-dashboard-outline</v-icon
                >
              </v-col>
            </v-row>
          </v-container>
        </v-card>
      </nuxt-link>
    </v-col>
    <v-col cols="6" sm="4">
      <v-card height="100%" class="card">
        <v-container fluid>
          <h4>Licences</h4>
          <v-row height="100%">
            <v-col>
              <h4
                class="grey--text text-h4 text-lg-h4 font-weight-bold lh-normal"
              >
                {{ LicencesExpiredInWeek }}
              </h4>
              <h6>
                {{ $t("licenceWillExpire") }}
              </h6>
            </v-col>
            <v-col align-self="center" align="center">
              <v-icon size="50" color="red">mdi-key-remove</v-icon>
            </v-col>
          </v-row>
        </v-container>
      </v-card>
    </v-col>
    <v-col cols="6" sm="4">
      <v-card height="100%" class="card">
        <v-container fluid>
          <h4>Partenaires</h4>
          <v-row height="100%">
            <v-col>
              <h4
                class="grey--text text-h4 text-lg-h4 font-weight-bold lh-normal"
              >
                {{ NumbreLicenceByPartenaire }}
              </h4>
              <h6 class="font-weight-normal grey--text">
                {{ $t("partnaireWithLicence") }}
              </h6>
            </v-col>
            <v-col align-self="center" align="center">
              <v-icon size="50" color="orange">mdi-handshake-outline</v-icon>
            </v-col>
          </v-row>
        </v-container>
      </v-card>
    </v-col>
  </v-row>
</template>
  <script setup>
import axios from "axios";
const dataClients = ref([]);
const dataApplications = ref([]);
const dataLicences = ref([]);
const dataPartenaires = ref([]);
const date = ref([]);
const today = new Date();
const ActifLicences = computed(() => {
  let activeLicenses = 0;
  dataLicences.value.forEach((license) => {
    const expirationDate = new Date(license.dateExp);
    if (expirationDate > today) {
      activeLicenses++;
    }
  });
  return activeLicenses;
});
const NumbreLicenceByPartenaire = computed(() => {
  let partenaireWithLicence = 0;
  dataLicences.value.forEach((licence) => {
    const partenaireIds = licence.partenaireId;
    if (partenaireIds !== null) {
      partenaireWithLicence++;
    }
  });
  return partenaireWithLicence;
});
const LicencesExpiredInWeek = computed(() => {
  const oneWeekFromNow = new Date();
  oneWeekFromNow.setDate(oneWeekFromNow.getDate() + 7); // Add 7 days to today's date

  let LicencesExpired = 0;
  dataLicences.value.forEach((license) => {
    const expirationDate = new Date(license.dateExp);
    if (expirationDate > today && expirationDate <= oneWeekFromNow) {
      LicencesExpired++;
    }
  });
  return LicencesExpired;
});
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
   // console.log("dataaaaaa", dataLicences.value);
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
<style>
.no-link-style {
  text-decoration: none;
  color: inherit;
}
</style>