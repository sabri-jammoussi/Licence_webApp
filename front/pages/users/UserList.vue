<template>
  <v-data-table
    :headers="headers"
    :items="users"
    :sort-by="[{ key: 'calories', order: 'asc' }]"
    v-model:search="search"
  >
    <template v-slot:top>
      <v-toolbar flat>
        <v-toolbar-title>
          <v-text-field
            v-model="search"
            density="compact"
            :label="$t('search')"
            prepend-inner-icon="mdi-magnify"
            variant="solo-filled"
            flat
            hide-details
            single-line
            class="justify-content-start"
          ></v-text-field>
        </v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <AddUser />
        <v-dialog v-model="dialogDelete" max-width="420">
          <v-card>
            <v-card-title>{{ $t("deleteconfirme") }}</v-card-title>
            <v-card-text>{{ $t("deletemsg") }}</v-card-text>
            <v-divider class="my-2"></v-divider>

            <v-card-actions>
              <v-spacer></v-spacer>

              <v-btn color="red" text @click="deleteItemConfirm">{{
                $t("delete")
              }}</v-btn>
              <v-btn color="grey" text @click="closeDelete">{{
                $t("cancel")
              }}</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </v-toolbar>
    </template>

    <template v-slot:[`item.actions`]="{ item }">
      <v-icon
        size="small"
        class="me-2"
        @click="openEditDialog(item)"
        color="green"
      variant="tonal"
      >
        mdi-pencil
      </v-icon>
      <v-icon size="small" @click.stop="deleteItem(item.id)" color="red">
        mdi-delete
      </v-icon>
    </template>
  </v-data-table>

  <EditUser
    :user="selectedUser"
    v-if="editDialog"
    @close-dialog="editDialog = false"
  />
</template>  
<script setup>
import axios from "axios";
import { ref, onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
import AddUser from "./AddUser.vue";
import EditUser from "./EditUser.vue";
const selectedUser = ref("");
const store = useMyStore();
const editDialog = ref(false);
const dialogDelete = ref(false);
const search = ref("");
let { t } = useI18n();

const headers = computed(() => [
  { title: t("lastname"), key: "firstName" },
  { title: t("firstname"), key: "lastName" },
  { title: "Email", key: "email" },
  { title: "Role", key: "role" },
  { title: "Actions", key: "actions", sortable: false },
]);

const users = computed(() => store.users);
const editedIndex = ref(-1);
onMounted(async () => {
  await store.getUsers();
});
const deleteItem = (utilisateurId) => {
  editedIndex.value = utilisateurId;
  dialogDelete.value = true;
};
const deleteItemConfirm = async () => {
  const utilisateurId = editedIndex.value;
  //console.log("iddddd", utilisateurId);
  try {
    const token = window.localStorage.getItem("token");
    if (token) {
      // If there is a token, set the authorization header
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
      console.log("Token checked:", axios.defaults.headers.common);
    } else {
      console.log("unauthorized");
      alert("unauthorized");
    }
    const res = await axios.delete(
      `http://localhost:5252/api/Users/${utilisateurId}`
    );
    console.log("deleting user", res);
    await store.getUsers();
  } catch (err) {
    console.error(err);
  } finally {
    closeDelete();
  }
};
const openEditDialog = (item) => {
  selectedUser.value = item;
  editDialog.value = true;
};
const closeDelete = () => {
  dialogDelete.value = false;
};
</script>
    