<template >
  <v-card class="card">
    <v-data-table
      :headers="headers"
      :items="data"
      :sort-by="[{ key: 'calories', order: 'asc' }]"
      :search="search"
      :loading="loading"
      :items-per-page="7"
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
              clearable
              class="justify-content-start"
            ></v-text-field>
          </v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
          <!-- <AddUser /> -->
          <!-- <AddClient @dataChanged="reloadData"/> -->
          <SelectApplication />
          <v-dialog v-model="dialogDelete" max-width="420">
            <v-card>
              <v-card-title>{{ $t("deleteconfirme") }}</v-card-title>
              <v-card-text>{{ $t("deletemsgLicence") }}</v-card-text>
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
          mdi-pencil-outline
        </v-icon>
        <v-icon size="small" @click.stop="deleteItem(item.id)" color="red">
          mdi-delete-outline
        </v-icon>
      </template>
    </v-data-table>
  </v-card>
  <!-- <EditClient
      :user="selectedUser"
      v-if="editDialog"
      @close-dialog="editDialog = false"
      @dataChanged="reloadData"
    /> -->
  <EditLicence
    :Licence="SelectedLicence"
    v-if="editDialog"
    @close-dialog="editDialog = false"
    @dataChanged="reloadData"
  />
  <SnackBar
    :key="keyToast"
    v-if="showSnackbar"
    :message="snackbarMessage"
    :showSnackBar="showSnackbar"
  />
</template>  
  <script setup>
import EditLicence from "./EditLicence.vue";
import axios from "axios";
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import { format } from "date-fns";
import SelectApplication from "./SelectApplication.vue";
import SnackBar from "~/components/SnackBar.vue";

const selectedUser = ref("");
const editDialog = ref(false);
const dialogDelete = ref(false);
const router = useRouter();
const search = ref("");
const data = ref([]);
const loading = ref(false);
const SelectedLicence = ref("");
let { t } = useI18n();
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const headers = computed(() => [
  { title: t("DateExp"), key: "formattedDate" },
  { title: t("Users"), key: "user" },
  { title: "Applications", key: "applicationNom" },
  { title: t("client"), key: "clientRaison" },
  { title: "Actions", key: "actions", sortable: false },
]);

const editedIndex = ref(-1);
const getLicences = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/licence");
    data.value = response.data.map((item) => ({
      ...item,
      formattedDate: format(new Date(item.dateExp), "MMMM dd, yyyy"),
    }));
  } catch (error) {
    console.error(error);
  }
};
const reloadData = async () => {
  return await getLicences();
};
onMounted(async () => {
  // console.log('selected user ', selectedUser.value);

  loading.value = true;
  try {
    await getLicences();
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
  console.log(utilisateurId);
  try {
    await axios.delete(`http://localhost:5252/api/licence?id=${utilisateurId}`);
    loading.value = true;
    try {
      showSnackbar.value = true;
      keyToast.value++;
      snackbarMessage.value = t("deleteItem");
    } catch (error) {
      console.error(error);
    } finally {
      loading.value = false;
    }
  } catch (err) {
    console.error(err);
  } finally {
    closeDelete();
    await new Promise((resolve) => setTimeout(resolve, 2510));
    reloadData();
  }
};
const openEditDialog = (item) => {
  SelectedLicence.value = item;
  //console.log('selected user 2 ', SelectedLicence.value);

  editDialog.value = true;
};
const consulter = (item) => {
  SelectedLicence.value = item;
  // console.log("selected user 33", SelectedLicence.value.id);

  router.push(`/Manager/Licences/${SelectedLicence.value.id}`);
};
const closeDelete = () => {
  dialogDelete.value = false;
};
</script>
