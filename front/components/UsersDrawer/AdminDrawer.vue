<template>
  <v-navigation-drawer v-model="drawer">
    <v-list class="custom-list-margin">
      <div class="logo-container">
        <img src="/assets/logo.jpg" alt="Logo" />
      </div>

      <v-list-item
        to="/Admin/users/UserList"
        prepend-icon="mdi-account-outline"
        value="home"
      >
        {{ $t("Users") }}
        <!-- <v-tooltip activator="parent" location="end">{{
          $t("Users")
        }}</v-tooltip> -->
      </v-list-item>
      <v-list-item
        to="/Admin/Applications/ApplicationList"
        prepend-icon="mdi-view-dashboard-outline"
        title="Applications"
        value="home"
      >
        <!-- <v-tooltip activator="parent" location="end">Applications</v-tooltip> -->
      </v-list-item>
      <v-list-item
        to="/Admin/Enumeration/EnumerationList"
        prepend-icon="mdi-slack"
        value="home"
      >
        {{ $t("Enumerations") }}

        <!-- <v-tooltip activator="parent" location="end">Enumerations</v-tooltip> -->
      </v-list-item>

      <v-list-item to="/about" prepend-icon="mdi-forum-outline" value="home">
        {{ $t("About") }}
        <!-- <v-tooltip activator="parent" location="end">{{
          $t("About")
        }}</v-tooltip> -->
      </v-list-item>
    </v-list>
  </v-navigation-drawer>

  <v-app-bar
    style="
      background-color: #000;
      color: #fff;
      caret-color: #fff;
      top: 0;
      z-index: 1006;
      transform: translateY(0%);
      position: fixed;
      transition: none !important;
    "
  >
    <v-app-bar-nav-icon
      variant="text"
      @click.stop="drawer = !drawer"
    ></v-app-bar-nav-icon>
    <UsersDrawerDefaultAppBar />
  </v-app-bar>

  <v-main>
    <v-card-text>
      <NuxtPage />
    </v-card-text>
  </v-main>
  <Footer />
</template>
<script setup>
import { ref } from "vue";
const drawer = ref(false);
</script>
<style scoped>
.custom-list-margin {
  margin-top: -8px;
  width: 100%;
}
.logo-container {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 64px;
  width: 255px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  background-color: #000000;
}
.logo-container img {
  max-height: 100%;
  max-width: 100%;
  object-fit: contain;
}
</style>