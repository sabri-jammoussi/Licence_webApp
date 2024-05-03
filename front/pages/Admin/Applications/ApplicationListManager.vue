<template >
  <div class="back">
    <v-data-table
    class="background-table"
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
              clearable
              hide-details
              single-line
              class="justify-content-start"
            ></v-text-field>
          </v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
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
          <span>{{ $t("ConsultApp") }}</span>
        </v-tooltip>
      </template>
    </v-data-table>
  </div>
</template>
    
    <script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import { useRouter } from "vue-router";

const data = ref([]);
const search = ref("");
const loading = ref(false);
const selectedUser = ref("");
const router = useRouter();
let { t } = useI18n();

const headers = computed(() => [
  { title: t("identifier"), key: "identifiant" },
  { title: t("name"), key: "nom" },
  { title: "Description", key: "description" },
  { title: "Actions", key: "actions", sortable: false },
]);
const getApplications = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/appliction");

    data.value = response.data;
    console.log(data);
  } catch (error) {
    console.error(error);
  }
};
const consulter = (item) => {
  selectedUser.value = item;
  console.log("idd from application ", selectedUser.value.id);
  router.push(`/Admin/Applications/${selectedUser.value.id}`);
};
onMounted(async () => {
  // console.log('selected user ', selectedUser.value);
  loading.value = true;
  try {
    getApplications();
  } catch (error) {
    console.error(error);
  } finally {
    loading.value = false;
  }
});
</script>
<style>
.back {
  color: black;
}
</style>