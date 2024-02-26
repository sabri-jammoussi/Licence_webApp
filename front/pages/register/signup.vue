<template>
  <v-container class="mt-10">
    <v-row align="center" justify="center" class="mt-10">
      <v-col cols="12" sm="8" md="6">
        <v-card class="elevation-12">
          <v-card-title class="headline grey lighten-2 text-center">
            <v-icon color="primary">mdi-account-plus</v-icon>
            Register!
          </v-card-title>

          <!-- <Notification :message="error" v-if="error"/> -->

          <v-form @submit.prevent="signup">
            <v-text-field
              v-model="firstName"
              label="firstName"
              @blur="v$.firstName.touch"
              @input="v$.firstName.$touch"
              :error-messages="v$.firstName.$errors.map((e) => e.$message)"
            ></v-text-field>
            <v-text-field
              v-model="lastName"
              label="lastName"
              @blur="v$.lastName.touch"
              @input="v$.lastName.$touch"
              :error-messages="v$.lastName.$errors.map((e) => e.$message)"
            ></v-text-field>
            <v-text-field
              v-model="email"
              label="Email"
              type="email"
              @blur="v$.email.touch"
              @input="v$.email.$touch"
              :error-messages="v$.email.$errors.map((e) => e.$message)"
            ></v-text-field>
            <v-text-field
              v-model="password"
              label="Password"
              @blur="v$.password.touch"
              @input="v$.password.$touch"
              :error-messages="v$.password.$errors.map((e) => e.$message)"
              :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
              @click:append="show2 = !show2"
              :type="show2 ? 'text' : 'password'"
            ></v-text-field>
            <v-text-field
              v-model="role"
              label="role"
              @blur="v$.role.touch"
              @input="v$.role.$touch"
              :error-messages="v$.role.$errors.map((e) => e.$message)"
            ></v-text-field>
            <v-btn type="submit" color="black" block> Register </v-btn>
          </v-form>

          <div class="text-center mt-4">
            Already got an account?
            <nuxt-link to="/register/login" class="primary--text"
              >Login</nuxt-link
            >
          </div>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup>
import { ref } from "vue";
import { useMyStore } from "@/store/index.js";
import { useRouter } from "vue-router";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
const { withMessage, withAsync } = helpers;
const show2 = ref(false);
definePageMeta({
  layout: "custom",
});

const firstName = ref("");
const lastName = ref("");
const email = ref("");
const password = ref("");
const role = ref("");

const router = useRouter();

const store = useMyStore();

const rules = {
  firstName: {
    required: withMessage("First Name obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  lastName: {
    required: withMessage("Last Name obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  email: {
    required: withMessage("E-mail obligatoire", required),
    emailRule: withMessage("email doit avoir  @ and .domain", (v) =>
      /.+@.+\..+/.test(v)
    ),
  },
  password: {
    required: withMessage("Password obligatoire", required),
    min: withMessage("Min 8 caractères", minLength(8)),
    uppercase: withMessage("Au moins une lettre majuscule", (v) =>
      /[A-Z]/.test(v)
    ),
    lowercase: withMessage("Au moins une lettre minuscule", (v) =>
      /[a-z]/.test(v)
    ),
    Number : withMessage("Au moins un chiffre", (v)=>
    /[0-9]/.test(v)
    ),
  },
  role: {
    required: withMessage("role obligatoire", required),
  },
};

const v$ = useVuelidate(rules, {
  firstName,
  lastName,
  email,
  password,
  role,
});
const signup = async () => {
  // Clear previous validation errors
  v$.value.$reset();

  // Validate the form
  v$.value.$touch();

  // Check if there are any validation errors
  if (v$.value.$pending || v$.value.$error) {
    // Display an alert if there are validation errors
  } else {
    // Form is valid, proceed with signup
    const data = {
      firstName: firstName.value,
      lastName: lastName.value,
      email: email.value,
      password: password.value,
      role: role.value,
    };
    console.log("dataaaaaSended", data);

    await store.createUser({ router }, data);
  }
};
</script>
