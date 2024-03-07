<template>
  <v-card class="mx-auto my-8" elevation="5" max-width="1200">
    <v-toolbar flat>
      <v-btn icon="mdi-account"></v-btn>
      <v-toolbar-title class="font-weight-light">
        User Profile
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon @click="isEditing = !isEditing">
        <v-fade-transition leave-absolute>
          <v-icon v-if="isEditing">mdi-close</v-icon>
          <v-icon v-else>mdi-pencil</v-icon>
        </v-fade-transition>
      </v-btn>
    </v-toolbar>
    <v-card-text>
      <v-text-field
        v-model="FirstName"
        :disabled="!isEditing"
        base-color="green"
        label="FirstName"
        @blur="v$.FirstName.$touch"
        @input="v$.FirstName.$touch"
        :error-messages="v$.FirstName.$errors.map((e) => e.$message)"
      ></v-text-field>
      <v-text-field
        v-model="LastName"
        :disabled="!isEditing"
        base-color="green"
        label="LastName"
        @blur="v$.LastName.$touch"
        @input="v$.LastName.$touch"
        :error-messages="v$.LastName.$errors.map((e) => e.$message)"
      ></v-text-field>
      <v-text-field
        v-model="Email"
        :disabled="!isEditing"
        base-color="green"
        label="Email"
        @blur="v$.Email.$touch"
        @input="v$.Email.$touch"
        :error-messages="v$.Email.$errors.map((e) => e.$message)"
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
        Valider
      </v-btn>
      <v-btn :disabled="!isEditing" @click="close"> Annuler </v-btn>
    </v-card-actions>
  </v-card>
  <v-card class="mx-auto my-8" elevation="5" max-width="1200">
    <v-toolbar flat>
      <v-btn icon="mdi-lock"></v-btn>
      <v-toolbar-title class="font-weight-light">
        Update Password
      </v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn icon @click="isEditingPassword = !isEditingPassword">
        <v-fade-transition leave-absolute>
          <v-icon v-if="isEditingPassword">mdi-close</v-icon>
          <v-icon v-else>mdi-pencil</v-icon>
        </v-fade-transition>
      </v-btn>
    </v-toolbar>
    <v-card-text>
      <v-text-field
        v-model="CurrentPassword"
        :disabled="!isEditingPassword"
        base-color="green"
        label="CurrentPassword"
        @blur="v$.CurrentPassword.touch"
        @input="v$.CurrentPassword.$touch"
        :error-messages="v$.CurrentPassword.$errors.map((e) => e.$message)"
      ></v-text-field>
      <v-text-field
        v-model="Newpassword"
        :disabled="!wrongCurrentPWD"
        base-color="green"
        label="NewPassword"
        @blur="v$.Newpassword.$touch"
        @input="v$.Newpassword.$touch"
        :error-messages="v$.Newpassword.$errors.map((e) => e.$message)"
      ></v-text-field>
      <v-text-field
        v-model="ConfirmPassword"
        :disabled="!wrongCurrentPWD"
        base-color="green"
        label="ConfirmPassword"
      ></v-text-field>
    </v-card-text>
    <v-divider></v-divider>
    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn
        :disabled="!isEditingPassword"
        @click="UpdateUserPassword"
        :loading="loadingPWd"
      >
        Valider
      </v-btn>
      <v-btn :disabled="!isEditingPassword" @click="reset"> Annuler </v-btn>
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

const store = useMyStore();
const router = useRouter();
const isEditing = ref(false);
const isEditingPassword = ref(false);
const wrongCurrentPWD = ref(false);
const loading = ref(false);
const loadingPWd = ref(false);
const FirstName = ref("");
const LastName = ref("");
const Email = ref("");
const CurrentPassword = ref("");
const Newpassword = ref("");
const ConfirmPassword = ref("");
const { withMessage, withAsync } = helpers;
const userId = store.user.idd;
watch(CurrentPassword, () => {
  currentPasswordIsCorrect();
});
const currentPasswordIsCorrect = async () => {
  try {
    /*    const response = await axios.post(
      "http://localhost:5252/api/account/verify-password",
      data
    ); */
    const { data } = await axios.get(
      `http://localhost:5252/api/account/verify-password`,
      {
        params: {
          Id: parseInt(userId),
          CurrentPassword: CurrentPassword.value,
        },
      }
    );
    console.log("res password confirm", data);

    if (data) {
      wrongCurrentPWD.value = true;
      return data;
    } else {
      wrongCurrentPWD.value = false;
      return data;
    }
  } catch (error) {
    console.error(error);
    return false;
  }
};

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
  Newpassword: {
    required: withMessage("Password obligatoire", required),
    min: withMessage("Min 8 caractères", minLength(8)),
    uppercase: withMessage("Au moins une lettre majuscule", (v) =>
      /[A-Z]/.test(v)
    ),
    lowercase: withMessage("Au moins une lettre minuscule", (v) =>
      /[a-z]/.test(v)
    ),
    Number: withMessage("Au moins un chiffre", (v) => /[0-9]/.test(v)),
    specialChar: withMessage("Au moins un caractère spécial", (v) =>
      /[!@#$%^&*(),.?":{}|<>]/.test(v)
    ),
  },
  CurrentPassword: {
    currentPWdcorrect: withMessage(
      "Wrong Current Password",
      withAsync(currentPasswordIsCorrect)
    ),
  },
  ConfirmPassword: {
    required: withMessage("Confirm Password obligatoire", required),
  },
};
const v$ = useVuelidate(
  rules,
  {
    FirstName,
    LastName,
    Email,
    Newpassword,
  },
  { $stopPropagation: true }
);

onMounted(async () => {
  FirstName.value = store.user.firstName;
  LastName.value = store.user.lastName;
  Email.value = store.user.email;
  console.log("id", userId);
});

const Updateuserprofile = async () => {
  loading.value = true;
  setTimeout(async () => {
    try {
      const userId = store.user.idd;
      console.log("idddddd", userId);
      const data = {
        firstName: FirstName.value,
        lastName: LastName.value,
        email: Email.value,
      };
      await store.UpdateUserProfile(userId, data);
      console.log("UserSended", data);
      loading.value = false;
      isEditing.value = false;
      await store.logoutUser({ router });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  }, 1500);
};
const reset = async () => {
  isEditingPassword.value = false;
  wrongCurrentPWD.value = false;
  v$.value.$reset();
  CurrentPassword.value = "";
  Newpassword.value = "";
  ConfirmPassword.value = "";
};
const UpdateUserPassword = async () => {
  loadingPWd.value = true;
  setTimeout(async () => {
    try {
      const userId = store.user.idd;
      //console.log("idddddd", userId);
      const data = {
        id: userId,
        currentPassword: CurrentPassword.value,
        newPassword: Newpassword.value,
      };
      const response = await axios.post(
        "http://localhost:5252/api/account/password",
        data
      );
      console.log("User Password Sended", data);
      loadingPWd.value = false;
      isEditingPassword.value = false;
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