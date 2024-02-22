<template>
  <v-container>
    <v-row justify="space-around">
      <v-card width="1000" title="Ajouter Utilisateur">
        <v-sheet width="900" height="560" class="mx-auto">
          <div v-if="isLoading" class="text-center">
            <loading :title="isLoadingTitle" />
          </div>
          <div v-else>
            <form @submit.prevent="addUtilisateur">
              <!-- <v-row>
                <v-col>
                  <v-alert type="error" v-for="(eror, index) in errorList.value" :key="index">
                    {{ errorList.value }}
                  </v-alert>
                </v-col>
              </v-row> -->
              <v-text-field v-model="users.firstName" label="Nom"></v-text-field>
              <v-text-field v-model="users.lastName" label="Prenom"></v-text-field>
              <v-text-field v-model="users.email" label="Email"></v-text-field>
              <v-text-field v-model="users.password" label="password"></v-text-field>
              <v-text-field v-model="users.role" label="role"></v-text-field>

              <v-row justify="flex-end">
                <v-btn type="submit" class="button-85 green-btn">Ajouter</v-btn>
                <Nuxt-link to="/users/">
                  <v-btn type="submit" class="button-85 red-btn">Annuler</v-btn>
                </Nuxt-link>
              </v-row>
            </form>
          </div>
        </v-sheet>
      </v-card>
    </v-row>
  </v-container>
</template>

<script setup>
import Loading from "~/components/Loading.vue";
import axios from "axios";
import { ref, onMounted } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();

const users = ref({
  firstName: "",
  lastName: "",
  email: "",
  password: "",
  role: "",
});

const isLoading = ref(false);
const isLoadingTitle = ref("Loading");
const errorList = ref([]);

const addUtilisateur = () => {
  isLoading.value = true;
  isLoadingTitle.value = "Saving";
  const token = window.localStorage.getItem('token');
  console.log("tokeeeeeeeeeeen",token);
  if (token) {
    axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
    console.log('tokenCheked',axios.defaults.headers.common);
  }

  axios
    .post("http://localhost:5252/api/Users", users.value)
    .then((result) => {
      console.log("resultat", result);
      users.value.firstName = "";
      users.value.lastName = "";
      users.value.email = "";
      users.value.password = "";
      users.value.role = "";
      isLoading.value = false;
      isLoadingTitle.value = "Loading";

      router.push('/users/');
    })
    .catch((error) => {
      console.error("Error fetching data:", error);
      if (error.response) {
        if (error.response.status == 400) {
          errorList.value = error.response.data;
          console.log('aaaaaaaaaaa', errorList.value);
          isLoading.value = false;
        }
      }
      isLoading.value = false;
    });
};
</script>

<style>
  /* Your styles go here */
</style>
