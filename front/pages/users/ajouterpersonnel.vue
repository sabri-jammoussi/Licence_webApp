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
                  <v-alert type="error" v-for="(eror, index) in errorList" :key="index">
                    {{ eror }}
                  </v-alert>
                </v-col>
              </v-row> -->
              <v-text-field v-model="firstName" label="Nom"></v-text-field>
              <v-text-field v-model="lastName" label="Prenom"></v-text-field>
              <v-text-field v-model="email" label="Email"></v-text-field>
              <v-text-field v-model="password" label="password"></v-text-field>
              <div class="card flex justify-content-center">
        <!-- <Password v-model="value">
            <template #header>
                <h6>Pick a password</h6>
            </template>
            <template #footer>
                <Divider />
                <p class="mt-2">Suggestions</p>
                <ul class="pl-2 ml-2 mt-0" style="line-height: 1.5">
                    <li>At least one lowercase</li>
                    <li>At least one uppercase</li>
                    <li>At least one numeric</li>
                    <li>Minimum 8 characters</li>
                </ul>
            </template>
        </Password> -->
    </div>
    <v-select
              v-model="role"
              :items="roleOptions"
              label="Role"
              
            ></v-select>


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
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { useMyStore } from '@/store/index.js';
import  axios from "axios";

const router = useRouter();
const store = useMyStore();
const firstName=ref('');
const lastName=ref('');
const email=ref('');
const password=ref('');
const role=ref('');
const isLoading = ref(false);
const isLoadingTitle = ref("Loading");
const errorList = ref([]);
const roleOptions=ref('');

 const ReadRoles=  async() =>{
      try{
        const response = await axios.get('http://localhost:5252/api/users/roles');
        roleOptions.value = response.data.roles;
        console.log('rolessssss',roleOptions);
      }catch(error){
        console.log('error',error);
      }
    }
    onMounted(async () => {
      await ReadRoles();
    });
const addUtilisateur = () => {
  isLoading.value = true;
  isLoadingTitle.value = "Saving";

  setTimeout(async () => {
    try {
      const data = {
        firstName:firstName.value,
        lastName:lastName.value,
        email:email.value,
        password:password.value,
        role:role.value
      }
      await store.CreateUser({router},data);
      console.log('UserSended',data);
      isLoading.value = false;
      isLoadingTitle.value = "Loading";
    } catch (error) {
      console.error("Error fetching data:", error);
      if (error.response) {
        if (error.response.status == 400) {
          errorList.value = error.response.data;
          console.log('aaaaaaaaaaa', errorList.value);
          isLoading.value = false;
        }
      }
      isLoading.value = false;
    }
  }, 1500);
};
</script>


<style>
  /* Your styles go here */
</style>
