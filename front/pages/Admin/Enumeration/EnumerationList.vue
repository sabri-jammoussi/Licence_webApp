<template>
  <v-card elevation="5">
    <v-data-iterator :items="enums" :items-per-page="12" :search="search">
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
          <AddEnumeration />
          <EditEnumeration
            :user="selectedUser"
            v-if="editDialog"
            @close-dialog="editDialog = false"
          />
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
      <template v-slot:default="{ items, isGroupOpen, toggleGroup }">
        <v-container class="pa-2" fluid>
          <v-row>
            <v-col v-for="item in items" :key="item.code" cols="auto" md="4">
              <v-card class="pb-3" border flat>
                <!-- <v-img :src="item.raw.code"></v-img> -->

                <v-list-item class="mb-2">
                  <template v-slot:subtitle> {{ item.raw.nom }}
                  
                  </template>
                  <template v-slot:title>
                    <strong class="text-h6 mb-2"
                      >Code : {{ item.raw.code }}
                    </strong>
                  </template>
                  <!-- <template v-slot:>
                      <strong class="text-h6 mb-2">{{ item.raw.id }}</strong>
                    </template> -->

                  <v-divider></v-divider>

                  <!-- <v-expansion-panels>
                    <v-expansion-panel
                      title="Title"
                      text="Lorem ipsum dolor sit amet consectetur adipisicing elit. Commodi, ratione debitis quis est labore voluptatibus! Eaque cupiditate minima"
                    >
                    </v-expansion-panel>
                  </v-expansion-panels> -->
                  <div class="d-flex justify-space-between px-2">
                    <v-btn
                      :icon="isGroupOpen(item.raw) ? '$expand' : '$next'"
                      size="small"
                      variant="text"
                      @click="toggleGroup(item.raw)"
                    >
                    </v-btn>

                    <v-spacer></v-spacer>

                    <v-icon
                      size="small"
                      class="me-3 mt-3"
                      @click="openEditDialog(item.raw)"
                      color="green"
                      variant="tonal"
                    >
                      mdi-pencil
                    </v-icon>
                    <v-icon
                      size="small"
                      class="mt-3"
                      @click.stop="deleteItem(item.raw.id)"
                      color="red"
                    >
                      mdi-delete
                    </v-icon>
                  </div>
                  <v-divider></v-divider>
                  <div v-if="isGroupOpen(item.raw)">
                    <EnumerationValList :enumerationId="item.raw.id" />
                  </div>
                </v-list-item>

                <div class="d-flex justify-space-between px-2">
                  <div
                    class="d-flex align-center text-caption text-medium-emphasis me-1"
                  >
                    <!-- <v-icon icon="mdi-clock" start></v-icon>
  
                      <div class="text-truncate">{{ item.raw.duration }}</div> -->
                  </div>
                  <v-spacer></v-spacer>
                </div>
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
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useMyStore } from "@/store/index.js";
import AddEnumeration from "./AddEnumeration.vue";
import EditEnumeration from "./EditEnumeration.vue";
import EnumerationValList from "../EnumerationValeur/EnumerationValList.vue";
const selectedUser = ref("");

const search = ref("");
const dialogDelete = ref(false);
const editDialog = ref(false);
const loading = ref(false);
const store = useMyStore();
const enums = computed(() => store.enums);
const editedIndex = ref(-1);

const deleteItem = (utilisateurId) => {
  editedIndex.value = utilisateurId;
  dialogDelete.value = true;
};
const deleteItemConfirm = async () => {
  const utilisateurId = editedIndex.value;

  try {
    const token = window.localStorage.getItem("token");
    if (token) {
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    } else {
      console.log("unauthorized");
      alert("unauthorized");
    }
    await axios.delete(
      `http://localhost:5252/api/enumeration/${utilisateurId}`
    );
    loading.value = true;
    try {
      await store.getEnumerations();
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
const closeDelete = () => {
  dialogDelete.value = false;
};
const openEditDialog = (item) => {
  selectedUser.value = item;
  console.log("selected user 2 ", selectedUser.value);
  editDialog.value = true;
};

onMounted(async () => {
  try {
    await store.getEnumerations();
    if (selectedUser.value) {
      provide("enumerationId", selectedUser.value.id);
    }
  } catch (error) {
    console.error(error);
  }
});
</script>
