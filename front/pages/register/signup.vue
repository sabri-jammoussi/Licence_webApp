<template>
   <v-container class="mt-10">
    <v-row align="center" justify="center" class="mt-10">
      <v-col cols="12" sm="8" md="6">
        <v-card class="elevation-12">
          <v-card-title class="headline grey lighten-2 text-center " > 
            <v-icon color="primary">mdi-account-plus</v-icon>
            Register!
          </v-card-title>

          <!-- <Notification :message="error" v-if="error"/> -->

          <v-form @submit.prevent="signup">
            <v-text-field v-model="firstName" label="firstName" required></v-text-field>
            <v-text-field v-model="lastName" label="lastName" required></v-text-field>
            <v-text-field v-model="email" label="Email" type="email" required></v-text-field>
            <v-text-field v-model="password" label="Password" type="password" required></v-text-field>
            <v-text-field v-model="role" label="role" required></v-text-field>
            <v-btn type="submit" color="black" block> Register </v-btn>
          </v-form>

          <div class="text-center mt-4">
            Already got an account?
            <nuxt-link to="/register/login" class="primary--text">Login</nuxt-link>
          </div>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { ref } from 'vue';
import { useMyStore } from '@/store/index.js';
import { useRouter } from 'vue-router';

definePageMeta({
  layout:"custom"
})

const firstName = ref('');
const lastName = ref('');
const email = ref('');
const password = ref('');
const role = ref('');
const router = useRouter();

const store = useMyStore();

const signup = async () => {
  if (!firstName.value || !email.value || !password.value) {
    alert('Please Fill the Field');
  } else {
    const data = {
      firstName: firstName.value,
      lastName: lastName.value,
      email: email.value,
      password: password.value,
      role: role.value,
    };
    console.log('dataaaaaSended', data);

    await store.createUser({ router}, data );
  }
};


</script>
