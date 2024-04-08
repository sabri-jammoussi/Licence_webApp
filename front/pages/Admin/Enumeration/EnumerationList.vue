<template>
  <SnackBar
    :key="keyToast"
    v-if="showSnackbar"
    :message="snackbarMessage"
    :showSnackBar="showSnackbar"
  />
  <v-card elevation="4">
    <v-data-iterator :items="data" :items-per-page="12" :search="search">
      <template v-slot:header>
        <v-toolbar flat>
          <v-toolbar-title>
            <v-text-field
              v-model="search"
              density="compact"
              :label="$t('search')"
              prepend-inner-icon="mdi-magnify"
              variant="solo-filled"
              flat
              single-line
              clearable
              hide-details
              class="justify-content-start"
            ></v-text-field>
          </v-toolbar-title>
          <AddEnumeration @dataChanged="reloadData" />
          <EditEnumeration
            :user="selectedUser"
            v-if="editDialog"
            @close-dialog="editDialog = false"
            @dataChanged="reloadData"
          />
          <AddEnumVal
            v-if="addDialog"
            :showDialog="addDialog"
            :enumId="selectedID"
            @close-dialog="addDialog = false"
            @reload-data="reloadEnumValues"
          />
          <v-dialog v-model="dialogDelete" max-width="420">
            <v-card>
              <v-card-title>{{ $t("deleteconfirme") }}</v-card-title>
              <v-card-text>{{ $t("deletemsgEnum") }}</v-card-text>
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
      <template v-slot:default="{ items }">
        <v-container class="pa-" fluid>
          <v-row>
            <v-col v-for="item in items" :key="item.code" cols="auto" md="4">
              <v-card class="pb-3" border flat>
                <!-- <v-img :src="item.raw.code"></v-img> -->
                <v-list-item class="mb-2" max-height="350">
                  <div class="d-flex align-items-center">
                    <div class="p-2">
                      <div class="d-flex flex-column">
                        <strong>{{ $t("name") }} : {{ item.raw.nom }}</strong>
                        <small>Code : {{ item.raw.code }}</small>

                        <!-- <div class="p-2">Flex item 3</div> -->
                      </div>
                    </div>

                    <div class="ml-auto p-2">
                      <v-tooltip text="Tooltip" location="bottom">
                        <template v-slot:activator="{ props }">
                          <v-icon
                            color="blue"
                            variant="tonal"
                            class="me-1"
                            @click="openAddDialog(item.raw.id)"
                            v-bind="props"
                          >
                            mdi-plus
                          </v-icon>
                        </template>
                        <span>{{ $t("newEnumVal") }}</span>
                      </v-tooltip>
                      <v-tooltip text="Tooltip" location="bottom">
                        <template v-slot:activator="{ props }">
                          <v-icon
                            size="small"
                            class="me-1"
                            @click="openEditDialog(item.raw)"
                            color="green"
                            variant="tonal"
                            v-bind="props"
                          >
                            mdi-pencil
                          </v-icon>
                        </template>
                        <span>{{ $t("updateEnum") }}</span>
                      </v-tooltip>
                      <v-tooltip text="Tooltip" location="bottom">
                        <template v-slot:activator="{ props }">
                          <v-icon
                            size="small"
                            class="ms-1"
                            @click.stop="deleteItem(item.raw.id)"
                            color="red"
                            v-bind="props"
                          >
                            mdi-delete
                          </v-icon>
                        </template>
                        <span>{{ $t("deleteEnum") }}</span>
                      </v-tooltip>
                    </div>
                  </div>
                  <v-divider></v-divider>
                  <div>
                    <EnumerationValList
                      :key="updateDataValues"
                      :enumerationId="item.raw.id"
                    />
                    <!-- el key bech T3awed tchargi les données  -->
                  </div>
                </v-list-item>
              </v-card>
            </v-col>
          </v-row>
        </v-container>
      </template>

      <template v-slot:footer="{ page, pageCount, prevPage, nextPage }">
        <div class="d-flex align-center justify-center pa-4">
          <v-btn
            :disabled="page === 1"
            density="comfortable"
            icon="mdi-arrow-left"
            variant="tonal"
            rounded
            @click="prevPage"
          ></v-btn>

          <div class="mx-2 text-caption">
            Page {{ page }} of {{ pageCount }}
          </div>

          <v-btn
            :disabled="page >= pageCount"
            density="comfortable"
            icon="mdi-arrow-right"
            variant="tonal"
            rounded
            @click="nextPage"
          ></v-btn>
        </div>
      </template>
    </v-data-iterator>
  </v-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import AddEnumeration from "./AddEnumeration.vue";
import EditEnumeration from "./EditEnumeration.vue";
import EnumerationValList from "../EnumerationValeur/EnumerationValList.vue";
import AddEnumVal from "../EnumerationValeur/AddEnumVal.vue";
import SnackBar from "~/components/SnackBar.vue";

const selectedUser = ref("");
const selectedID = ref("");
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const search = ref("");
const dialogDelete = ref(false);
const editDialog = ref(false);
const addDialog = ref(false);

const loading = ref(false);
const editedIndex = ref(-1);
const data = ref([]);
const updateDataValues = ref(0);
const deleteItem = (utilisateurId) => {
  editedIndex.value = utilisateurId;
  dialogDelete.value = true;
};
const deleteItemConfirm = async () => {
  const utilisateurId = editedIndex.value;

  try {
    await axios.delete(
      `http://localhost:5252/api/enumeration/${utilisateurId}`
    );

    loading.value = true;
    try {
      showSnackbar.value = true;
      keyToast.value++;
      snackbarMessage.value = "Item deleted successfully.";
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
    await getEnumerations();
  }
};
const closeDelete = () => {
  dialogDelete.value = false;
};
const openEditDialog = (item) => {
  selectedUser.value = item;
  console.log("selected user  ", selectedUser.value);
  editDialog.value = true;
};
const openAddDialog = (item) => {
  //console.log("Selected from the add ", item);
  selectedID.value = item;
  //console.log("Selected from the add 22", selectedID.value);

  addDialog.value = true;
};
const getEnumerations = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/enumeration");

    data.value = response.data;
    //console.log(data);
  } catch (error) {
    console.error(error);
  }
};
const reloadEnumValues = async () => {
  try {
    // console.log("reloadEnumValues", selectedID.value);
    const id = selectedID.value;
    // console.log("idd", id);
    const response = await axios.get(
      `http://localhost:5252/api/enumerationvaleur/getenumval/${id}`
    );
    updateDataValues.value++;
  } catch (error) {
    console.error(error);
  }
};

const reloadData = async () => {
  return await getEnumerations();
};

onMounted(async () => {
  try {
    await getEnumerations();
    if (selectedUser.value) {
      provide("enumerationId", selectedUser.value.id);
    }
  } catch (error) {
    console.error(error);
  }
});
</script>
