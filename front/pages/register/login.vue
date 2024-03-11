<template>
  <v-container class="mt-10">
    <v-row align="center" justify="center" class="mt-10">
      <v-col cols="12" sm="8" md="6">
        <v-card
          class="mx-auto pa-12 pb-"
          elevation="8"
          max-width="488"
          rounded="lg"
        >
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
              :append-inner-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
              @click:append-inner="show2 = !show2"
              :type="show2 ? 'text' : 'password'"
              label="Password"
              placeholder="Enter your password"
              :rules="[rules.required, rules.min]"
            ></v-text-field>
            <v-btn color="black" block type="submit"> Login </v-btn>
          </v-form>
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
const isLoggedinn = computed(() => store.isLoggedin);
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
  if (!email.value || !password.value) {
    errorListe = null;
  } else {
    const data = {
      email: email.value,
      password: password.value,
    };
    console.log("dataaaaaSended", data);
    // Your login logic
    await store.loginUser({ router }, data);
  }
  onMounted(async () => {
    console.log("isloggedin ", isLoggedinn);
  });

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