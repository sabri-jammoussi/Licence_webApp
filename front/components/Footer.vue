<template>
  <v-footer
    style="
      background-color: rgb(255, 255, 255);
      color: #000;
      caret-color: #000;
      bottom: 0;
      z-index: 1006;
      transform: translateY(0%);
      position: fixed;
    "
    app
  >
    <div class="bg-">
      <img
        src="/assets/Logoo2.png"
        alt="Logo"
        style="height: 30px; width: 150px"
      />
    </div>
    <div class="text-right w-100">
      <span style="color: #16df17"
        >AP Licence copyright &copy; {{ new Date().getFullYear() }}</span
      >
    </div>
  </v-footer>
</template>
<style scoped>
</style>