<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template v-slot:activator="{ props }">
      <v-btn color="primary" dark class="mb-2" v-bind="props">
        <v-tooltip location="bottom" activator="parent">{{$t("newUser")}}</v-tooltip>

        <v-icon color="green" style="margin: auto">mdi-plus</v-icon>
        {{ $t("new") }}
      </v-btn>
    </template>
    <v-card>
      <div  class="grey--text text-h6 text-lg-h6 mt-2">
                <v-icon left color="green" size="35" class="ml-2">mdi-account  </v-icon>
                 {{ $t("newUser") }}
              </div>
              <v-divider></v-divider>
      <v-card-text>
        <v-container>
          <v-row no-gutters>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                base-color="green"
                :label="$t('lastname')"
                v-model="lastName"
                @blur="v$.lastName.touch"
                @input="v$.lastName.$touch"
                :error-messages="v$.lastName.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                v-model="firstName"
                :label="$t('firstname')"
                base-color="green"
                @blur="v$.firstName.touch"
                @input="v$.firstName.$touch"
                :error-messages="v$.firstName.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                v-model="email"
                label="Email"
                base-color="green"
                @blur="v$.email.touch"
                @input="v$.email.$touch"
                :error-messages="v$.email.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                v-model="password"
                :label="$t('password')"
                base-color="green"
                @blur="v$.password.touch"
                @input="v$.password.$touch"
                :error-messages="v$.password.$errors.map((e) => e.$message)"
                :append-inner-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
                @click:append-inner="show2 = !show2"
                :type="show2 ? 'text' : 'password'"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-select
                variant="outlined"
                v-model="role"
                label="Role"
                base-color="green"
                :items="roleOptions"
                @blur="v$.role.touch"
                @input="v$.role.$touch"
                :error-messages="v$.role.$errors.map((e) => e.$message)"
              ></v-select>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
      <v-divider class="my-2"></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="green"
          variant="text"
          @click="addUtilisateur"
          :loading="loading"
        >
          {{ $t("add") }}
        </v-btn>
        <v-btn color="grey" variant="text" @click="close">
          {{ $t("cancel") }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup>
import axios from "axios";
import { ref, onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
const { emit } = getCurrentInstance();
const show2 = ref(false);
const roleOptions = computed(() => store.roleOptions);
const firstName = ref("");
const lastName = ref("");
const email = ref("");
const password = ref("");
const role = ref("");
const dialog = ref(false);
const loading = ref(false);
const store = useMyStore();
const { withMessage, withAsync } = helpers;

const EmailIsUnique = async () => {
  try {
    const response = await axios.get(
      `http://localhost:5252/api/Users?email=${email.value}`
    );
    const existingEmail = response.data.find(
      (emailUnique) => emailUnique.email === email.value
    );
    console.log("response of emailUnique", existingEmail);

    if (existingEmail) return false;
    else return true;
  } catch (error) {
    console.error(error);
    return false;
  }
};
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
    EmailExiste: withMessage("Email déja Existe ", withAsync(EmailIsUnique)),
    emailRule: withMessage("email doit avoir  @ and .domain", (v) =>
      /.+@.+\..+/.test(v)
    ),
    InvalidEmail: withMessage("Email invalid", (v) =>
      /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(v)
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
    Number: withMessage("Au moins un chiffre", (v) => /[0-9]/.test(v)),
    specialChar: withMessage("Au moins un caractère spécial", (v) =>
      /[!@#$%^&*(),.?":{}|<>]/.test(v)
    ),
  },
  role: {
    required: withMessage("role obligatoire", required),
  },
};
const v$ = useVuelidate(
  rules,
  {
    firstName,
    lastName,
    email,
    password,
    role,
  },
  { $stopPropagation: true }
);
//stopPropagation bech thot rules 7ad page heki
watch(dialog, (val) => {
  val || close();
});

const addUtilisateur = () => {
  loading.value = true;
  // Clear previous validation errors
  v$.value.$reset();
  // Validate the form
  v$.value.$touch();

  if (v$.value.$pending || v$.value.$error) {
    loading.value = false;
  } else {
    setTimeout(async () => {
      try {
        const data = {
          firstName: firstName.value,
          lastName: lastName.value,
          email: email.value,
          password: password.value,
          role: role.value,
        };
        await store.CreateUser(data);
        emit("dataChanged");
        console.log("UserSended", data);
        loading.value = false;
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        close();
      }
    }, 1500);
  }
};
const close = () => {
  // Reset form fields and validation
  firstName.value = "";
  lastName.value = "";
  email.value = "";
  password.value = "";
  role.value = "";
  v$.value.$reset();
  dialog.value = false;
};
onMounted(async () => {
  await store.ReadRoles();
});
</script> 