<template>
  <v-container class="mt-10">
    <v-row align="center" justify="center" class="mt-10">
      <v-col cols="12" sm="8" md="6">
        <v-card class="elevation-15">
          <v-card-title class="headline grey lighten-2 text-center">
            <v-icon color="primary">mdi-account</v-icon>
            Sign-in!
          </v-card-title>

          <!-- <Notification :message="error" v-if="error"/> -->

          <v-form @submit.prevent="login">
            <v-alert type="error" v-if="errorListe" class="mt-2">
              {{ errorListe }}
            </v-alert>
            <v-text-field
              v-model="email"
              label="Email"
              type="email"
              :rules="loginEmailRules"
            ></v-text-field>

            <v-text-field
              v-model="password"
              :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
              @click:append="show2 = !show2"
              :type="show2 ? 'text' : 'password'"
              label="Password"
              :rules="[rules.required, rules.min]"
            ></v-text-field>

            <v-btn color="black" block type="submit"> Login </v-btn>
          </v-form>

          <div class="text-center mt-5">
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
definePageMeta({
  layout: "custom",
});
import { ref } from "vue";
import { useMyStore } from "@/store/index.js";
import { useRouter } from "vue-router";

const email = ref("");
const password = ref("");
const errorListe = computed(() => store.eroorlistee);
const store = useMyStore();
const router = useRouter();
const show2 = ref(false);

const rules = {
  required: (value) => !!value || "Password obligatoire.",
  min: (v) => (v && v.length >= 8) || "Min 8 caractères",
};

const loginEmailRules = [
  (v) => !!v || "E-mail obligatoire",
  (v) => /.+@.+\..+/.test(v) || "E-mail doit avoir @ et .Domain ",
];
const login = async () => {
  const data = {
    email: email.value,
    password: password.value,
  };
  console.log("dataaaaaSended", data);
  // Your login logic
  await store.loginUser({ router }, data);

  //console.log('eroor  from login ',errorListe.value);
  // Check the status or response in the store, then navigate based on that.
  // if (store.tokenLoaded) {
  //   router.push('/');
  // } else {
  //   // Handle login failure, display an error message, etc.
  //   alert('Login failed. Please check your credentials.');
  // }
};
</script>