<template>
  <v-card class="card">
  <v-data-table
    :headers="headers"
    :items="data"
    :sort-by="[{ key: 'calories', order: 'asc' }]"
    :search="search"
    :loading="loading"
    :items-per-page="5"
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
            clearable
            hide-details
            single-line
            class="justify-content-start"
          ></v-text-field>
        </v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <!-- <AddUser /> -->
        <AddClient @dataChanged="reloadData" />
        <v-dialog v-model="dialogDelete" max-width="420">
          <v-card>
            <v-card-title>{{ $t("deleteconfirme") }}</v-card-title>
            <v-card-text>{{ $t("deletemsgClient") }}</v-card-text>
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
        @click="consulter(item)"
        color="blue"
        variant="tonal"
      >
        mdi-magnify
      </v-icon>
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
</v-card>
  <EditClient
    :user="selectedUser"
    v-if="editDialog"
    @close-dialog="editDialog = false"
    @dataChanged="reloadData"
  />
</template>  
<script setup>
import axios from "axios";
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import EditClient from "./EditClient.vue";
import AddClient from "./AddClient.vue";
const selectedUser = ref("");
const editDialog = ref(false);
const dialogDelete = ref(false);
const router = useRouter();
const search = ref("");
const data = ref([]);
const loading = ref(false);
let { t } = useI18n();

const headers = computed(() => [
  { title: t("Social reason"), key: "raisonSocial" },
  { title: t("identifier"), key: "identifiant" },
  { title: t("phone"), key: "telephone" },
  { title: "Email", key: "email" },
  { title: t("city"), key: "ville" },
  { title: t("address"), key: "adresse" },
  { title: t("Postal code"), key: "codePostal" },
  { title: t("country"), key: "pays" },
  { title: "Actions", key: "actions", sortable: false },
]);

const editedIndex = ref(-1);
const getClients = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/client");

    data.value = response.data;
    console.log(data);
  } catch (error) {
    console.error(error);
  }
};
const reloadData = async () => {
  return await getClients();
};
onMounted(async () => {
  // console.log('selected user ', selectedUser.value);
  loading.value = true;
  try {
    await getClients();
  } catch (error) {
    console.error(error);
  } finally {
    loading.value = false;
  }
});

const deleteItem = (utilisateurId) => {
  editedIndex.value = utilisateurId;
  dialogDelete.value = true;
};
const deleteItemConfirm = async () => {
  const utilisateurId = editedIndex.value;

  try {
    await axios.delete(`http://localhost:5252/api/client/${utilisateurId}`);
    loading.value = true;
    try {
      await getClients();
    } catch (error) {
      console.error(error);
    } finally {
      loading.value = false;
    }
  } catch (err) {
    console.error(err);
  } finally {
    closeDelete();
  }
};
const openEditDialog = (item) => {
  selectedUser.value = item;
  //console.log('selected user 2 ', selectedUser.value);

  editDialog.value = true;
};
const consulter = (item) => {
  selectedUser.value = item;
  //console.log('selected user 33', selectedUser.value.id);

  router.push(`/Manager/Clients/${selectedUser.value.id}`);
};
const closeDelete = () => {
  dialogDelete.value = false;
};
</script>
          