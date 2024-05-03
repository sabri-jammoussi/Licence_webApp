<template>
  <v-row v-if="items && items.length">
    <v-col
      v-for="item in items"
      :key="item.title.replace(' ', '_')"
      cols="6"
      sm="3"
    >
      <v-card height="100%" class="card">
        <v-container fluid>
          <h3>{{ item.title }}</h3>
          <v-row height="100%">
            <v-col>
              <h4
                class="grey--text text-h4 text-lg-h4 font-weight-bold lh-normal"
              >
                {{ item.summary }}
              </h4>
              <h6 class="font-weight-normal grey--text">{{ item.total }}</h6>
            </v-col>
            <v-col align-self="center" align="center">
              <v-icon size="60" :color="item.color">{{ item.icon }}</v-icon>
            </v-col>
          </v-row>
        </v-container>
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
import axios from "axios";
const dataUsers = ref([]);
const dataApplications = ref([]);
const dataEnumerations = ref([]);
const countedUsers = computed(() => dataUsers.value.length);
const countedApplications = computed(() => dataApplications.value.length);
const countedEnumerations = computed(() => dataEnumerations.value.length);
let { t } = useI18n();

onMounted(async () => {
  await getUsers();
  await getApplications();
  await getEnumerations();
});
const getUsers = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/Users");

    dataUsers.value = response.data;
    // console.log(data);
  } catch (error) {
    console.error(error);
  }
};
const getApplications = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/appliction");

    dataApplications.value = response.data;
    // console.log(data);
  } catch (error) {
    console.error(error);
  }
};
const getEnumerations = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/enumeration");

    dataEnumerations.value = response.data;
    //console.log(data);
  } catch (error) {
    console.error(error);
  }
};
const items = computed(() =>[
  {
    title: t("Users"),
    summary: countedUsers,
    total: "+23% Up",
    color: "green",
    icon: "mdi-account-box-outline",
  },
  {
    title: "Applications",
    summary: countedApplications,
    total: "-3% Down",
    color: "warning",
    icon: "mdi-view-dashboard",
  },
  {
    title: t("Enumerations"),
    summary: countedEnumerations,
    total: "-10% Down",
    color: "red",
    icon: "mdi-slack",
  },
  {
    title: "Views Summary",
    summary: "4124",
    total: "+50% Up",
    color: "orange",
    icon: "mdi-gesture-double-tap",
  },
]);
</script>
