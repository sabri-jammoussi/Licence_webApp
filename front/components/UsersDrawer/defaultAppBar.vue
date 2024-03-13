<template>
  <v-toolbar-title>{{ $t("myusers") }}</v-toolbar-title>
  <v-row class="d-flex justify-end mr-2">
    <v-menu min-width="200px" rounded>
      <template v-slot:activator="{ props }">
        <v-btn icon="mdi-translate" v-bind="props" style="color: aliceblue">
        </v-btn>
      </template>
      <v-card>
        <v-card-text>
          <div class="mx-auto text-center">
            <v-divider class="my-3"></v-divider>
            <span>
                <v-btn variant="text" @click="changeLocale('en')"  :class="{ 'selected': locale === 'en' }">English(US)</v-btn>

            </span>
            <v-divider class="my-3"></v-divider>
            <span>
              <v-btn variant="text" @click="changeLocale('fr')"
              :class="{ 'selected': locale === 'fr' }"
                >Français(FR)</v-btn
              >
            </span>
          </div>
        </v-card-text>
      </v-card>
    </v-menu>
  </v-row>

  <v-menu min-width="200px" rounded>
    <template v-slot:activator="{ props }">
      <v-btn icon="mdi-dots-vertical " v-bind="props" style="color: aliceblue">
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
            <v-btn rounded variant="text">{{ $t("Editaccount") }}</v-btn>
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
              {{ $t("Disconnect") }}
              <v-icon color="red"> mdi-logout</v-icon>
            </v-btn>
          </div>
        </div>
      </v-card-text>
    </v-card>
  </v-menu>
</template>
<script setup >
import { ref, onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
import { useRouter } from "vue-router";
import en from "../locales/en.json";
import fr from "../locales/fr.json";
const store = useMyStore();
const router = useRouter();
const userFirstName = computed(() => store.user?.firstName);
const userLastName = computed(() => store.user?.lastName);
const userEmail = computed(() => store.user?.email);
const userrole = computed(() => store.user?.role);
const { locale } = useI18n();
const locales = { en, fr };
const changeLocale = (newLocale) => {
  locale.value = newLocale;
};

onMounted(async () => {
  await store.loadTokenFromLocalStorage();
  console.log("roleeeeee", userrole.value);
});
const logout = async () => {
  await store.logoutUser({ router });
};
</script>
<style scoped>
.selected {
  background-color:#35D300; /* Example background color for selected button */
 
}
</style>y