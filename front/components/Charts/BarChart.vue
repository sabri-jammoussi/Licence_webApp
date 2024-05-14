<!-- components/Chart.vue -->
<template>
    <div>
      <canvas ref="chartCanvas2"></canvas>
    </div>
  </template>
  
  <script>
  import { ref, onMounted } from 'vue';
  import Chart from 'chart.js/auto';
  
  export default {
    name: 'Chart',
    props: {
      chartData: {
        type: Object,
        required: true,
      },
      chartOptions: {
        type: Object,
        default: () => ({}),
      },
    },
    setup(props) {
      const chartCanvas2 = ref(null);
      let chartInstance = null;
  
      onMounted(() => {
       // console.log("dsdjdklsjdklzhfkjhdfjkhsdj", props.chartData);
        if (chartCanvas2.value) {

          chartInstance = new Chart(chartCanvas2.value, {
            type: 'bar', // Change this to the chart type you want (e.g., line, pie, etc.)
            data: props.chartData,
            options: props.chartOptions,
          });
        }
      });
  
      return {
        chartCanvas2,
        chartInstance,
      };
    },
  };
  </script>
  
  <style scoped>
  /* Add custom styles if needed */
  </style>
  