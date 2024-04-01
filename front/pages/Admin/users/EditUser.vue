<template>
  <v-dialog v-model="editDialog" max-width="500px">
    <v-card>
      <v-card-text>
        <v-container>
          <v-spacer></v-spacer>
          <v-row no-gutters>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                v-model="firstName"
                :label="$t('lastname')"
                base-color="green"
                @blur="v$.firstName.$touch"
                @input="v$.firstName.$touch"
                :error-messages="v$.firstName.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                v-model="lastName"
                :label="$t('firstname')"
                base-color="green"
                @blur="v$.lastName.$touch"
                @input="v$.lastName.$touch"
                :error-messages="v$.lastName.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                v-model="email"
                label="Email"
                base-color="green"
                @blur="v$.email.$touch"
                @input="v$.email.$touch"
                :error-messages="v$.email.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-select
                variant="outlined"
                v-model="role"
                label="Role"
                base-color="green"
                :items="roleOptions"
                @blur="v$.role.$touch"
                @input="v$.role.$touch"
                :error-messages="v$.role.$errors.map((e) => e.$message)"
                :disabled="true"
              ></v-select>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
      <v-divider class="my-2"></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="blue-darken-1"
          variant="text"
          @click="Updateuser"
          :loading="loading"
        >
          {{ $t("edit") }}
        </v-btn>
        <v-btn color="grey" variant="text" @click="close">
          {{ $t("cancel") }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup>
import { ref, onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";

const { emit } = getCurrentInstance();
const { withMessage } = helpers;
const roleOptions = computed(() => store.roleOptions);
const editDialog = ref(false);
const store = useMyStore();
const loading = ref(false);
const firstName = ref("");
const lastName = ref("");
const email = ref("");
const role = ref("");
const id = ref("");
const props = defineProps(["user"]);

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
    InvalidEmail: withMessage("Email invaliddddd", (v) =>
      /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(v)
    ),
  },
  role: {
    required: withMessage("role obligatoire", required),
  },
};
watch(editDialog, (val) => {
  val || close();
});

const v$ = useVuelidate(
  rules,
  {
    firstName,
    lastName,
    email,
    role,
  },
  { $stopPropagation: true }
);

const Updateuser = async () => {
  loading.value = true;
  v$.value.$touch();
  console.log("Form is pending: ", v$.value);
  console.log("Form has errors: ", v$.value);
  if (v$.value.$pending || v$.value.$error) {
    console.log("Form is pending: ", v$.value);
    console.log("Form has errors: ", v$.value);
    loading.value = false;
  } else {
    setTimeout(async () => {
      try {
        const userId = id.value;
        console.log("idddddd", userId);
        const data = {
          firstName: firstName.value,
          lastName: lastName.value,
          email: email.value,
          role: role.value,
        };
        await store.UpdateUser(userId, data);
        console.log("UserSended", data);
        loading.value = false;
        emit("dataChanged");
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        close();
      }
    }, 1500);
  }
};
onMounted(async () => {
  //console.log("edit dialog",props)
  if (props.user) {
    firstName.value = props.user.firstName;
    lastName.value = props.user.lastName;
    email.value = props.user.email;
    role.value = props.user.role;
    id.value = props.user.id;
    editDialog.value = true;
  }
  await store.ReadRoles();
});
const close = () => {
  v$.value.$reset();
  editDialog.value = false;
  emit("close-dialog");
};
</script>