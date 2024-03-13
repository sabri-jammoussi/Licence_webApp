<template>
  <v-navigation-drawer v-model="drawer">
    <v-list class="custom-list-margin">
      <div class="logo-container">
        <img src="/assets/logo.jpg" alt="Logo" />
      </div>
      <v-divider></v-divider>
      <v-list-item
        to="/"
        prepend-icon="mdi-view-dashboard"
        title="Accueil"
        value="home"
      >
        <v-tooltip activator="parent" location="end">Accueil</v-tooltip>
      </v-list-item>
      <v-list-item
        to="/"
        prepend-icon="mdi-view-dashboard"
        title="Client"
        value="home"
      >
        <v-tooltip activator="parent" location="end">Accueil</v-tooltip>
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
    <v-toolbar-title>My Users</v-toolbar-title>
    <v-row class="d-flex justify-end mr-2">
      <v-menu min-width="200px" rounded>
        <template v-slot:activator="{ props }">
          <v-btn
            icon="mdi-dots-vertical "
            v-bind="props"
            style="color: aliceblue"
          >
          </v-btn>
        </template>
        <v-card>
          <v-card-text>
            <div class="mx-auto text-center">
              <v-avatar color="brown" size="60">
                <span class="text">{{ userrole }}</span>
              </v-avatar>
              <h3>{{ userFirstName }} {{ userLastName }}</h3>
              <p class="text-caption mt-1">
                {{ userEmail }}
              </p>
              <v-divider class="my-3"></v-divider>
              <span @click="navigateTo('/updateUserProfile/CurrentUserPage')">
                <v-btn rounded variant="text"> Modifier le compte </v-btn>
              </span>
              <v-divider class="my-3"></v-divider>
              <div class="pt-2 pb-2 px-4 text-center">
                <v-btn
                  rounded
                  @click="logout"
                  variant="outlined"
                  class="rounded-pill"
                  block
                >
                  Déconnecter
                  <v-icon color="red"> mdi-logout</v-icon>
                </v-btn>
              </div>
            </div>
          </v-card-text>
        </v-card>
      </v-menu>
    </v-row>
  </v-app-bar>
  <v-main>
    <v-card-text>
      <Nuxt-Page />
    </v-card-text>
  </v-main>
  <v-footer
    style="
      background-color: rgb(220, 220, 220);
      color: #000;
      caret-color: #000;
      bottom: 0;
      z-index: 1006;
      transform: translateY(0%);
      position: fixed;
    "
    app
  >
    <span style="color: #16df17"
      >&copy; APBS {{ new Date().getFullYear() }}</span
    >
  </v-footer>
</template>
  
  <script setup>
import { ref, onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
import { useRouter } from "vue-router";

const isLoading = ref(true);
const drawer = ref(false);
const store = useMyStore();
const router = useRouter();
const userFirstName = computed(() => store.user?.firstName);
const userLastName = computed(() => store.user?.lastName);
const userEmail = computed(() => store.user?.email);
const userrole = computed(() => store.user?.role);

onMounted(async () => {
  await setTimeout(() => {
    isLoading.value = false;
  }, 1500);
  await store.loadTokenFromLocalStorage();
  console.log("roleeeeee", userrole.value);

  //console.log("user from store", store.user.idd);
  //console.log("storeeeee tokennnn ",store.token);
});
const logout = async () => {
  //console.log("logged out ",isloggedinn);
  await store.logoutUser({ router });
};
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
  