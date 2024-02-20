<template>
  <v-container class="">
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6">
        <v-card class="elevation-12">
          <v-card-title class="headline grey lighten-2">
            <v-icon color="primary">mdi-account</v-icon>
            Sign-in!
          </v-card-title>

          <!-- <Notification :message="error" v-if="error"/> -->

          <v-form @submit.prevent="login">
            <v-text-field
              v-model="email"
              label="Email"
              type="email"
              required
            ></v-text-field>

            <v-text-field
              v-model="password"
              label="Password"
              type="password"
              required
            ></v-text-field>

            <v-btn color="black" block type="submit"> Login </v-btn>
          </v-form>

          <div class="text-center mt-4">
            Already got an account?
            <nuxt-link to="/register/signup" class="primary--text"
              >Sign-up</nuxt-link
            >
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

const email = ref('');
const password = ref('');

const store = useMyStore();
const router = useRouter();

const login = async () => {
  if (!email.value || !password.value) {
    alert('error');
  } else {
    const data = {
      email: email.value,
      password: password.value,
    };
    console.log('dataaaaaSended', data);
      // Your login logic
      await store.loginUser({ router }, data);

      // Check the status or response in the store, then navigate based on that.
      // if (store.tokenLoaded) {
      //   router.push('/');
      // } else {
      //   // Handle login failure, display an error message, etc.
      //   alert('Login failed. Please check your credentials.');
      // }
    
  }
};
</script>