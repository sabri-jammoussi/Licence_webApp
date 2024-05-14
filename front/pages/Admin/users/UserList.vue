<template>
  <v-card class="mx-auto my-2 card">
    <v-data-table
      :headers="headers"
      :items="data"
      :sort-by="[{ key: 'calories', order: 'asc' }]"
      v-model="search"
      :loading="loading"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>
            <v-text-field
              v-model="search"
              :label="$t('search')"
              prepend-inner-icon="mdi-magnify"
              variant="solo-filled"
              flat
              hide-details
              clearable
              single-line
              align-self-start
            ></v-text-field>
          </v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <AddUser @dataChanged="reloadData" />
          <v-dialog v-model="dialogDelete" max-width="420">
            <v-card>
              <v-card-title>{{ $t("deleteconfirme") }}</v-card-title>
              <v-card-text>{{ $t("deletemsgUser") }}</v-card-text>
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
        <div>
          <v-tooltip text="Tooltip" location="bottom">
            <template v-slot:activator="{ props }">
              <v-icon
                size="small"
                class="me-2"
                @click="openEditDialog(item)"
                color="green"
                variant="tonal"
                v-bind="props"
              >
                mdi-pencil-outline
              </v-icon>
            </template>
            <span>{{ $t("UpdateUser") }}</span>
          </v-tooltip>
          <v-tooltip text="Tooltip" location="bottom">
            <template v-slot:activator="{ props }">
              <v-icon
                size="small"
                v-bind="props"
                @click.stop="deleteItem(item.id)"
                color="red"
              >
                mdi-delete-outline
              </v-icon>
            </template>
            <span>{{ $t("DeleteUser") }}</span>
          </v-tooltip>
        </div>
      </template>
    </v-data-table>
  </v-card>

  <EditUser
    :user="selectedUser"
    v-if="editDialog"
    @close-dialog="editDialog = false"
    @dataChanged="reloadData"
  />
</template>

<script setup>
import { computed, ref, onMounted } from "vue";
import { useI18n } from "vue-i18n";
import axios from "axios";
import AddUser from "./AddUser.vue";
import EditUser from "./EditUser.vue";

const { t } = useI18n();

const selectedUser = ref("");
const editDialog = ref(false);
const dialogDelete = ref(false);
const loading = ref(false);
const search = ref("");
const data = ref([]);

const headers = computed(() => [
  { title: t("lastname"), key: "lastName" },
  { title: t("firstname"), key: "firstName" },
  { title: "Email", key: "email" },
  { title: "Role", key: "role" },
  { title: "Actions", key: "actions", sortable: false },
]);

const getUsers = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/Users");
    data.value = response.data;
  } catch (error) {
    console.error(error);
  }
};

const reloadData = async () => {
  await getUsers();
};

onMounted(async () => {
  await getUsers();
});

const deleteItem = (utilisateurId) => {
  selectedUser.value = utilisateurId;
  dialogDelete.value = true;
};

const deleteItemConfirm = async () => {
  const utilisateurId = selectedUser.value;
  try {
    await axios.delete(`http://localhost:5252/api/Users/${utilisateurId}`);
    await getUsers();
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
