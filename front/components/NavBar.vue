<template>
  <div v-if="isLoading" class="progress_circular">
      <v-progress-circular :size="60" color="success" indeterminate></v-progress-circular>
    </div>
    <div v-else>
  <v-layout>
    <!-- <v-system-bar color="deep-purple darken-3"></v-system-bar> -->
    <v-navigation-drawer v-model="drawer">
      <v-list class="custom-list-margin">
        <div class="logo-container">
          <img src="/assets/logo.jpg" alt="Logo" />
        </div>
        <nuxt-link to="/" class="v-list-item__content">
          <v-list-item prepend-icon="mdi-view-dashboard" value="home">
            <v-tooltip activator="parent" location="end">Accueil</v-tooltip>
            <span class="v-list-item__title">Accueil</span>
          </v-list-item>
        </nuxt-link>

        <v-list>
          <!-- <nuxt-link
                v-for="item in items"
                :key="item.text"
                :to="item.route"
              > 
        
                <v-list-item :prepend-icon="item.icon">
                  <span>{{ item.text }}</span>
           
                </v-list-item>
                
              </nuxt-link> -->
        </v-list>
        <nuxt-link to="/users/">
          <v-list-item prepend-icon="mdi-account">
            <v-tooltip activator="parent" location="end">Utilisateur</v-tooltip>
            <span class="v-list-item__title">Utilisateur</span>
          </v-list-item>
        </nuxt-link>
        <nuxt-link to="/about" class="v-list-item__content">
          <v-list-item prepend-icon="mdi-forum">
            <v-tooltip activator="parent" location="end">A propos</v-tooltip>
            <span class="v-list-item__title">A propos</span>
          </v-list-item>
        </nuxt-link>
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
      dark
      prominent
    >
      <v-app-bar-nav-icon
        variant="text"
        @click.stop="drawer = !drawer"
      ></v-app-bar-nav-icon>
      <v-toolbar-title>My Users</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-row class="d-flex justify-end mr-2">
        <v-menu min-width="200px" rounded>
          <template v-slot:activator="{ props }">
            <v-btn
              icon="mdi-dots-vertical "
              v-bind="props"
              style="color: aliceblue"
              @click="readusers"
            >
            </v-btn>
          </template>
          <v-card>
            <v-card-text>
              <div class="mx-auto text-center">
                <v-avatar color="brown">
                  <span class="text">{{ store.user.firstName }}</span>
                </v-avatar>
              <h3>{{store.user.firstName}} {{ store.user.lastName }}</h3>
            <p class="text-caption mt-1">
               {{store.user.email}}
              </p>
              <v-divider class="my-3"></v-divider>
           <nuxt-link to="/"> 
            <v-btn
                rounded
                variant="text"
              >
              Modifier le compte
              </v-btn>
           </nuxt-link> 
       
                <v-divider class="my-3"></v-divider>

                  <v-btn rounded variant="text" @click="logout" > Déconnecter </v-btn>
   
              </div>
            </v-card-text>
          </v-card>
        </v-menu>
      </v-row>
    </v-app-bar>
    <v-main>
      <v-card-text>
        <!-- Your main content goes here (for example, the Home component) -->
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
  </v-layout>
</div>
</template>
  
<script setup>
import { ref, onMounted } from 'vue';
import { useMyStore } from '@/store/index.js';
import { useRouter } from 'vue-router';
const isLoading = ref(true);
const drawer = ref(false);
const store = useMyStore();
const router = useRouter();

onMounted(async () => {
  await setTimeout(() => {
    isLoading.value = false;
  }, 1500);
});
 
const logout= async() =>{
await store.logoutUser({  router});
}
const readusers = async ()=>{
  await store.ReadUser();
  console.log('Store User:', store.user.firstName);

}
// const userfirstName = computed(()=>store.user)
// console.log('offfffffffff',userfirstName);

</script>

  <style scoped>
.custom-list-margin {
  margin-top: -8px; /* Adjust the margin-top value as per your preference */
  width: 100%;
}
.logo-container {
  display: flex;
  align-items: center;
  justify-content: center; /* Center the logo horizontally */
  height: 64px;
  width: 255px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  /* Adjust the height as needed */
  background-color: #000000; /* Add a background color for the logo container */
}

.logo-container img {
  max-height: 100%; /* Ensure the logo scales proportionally */
  max-width: 100%; /* Ensure the logo scales proportionally */
  object-fit: contain; /* Preserve the aspect ratio and fit the logo within the container */
}
.progress_circular {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
  }
</style>
  