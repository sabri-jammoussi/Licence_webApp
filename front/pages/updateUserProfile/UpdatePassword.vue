<template>
  <v-card class="mx-auto my-8" elevation="5" max-width="1200">
    <v-toolbar flat>
      <v-btn icon="mdi-lock"></v-btn>
      <v-toolbar-title class="font-weight-light">
        {{ $t("UpdatePassword") }}
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
        v-model="currentPassword"
        :disabled="!isEditingPassword"
        :append-inner-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
        @click:append-inner="show1 = !show1"
        :type="show1 ? 'text' : 'password'"
        base-color="green"
        :label="$t('CurrentPassword')"
        @blur="v$.currentPassword.$touch"
        @input="v$.currentPassword.$touch"
        :error-messages="v$.currentPassword.$errors.map((e) => e.$message)"
      ></v-text-field>
      <v-text-field
        v-model="Newpassword"
        :disabled="!wrongCurrentPWD"
        base-color="green"
        :label="$t('NewPassword')"
        :append-inner-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
        @click:append-inner="show2 = !show2"
        :type="show2 ? 'text' : 'password'"
        @blur="v$.Newpassword.$touch"
        @input="v$.Newpassword.$touch"
        :error-messages="v$.Newpassword.$errors.map((e) => e.$message)"
      ></v-text-field>
      <v-text-field
        v-model="ConfirmPassword"
        :disabled="!wrongCurrentPWD"
        base-color="green"
        :label="$t('ConfirmPassword')"
        :append-inner-icon="show3 ? 'mdi-eye' : 'mdi-eye-off'"
        @click:append-inner="show3 = !show3"
        :type="show3 ? 'text' : 'password'"
        @blur="v$.ConfirmPassword.$touch"
        @input="v$.ConfirmPassword.$touch"
        :error-messages="v$.ConfirmPassword.$errors.map((e) => e.$message)"
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
        {{ $t("validate") }}
      </v-btn>
      <v-btn :disabled="!isEditingPassword" @click="reset">
        {{ $t("cancel") }}
      </v-btn>
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
const loadingPWd = ref(false);
const isEditingPassword = ref(false);
const currentPassword = ref("");
const Newpassword = ref("");
const ConfirmPassword = ref("");
const { withMessage, withAsync } = helpers;
const wrongCurrentPWD = ref(false);
const userId = store.user.idd;
const show1 = ref(false);
const show2 = ref(false);
const show3 = ref(false);

watch(currentPassword, () => {
  currentPasswordIsCorrect();
});
const wrongCofirmPWd = async () => {
  if (Newpassword.value === ConfirmPassword.value) return true;
  else return false;
};
const SamePasswordError = () => {
  if (currentPassword.value === Newpassword.value) return false;
  else return true;
};

const currentPasswordIsCorrect = async () => {
  try {
    if (!currentPassword.value) return;
    const { data } = await axios.get(
      `http://localhost:5252/api/account/verify-password`,
      {
        params: {
          Id: parseInt(userId),
          CurrentPassword: currentPassword.value,
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
    SamePassword: withMessage(
      "Le méme Mot de passe utiliser ",
      withAsync(SamePasswordError)
    ),
  },
  currentPassword: {
    required: withMessage("Current Password obligatoire", required),
    currentPWdcorrect: withMessage(
      "Wrong Current Password",
      withAsync(currentPasswordIsCorrect)
    ),
  },
  ConfirmPassword: {
    required: withMessage("Confirm Password obligatoire", required),
    confirmPWd: withMessage(
      "Le nouveau mot de passe ne correspond pas.",
      withAsync(wrongCofirmPWd)
    ),
  },
};
const v$ = useVuelidate(
  rules,
  {
    Newpassword,
    currentPassword,
    ConfirmPassword,
  },
  { $stopPropagation: true }
);
const UpdateUserPassword = async () => {
  loadingPWd.value = true;
  // Validate the form
  v$.value.$touch();
  if (v$.value.$invalid) {
    loadingPWd.value = false;
  } else {
    setTimeout(async () => {
      try {
        const userId = store.user.idd;
        //console.log("idddddd", userId);
        const data = {
          id: userId,
          currentPassword: currentPassword.value,
          newpassword: Newpassword.value,
        };
        console.log("User Password Sended", data);
        await axios.post("http://localhost:5252/api/account/password", data);
        console.log("User Password Sended", data);
        loadingPWd.value = false;
        await store.logoutUser({ router });
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    }, 1500);
  }
};
const reset = async () => {
  isEditingPassword.value = false;
  wrongCurrentPWD.value = false;
  v$.value.$reset();
  currentPassword.value = "";
  Newpassword.value = "";
  ConfirmPassword.value = "";
  show1.value = false;
};
</script>
  