<template>
    <v-data-table
      :headers="headers"
      :items="apps"
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
              single-line
              class="justify-content-start"
            ></v-text-field>
          </v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
        </v-toolbar>
      </template>
    </v-data-table>
  </template>
    
    <script setup>
  import axios from "axios";
  import { ref, onMounted } from "vue";
  import { useMyStore } from "@/store/index.js";
  import { useRouter } from "vue-router";
  const selectedUser = ref("");
  const store = useMyStore();
  const editDialog = ref(false);
  const dialogDelete = ref(false);
  const router = useRouter();
  const search = ref("");
  const loading = ref(false);
  let { t } = useI18n();
  const apps = computed(() => store.apps);
  
  const headers = computed(() => [
    { title: t("identifier"), key: "identifiant" },
    { title: t("name"), key: "nom" },
    { title: "Description", key: "description" },
  ]);
  
  const editedIndex = ref(-1);
  onMounted(async () => {
    // console.log('selected user ', selectedUser.value);
    loading.value = true;
    try {
      await store.getApplications();
    } catch (error) {
      console.error(error);
    } finally {
      loading.value = false;
    }
  });
  </script>
