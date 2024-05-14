<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" md="12" v-if="isAdmin">
        <!-- <CardSummaryAdmin /> -->
      </v-col>
      <v-col cols="12" sm="12" lg="12" v-else-if="isManager">
        <v-container fluid>
          <v-row>
            <v-col cols="12" md="12">
              <CardSummaryManager />
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12" lg="6">
              <v-card height="100%" class="card">
                <v-card-title>{{ $t("licencesNombre") }}</v-card-title>
                <v-card-text>
                  <p>
                    {{ $t("licencePerMonth") }}
                  </p>

                  <LineChart
                    v-if="dataLicencesByMonths"
                    :chart-data="chartData"
                    :chart-options="chartOptions"
                    style="height: 280px"
                  />
                </v-card-text>
              </v-card>
            </v-col>
            <v-col cols="12" lg="6">
              <v-card height="100%" light class="card">
                <v-card-title>Sales by brand</v-card-title>
                <v-card-text>
                  <p>The sales summary is done by our company on salesperso</p>

                  <BarChart v-if="dataLicencesByYear" :chart-data="barChartData" style="width: 100%" />
                </v-card-text>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </v-col>
    </v-row>
  </v-container>
</template>
<script setup>
import { ref, computed } from "vue";
import { useMyStore } from "@/store/index.js";
import { onMounted } from "vue";
import CardSummaryManager from "@/components/Card/SummaryManager.vue";
import LineChart from "~/components/Charts/LineChart.vue";
import axios from "axios";
import BarChart from "~/components/Charts/BarChart.vue";
import { format } from "date-fns";
const dataLicencesByMonths = ref();
const dataLicencesByYear = ref();
let { t } = useI18n();

const store = useMyStore();
const userrole = computed(() => store.user?.role);
const isAdmin = computed(() => userrole.value === "Admin");
const isManager = computed(() => userrole.value === "Manager");

const labels = ref();
const labelsBar = ref();
const getLicences = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/licence");
    const dataLicences = response.data;

    // Initialize an object to store the counts for each month
    const licenceCountsByMonth = [];
    const licenceCountsByYear = {};
    dataLicences.map((items) => {
      const formattedYear = format(new Date(items.dateExp), "yyyy");
      if (!licenceCountsByYear[formattedYear]) {
        licenceCountsByYear[formattedYear] = 0;
      }
      licenceCountsByYear[formattedYear]++;
    });
    // Loop through dataLicences to count licences for each month
    dataLicences.map((item) => {
      const formattedMonth = format(new Date(item.dateExp), "MMMM");
      if (!licenceCountsByMonth[formattedMonth]) {
        licenceCountsByMonth[formattedMonth] = 0;
      }
      licenceCountsByMonth[formattedMonth]++;
    });

    // Update chartData with the counts
    dataLicencesByYear.value = Object.values(licenceCountsByYear);

    dataLicencesByMonths.value = Object.values(licenceCountsByMonth);
    // Update labels if necessary
    labels.value = Object.keys(licenceCountsByMonth);
    labelsBar.value = Object.keys(licenceCountsByYear);
    console.log("Licence counts by month:", licenceCountsByYear);
    // console.log("Licence counts by month:", licenceCountsByMonth);
    console.log("Licence  month:", labelsBar.value);
  } catch (error) {
    console.error(error);
  }
};
const chartData = ref({
  datasets: [
    {
      label: t("licencesNombre"),
      data: dataLicencesByMonths,
      backgroundColor: [
        "rgba(255, 99, 132, 0.2)",
        "rgba(255, 159, 64, 0.2)",
        "rgba(255, 205, 86, 0.2)",
        "rgba(75, 192, 192, 0.2)",
        "rgba(54, 162, 235, 0.2)",
        "rgba(153, 102, 255, 0.2)",
        "rgba(201, 203, 207, 0.2)",
      ],
      borderColor: [
        "rgb(255, 99, 132)",
        "rgb(255, 159, 64)",
        "rgb(255, 205, 86)",
        "rgb(75, 192, 192)",
        "rgb(54, 162, 235)",
        "rgb(153, 102, 255)",
        "rgb(201, 203, 207)",
      ],
      borderWidth: 1,
      fill: false,
    },
  ],
  labels: labels,
});
const chartOptions = ref({
  responsive: true,
  maintainAspectRatio: false,
  scales: {
    y: {
      beginAtZero: true,
      ticks: {
        stepSize: 1,
      },
    },
  },
});
const barChartData = ref({
  datasets: [
    {
      label: "Sales by Brand",
      data: dataLicencesByYear, // Example data, you should replace it with your actual data
      backgroundColor: [
        "rgba(255, 99, 132, 0.2)",
        "rgba(255, 159, 64, 0.2)",
        "rgba(255, 205, 86, 0.2)",
        "rgba(75, 192, 192, 0.2)",
        "rgba(54, 162, 235, 0.2)",
        "rgba(153, 102, 255, 0.2)",
        "rgba(201, 203, 207, 0.2)",
        "rgba(255, 51, 153, 0.2)",
      ],
      borderColor: [
        "rgb(255, 99, 132)",
        "rgb(255, 159, 64)",
        "rgb(255, 205, 86)",
        "rgb(75, 192, 192)",
        "rgb(54, 162, 235)",
        "rgb(153, 102, 255)",
        "rgb(201, 203, 207)",
      ],
      borderWidth: 1,
    },
  ],
  labels: labelsBar,
});

onMounted(async () => {
  await getLicences();
  //console.log("Data loaded", dataLicencesByMonths.value);

  //console.log("labels ", labels.value);
  await store.loadTokenFromLocalStorage();
});
</script>
