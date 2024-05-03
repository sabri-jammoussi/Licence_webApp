<!-- components/Chart.vue -->
<template>
    <div>
      <canvas ref="chartCanvas"></canvas>
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
      const chartCanvas = ref(null);
      let chartInstance = null;
  
      onMounted(() => {
        if (chartCanvas.value) {
          chartInstance = new Chart(chartCanvas.value, {
            type: 'bar', // Change this to the chart type you want (e.g., line, pie, etc.)
            data: props.chartData,
            options: props.chartOptions,
          });
        }
      });
  
      return {
        chartCanvas,
        chartInstance,
      };
    },
  };
  </script>
  
  <style scoped>
  /* Add custom styles if needed */
  </style>
  