<template>
  <v-card class="card">
    <v-data-table
      :headers="headers"
      :items="formattedLicences"
      :sort-by="[{ key: 'formattedDate', order: 'asc' }]"
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
        </v-toolbar>
      </template>
    </v-data-table>
  </v-card>
</template>

<script setup>
import axios from "axios";
import { ref, onMounted, computed } from "vue";
import { useRouter } from "vue-router";
import { format } from "date-fns";
const search = ref("");
const data = ref([]);
const loading = ref(false);
const formattedLicences = ref([]);

let { t } = useI18n();

const headers = computed(() => [
  { title: t("DateExp"), key: "formattedDate" },
  { title: t("Users"), key: "user" },
  { title: "Applications", key: "applicationNom" },
  { title: t("client"), key: "clientRaison" },
]);
const getLicences = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/licence");

    const oneWeekFromNow = new Date();
    oneWeekFromNow.setDate(oneWeekFromNow.getDate() + 7); // Add 7 days to today's date
    
    const expiredLicences = response.data.filter((license) => {
      const expirationDate = new Date(license.dateExp);
      return expirationDate > new Date() && expirationDate <= oneWeekFromNow;
    });
    formattedLicences.value = expiredLicences.map((license) => ({
      ...license,
      formattedDate: format(new Date(license.dateExp), "MMMM dd, yyyy"),
    }));
  } catch (error) {
    console.error(error);
  }
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
</script>
