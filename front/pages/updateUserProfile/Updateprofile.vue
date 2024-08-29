<template>
  <v-card class="mx-auto my-8" elevation="5" max-width="1200">
    <v-toolbar flat>
      <v-btn icon="mdi-account"></v-btn>
      <v-toolbar-title class="font-weight-light">
        {{ $t("UserProfile") }}
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon @click="isEditing = !isEditing">
        <v-fade-transition leave-absolute>
          <v-icon v-if="isEditing">mdi-close</v-icon>
          <v-icon v-else>mdi-pencil-outline</v-icon>
        </v-fade-transition>
      </v-btn>
    </v-toolbar>
    <v-card-text>
      <v-text-field
        v-model="FirstName"
        :disabled="!isEditing"
        base-color="green"
        :label="$t('firstname')"
        @blur="v$.FirstName.$touch"
        @input="v$.FirstName.$touch"
        :error-messages="v$.FirstName.$errors.map((e) => e.$message)"
        variant="outlined"

      ></v-text-field>
      <v-text-field
        v-model="LastName"
        :disabled="!isEditing"
        base-color="green"
        :label="$t('lastname')"
        @blur="v$.LastName.$touch"
        @input="v$.LastName.$touch"
        :error-messages="v$.LastName.$errors.map((e) => e.$message)"
        variant="outlined"
        
      ></v-text-field>
      <v-text-field
        v-model="Email"
        :disabled="!isEditing"
        base-color="green"
        label="Email"
        @blur="v$.Email.$touch"
        @input="v$.Email.$touch"
        :error-messages="v$.Email.$errors.map((e) => e.$message)"
        variant="outlined"

      ></v-text-field>
    </v-card-text>
    <v-divider></v-divider>
    <v-card-actions>
      <v-spacer></v-spacer>

      <v-btn
        :disabled="!isEditing"
        @click="Updateuserprofile"
        :loading="loading"
      >
        {{ $t("validate") }}
      </v-btn>
      <v-btn :disabled="!isEditing" @click="close"> {{ $t("cancel") }} </v-btn>
    </v-card-actions>
  </v-card>
</template>
<script setup>
import { ref } from "vue";
import { useMyStore } from "@/store/index.js";
import { useRouter } from "vue-router";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import axios from "axios";
let { t } = useI18n();
const store = useMyStore();
const router = useRouter();
const isEditing = ref(false);
const loading = ref(false);
const FirstName = ref("");
const LastName = ref("");
const Email = ref("");
const { withMessage, withAsync } = helpers;
const userId = store.user.idd;

const rules = {
  FirstName: {
    required: withMessage("First Name obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  LastName: {
    required: withMessage("Last Name obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  Email: {
    required: withMessage("E-mail obligatoire", required),
    emailRule: withMessage("email doit avoir  @ and .domain", (v) =>
      /.+@.+\..+/.test(v)
    ),
    InvalidEmail: withMessage("Email invaliddddd", (v) =>
      /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(v)
    ),
  },
};
const v$ = useVuelidate(
  rules,
  {
    FirstName,
    LastName,
    Email,
  },
  { $stopPropagation: true }
);

onMounted(async () => {
  FirstName.value = store.user.firstName;
  LastName.value = store.user.lastName;
  Email.value = store.user.email;
  //console.log("id", userId);
});

const Updateuserprofile = async () => {
  loading.value = true;
  setTimeout(async () => {
    try {
      const userId = store.user.idd;
      //console.log("idddddd", userId);
      const data = {
        firstName: FirstName.value,
        lastName: LastName.value,
        email: Email.value,
      };
      await axios.post(`http://localhost:5252/api/account/${userId}`, data);
      console.log("UserSended", data);
      loading.value = false;
      isEditing.value = false;
      await store.logoutUser({ router });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  }, 1500);
};
const close = async () => {
  isEditing.value = false;
};
</script>