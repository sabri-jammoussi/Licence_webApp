<template>
  <v-data-table
    :headers="headers"
    :items="users"
    :sort-by="[{ key: 'calories', order: 'asc' }]"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>My Users</v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-width="500px">
          <template v-slot:activator="{ props }">
            <v-btn color="primary" dark class="mb-2" v-bind="props">
              <v-icon color="green" style="margin: auto">mdi-plus</v-icon>

              New Item
            </v-btn>
          </template>
          <v-card>
            <v-card-text>
              <v-container>
                <v-spacer></v-spacer>
                <v-row>
                  <v-col cols="12" sm="6" md="12">
                    <v-text-field
                      v-model="firstName"
                      label="FirstName"
                      @blur="v$.firstName.touch"
                      @input="v$.firstName.$touch"
                      :error-messages="
                        v$.firstName.$errors.map((e) => e.$message)
                      "
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="12">
                    <v-text-field
                      v-model="lastName"
                      label="LastName"
                      @blur="v$.lastName.touch"
                      @input="v$.lastName.$touch"
                      :error-messages="
                        v$.lastName.$errors.map((e) => e.$message)
                      "
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="12">
                    <v-text-field
                      v-model="email"
                      label="Email"
                      @blur="v$.email.touch"
                      @input="v$.email.$touch"
                      :error-messages="v$.email.$errors.map((e) => e.$message)"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="12">
                    <v-text-field
                      v-model="password"
                      label="Password"
                      @blur="v$.password.touch"
                      @input="v$.password.$touch"
                      :error-messages="
                        v$.password.$errors.map((e) => e.$message)
                      "
                      :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
                      @click:append="show2 = !show2"
                      :type="show2 ? 'text' : 'password'"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="12">
                    <v-select
                      v-model="role"
                      label="Role"
                      :items="roleOptions"
                      @blur="v$.role.touch"
                      @input="v$.role.$touch"
                      :error-messages="v$.role.$errors.map((e) => e.$message)"
                    ></v-select>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="blue-darken-1"
                variant="text"
                @click="addUtilisateur"
                :loading="loading"
              >
                Ajouter
              </v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="close">
                Annuler
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="editDialog" max-width="500px">
          <v-card>
            <v-card-text>
              <v-container>         
                <v-spacer></v-spacer>
                <v-row>
                  <v-col cols="12" sm="6" md="12">
                    <v-text-field
                      v-model="firstName"
                      label="FirstName"
                      @blur="v$.firstName.touch"
                      @input="v$.firstName.$touch"
                      :error-messages="
                        v$.firstName.$errors.map((e) => e.$message)
                      "
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="12">
                    <v-text-field
                      v-model="lastName"
                      label="LastName"
                      @blur="v$.lastName.touch"
                      @input="v$.lastName.$touch"
                      :error-messages="
                        v$.lastName.$errors.map((e) => e.$message)
                      "
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="12">
                    <v-text-field
                      v-model="email"
                      label="Email"
                      @blur="v$.email.touch"
                      @input="v$.email.$touch"
                      :error-messages="v$.email.$errors.map((e) => e.$message)"
                    ></v-text-field>
                  </v-col>
                  <v-col cols="12" sm="6" md="12">
                    <v-select
                      v-model="role"
                      label="Role"
                      :items="roleOptions"
                      @blur="v$.role.touch"
                      @input="v$.role.$touch"
                      :error-messages="v$.role.$errors.map((e) => e.$message)"
                    ></v-select>
                  </v-col>
                </v-row>
              </v-container>
            </v-card-text>

            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn
                color="blue-darken-1"
                variant="text"
                @click="addUtilisateur"
                :loading="loading"
              >
                Ajouter
              </v-btn>
              <v-btn color="blue-darken-1" variant="text" @click="close">
                Annuler
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogDelete" max-width="400">
          <v-card>
            <v-card-title>Delete Confirmation</v-card-title>
            <v-card-text
              >Voulez-vous vraiment supprimer cet utilisateur ?</v-card-text
            >
            <v-card-actions>
              <v-btn color="red" text @click="deleteItemConfirm"
                >Supprimer</v-btn
              >
              <v-btn color="primary" text @click="closeDelete">Annuler</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>
    <template v-slot:item.actions="{ item }">
      <v-icon size="small" class="me-2" @click="openEditDialog(item)">
        mdi-pencil
      </v-icon>
      <v-icon size="small" @click.stop="deleteItem(item.id)">
        mdi-delete
      </v-icon>
    </template>
  </v-data-table>
</template>
  
  <script setup>
import axios from "axios";
import { ref, onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";

const { withMessage, withAsync } = helpers;
const show2 = ref(false);
const Email_Existe = computed(() => store.EmailError);

const store = useMyStore();
const dialog = ref(false);
const editDialog = ref(false);
const dialogDelete = ref(false);
const roleOptions = ref("");
const firstName = ref("");
const lastName = ref("");
const email = ref("");
const password = ref("");
const role = ref("");
const loading = ref(false);

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
    emailRule: withMessage("email doit avoir  @ and .domain", (v) =>
      /.+@.+\..+/.test(v)
    ),
    EmailExiste : withMessage("Email déja Existe ", withAsync(EmailIsUnique)),
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

const v$ = useVuelidate(rules, {
  firstName,
  lastName,
  email,
  password,
  role,
});

const headers = ref([
  { title: "FirstName", key: "firstName" },
  { title: "LastName", key: "lastName" },
  { title: "Email", key: "email" },
  { title: "Role", key: "role" },
  { title: "Actions", key: "actions", sortable: false },
]);
const users = ref([]);
const editedIndex = ref(-1);

const addUtilisateur = () => {
  loading.value = true;
  // Clear previous validation errors
  v$.value.$reset();

  // Validate the form
  v$.value.$touch();
  if (v$.value.$pending || v$.value.$error) {
    loading.value = false;
    // Display an alert if there are validation errors
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
        console.log("UserSended", data);
        console.log("emaillllllllllll", Email_Existe);
        loading.value = false;
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        close();
      }
    }, 1500);
  }
};
onMounted(() => {
  initialize();
  EmailIsUnique();
});

const initialize = async () => {
  await getUsers();
  await readRoles();
};

const readRoles = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/users/roles");
    roleOptions.value = response.data.roles;
    console.log("rolessssss", roleOptions.value);
  } catch (error) {
    console.log("error", error);
  }
};

const getUsers = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/Users");
    users.value = response.data;
    console.log("data", response);
  } catch (error) {
    console.error("Error fetching data:", error);
    alert("Error connecting to the database");
  }
};

const deleteItem = (utilisateurId) => {
  editedIndex.value = utilisateurId;
  dialogDelete.value = true;
};

const deleteItemConfirm = async () => {
  const utilisateurId = editedIndex.value;
  console.log("iddddd", utilisateurId);
  try {
    const res = await axios.delete(
      `http://localhost:5252/api/Users/${utilisateurId}`
    );
    console.log("deleting user", res);
    await getUsers();
  } catch (err) {
    console.error(err);
  } finally {
    closeDelete();
  }
};
const openEditDialog = (item) => {
  // Additional logic if needed when opening the edit dialog
  // You can pre-fill the form fields with the data from the selected item
   firstName.value = item.firstName;
   lastName.value = item.lastName;
   email.value = item.email;
   role.value = item.role;
  editDialog.value = true;
};
const close = () => {
  // Reset form fields and validation
  firstName.value = "";
  lastName.value = "";
  email.value = "";
  password.value = "";
  role.value = "";

  v$.value.$reset();

  // Close the dialog
  dialog.value = false;
  editDialog.value = false ;
};

const closeDelete = () => {
  dialogDelete.value = false;
};
</script>
  