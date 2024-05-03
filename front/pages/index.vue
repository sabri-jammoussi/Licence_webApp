<template>
  <v-container fluid>
    <v-row>
      <v-col cols="12" md="12" v-if="isAdmin">
        <CardSummaryAdmin />
        <v-row>
          <v-col cols="12" lg="6">
            <v-card height="100%" class="card">
              <v-card-title>Sales by brand</v-card-title>
              <v-card-text>
                <p>The sales summary is done by our company on salesperson</p>

                <LineChart
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

                <BarChart
                  :chart-data="barChartData"
                  :chart-options="barChartOptions"
                  
                />
              </v-card-text>
            </v-card>
          </v-col>
        </v-row>
      </v-col>
      <v-col cols="12" sm="12" lg="12" v-else-if="isManager">
        <v-container fluid>
          <v-row>
            <v-col cols="12" md="12">
              <CardSummaryManager />
            </v-col>
          </v-row>
        </v-container>
      </v-col>
      <!-- <v-col cols="12" sm="12" lg="12" v-else> aaaa ghf</v-col> -->
      <!-- <v-col cols="12" sm="12" lg="4">
          <div class="mb-6">
               <TrafficDistribution/> 
          </div>
          <div>
               <ProductSales/> 
          </div>
      </v-col>
      <v-col cols="12" sm="12" lg="4">
           <UpcommingSchedule/> 
      </v-col>
      <v-col cols="12" sm="12" lg="8" v-if="isManager">
          <TopPayingClients/>
      </v-col>
      <v-col cols="12" sm="12" lg="12">
         <ProductCards/> 
      </v-col> -->
    </v-row>
  </v-container>

  <!-- 
  <div>
    <canvas ref="chart" width="400" height="400"></canvas>
  </div> -->
</template>

<script setup>
import { ref, computed } from "vue";
// import { Line } from "vue-chartjs";
import { useMyStore } from "@/store/index.js";
import { onMounted } from "vue";
import CardSummaryAdmin from "@/components/Card/SummaryAdmin.vue";
import CardSummaryManager from "@/components/Card/SummaryManager.vue";
import LineChart from "~/components/Charts/LineChart.vue";

import BarChart from "~/components/Charts/BarChart.vue";
const store = useMyStore();
const userrole = computed(() => store.user?.role);
const isLoading = ref(true);
const isAdmin = computed(() => userrole.value === "Admin");
const isManager = computed(() => userrole.value === "Manager");
const barChartData = ref({
  labels: ["January", "February", "March", "April", "May", "June", "July"],
  datasets: [
    {
      label: "Sales by Brand",
      data: [200, 180, 220, 250, 200, 190, 210], // Example data, you should replace it with your actual data
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
    },
  ],
});

const chartData = ref({
  labels: ["January", "February", "March", "April", "May", "June", "July"],
  datasets: [
    {
      label: "My First Dataset",
      data: [165, 59, 80, 181, 56, 55, 100],
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
});
const chartOptions = ref({
  responsive: true,
  maintainAspectRatio: false,
});

onMounted(async () => {
  await setTimeout(() => {
    isLoading.value = false;
  }, 1500);
  await store.loadTokenFromLocalStorage();
});
</script>
