<template>
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
            hide-details
            clearable
            single-line
            class="justify-content-start"
          ></v-text-field>
        </v-toolbar-title>
        <v-divider class="mx-4" inset vertical></v-divider>
        <AddApplication @dataChanged="reloadData" />
        <EditApplication
          :user="selectedUser"
          v-if="editDialog"
          @close-dialog="editDialog = false"
          @dataChanged="reloadData"
        />
        <v-dialog v-model="dialogDelete" max-width="420">
          <v-card>
            <v-card-title>{{ $t("deleteconfirme") }}</v-card-title>
            <v-card-text>{{ $t("deletemsgApp") }}</v-card-text>
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
      <v-tooltip text="Tooltip" location="bottom">
        <template v-slot:activator="{ props }">
          <v-icon
            size="small"
            class="me-2"
            @click="consulter(item)"
            color="blue"
            variant="tonal"
            v-bind="props"
          >
            mdi-magnify
          </v-icon>
        </template>
        <span>{{$t('ConsultApp')}}</span>
      </v-tooltip>
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
            mdi-pencil
          </v-icon>
        </template>
        <span>
          {{ $t("UpdateApp") }}
        </span>
      </v-tooltip>
      <v-tooltip text="Tooltip" location="bottom">
        <template v-slot:activator="{ props }">
          <v-icon
            size="small"
            @click.stop="deleteItem(item.id)"
            v-bind="props"
            color="red"
          >
            mdi-delete
          </v-icon>
        </template>
        <span> {{ $t("DeleteApp") }} </span>
      </v-tooltip>
    </template>
  </v-data-table>
  <SnackBar
    :key="keyToast"
    :message="snackbarMessage"
    :showSnackBar="showSnackbar"
  />
</template>
    
    <script setup>
import axios from "axios";
import { ref, onMounted } from "vue";
import { useRouter } from "vue-router";
import AddApplication from "./AddApplication.vue";
import EditApplication from "./EditApplication.vue";
import SnackBar from "~/components/SnackBar.vue";
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const selectedUser = ref("");
const editDialog = ref(false);
const dialogDelete = ref(false);
const router = useRouter();
const search = ref("");
const loading = ref(false);
let { t } = useI18n();
const data = ref([]);

const headers = computed(() => [
  { title: t("identifier"), key: "identifiant" },
  { title: t("name"), key: "nom" },
  { title: "Description", key: "description" },
  { title: "Actions", key: "actions", sortable: false },
]);

const editedIndex = ref(-1);
const getApplications = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/appliction");

    data.value = response.data;
    console.log(data);
  } catch (error) {
    console.error(error);
  }
};
const reloadData = async () => {
  return await getApplications();
};
onMounted(async () => {
  // console.log('selected user ', selectedUser.value);
  loading.value = true;
  try {
    await getApplications();
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
    await axios.delete(`http://localhost:5252/api/appliction/${utilisateurId}`);
    loading.value = true;
    try {
      showSnackbar.value = true;
      keyToast.value++;
      snackbarMessage.value = "Item deleted successfully.";
      // Fetch data
    } catch (error) {
      console.error(error);
    } finally {
      loading.value = false;
    }
  } catch (err) {
    console.error(err);
  } finally {
    closeDelete();
    await new Promise((resolve) => setTimeout(resolve, 2510)); // Adjust time as needed
    await getApplications();
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
  router.push(`/Admin/Applications/${selectedUser.value.id}`);
};
const closeDelete = () => {
  dialogDelete.value = false;
};
</script>