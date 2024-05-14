<template>
  <v-dialog v-model="editDialog" max-width="500px">
    <v-card>
      <div class="grey--text text-h6 text-lg-h6 mt-4">
        <v-icon left color="green" size="35" class="ml-2">mdi-key </v-icon>
        {{ $t("editLicenses") }}
      </div>
      <v-divider></v-divider>
      <v-card-text>
        <v-container>
          <v-spacer></v-spacer>
          <v-row>
            <v-col cols="12" sm="6" md="6">
              <v-menu v-model="isMenuOpen" :close-on-content-click="false">
                <template v-slot:activator="{ props }">
                  <v-text-field
                    variant="outlined"
                    base-color="green"
                    label="Selected date"
                    :model-value="formattedDate"
                    readonly
                    v-bind="props"
                  ></v-text-field>
                </template>
                <v-date-picker
                  v-model="selectedDate"
                  :min="minDate"
                ></v-date-picker>
              </v-menu>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-select
                variant="outlined"
                v-model="selectedClient"
                label="Clients"
                base-color="green"
                item-title="raisonSocial"
                :items="ClientsData"
                item-value="id"
                @blur="v$.selectedClient.touch"
                @input="v$.selectedClient.$touch"
                :error-messages="
                  v$.selectedClient.$errors.map((e) => e.$message)
                "
              ></v-select>
            </v-col>
            <v-col cols="12" md="12">
              <v-select
                variant="outlined"
                v-model="selectedPartenaire"
                :label="t('partner')"
                base-color="green"
                item-title="raisonSocial"
                :items="PartenairesData"
                item-value="id"
              ></v-select>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                v-model="AppName"
                label="Application"
                :disabled="true"
                base-color="green"
                variant="outlined"
                item-title="aaafdl,"
                item-value="selectedApps"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                v-model="userFirstName"
                :disabled="true"
                label="User"
                base-color="green"
                variant="outlined"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
      <v-divider class="my-2"></v-divider>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="blue-darken-1"
          variant="text"
          @click="UpdateLicence"
          :loading="loading"
        >
          {{ $t("edit") }}
        </v-btn>
        <v-btn color="grey" variant="text" @click="close">
          {{ $t("cancel") }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
  <SnackBar
    :key="keyToast"
    v-if="showSnackbar"
    :message="snackbarMessage"
    :showSnackBar="showSnackbar"
  />
</template>
<script setup>
import { ref, computed, onMounted } from "vue";
import axios from "axios";
import { required, helpers } from "@vuelidate/validators";
import { useVuelidate } from "@vuelidate/core";
import SnackBar from "~/components/SnackBar.vue";

const isMenuOpen = ref(false);
const { withMessage } = helpers;
const { emit } = getCurrentInstance();
const selectedPartenaire = ref("");
const PartenairesData = ref([]);

const selectedDate = ref("");
const AppName = ref("");
const ClientsData = ref([]);
const selectedClient = ref("");
const userFirstName = ref("");
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const loading = ref(false);
const editDialog = ref(false);
let { t } = useI18n();

const props = defineProps({
  Licence: {
    type: Object,
    required: true,
  },
});
const id = ref("");

watch(editDialog, (val) => {
  val || close();
});

const rules = {
  selectedClient: {
    required: withMessage("Client obligatoire", required),
  },
};
const v$ = useVuelidate(
  rules,
  {
    selectedClient,
  },
  { $stopPropagation: true }
);
const minDate = new Date();
watch(
  () => props.Licence.dateExp,
  (newDateExp) => {
    // Update only the year while keeping month and day unchanged
    const year = selectedDate.value.getFullYear();
    const month = selectedDate.value.getMonth();
    const day = selectedDate.value.getDate();
    const newDate = new Date(newDateExp);
    newDate.setFullYear(year);
    newDate.setMonth(month);
    newDate.setDate(day);
    selectedDate.value = newDate;
  }
);
const formattedDate = computed(() => {
  const date = selectedDate.value;
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
});

const getClients = async () => {
  try {
    const response = await axios.get(`http://localhost:5252/api/client`);
    // console.log("response of clients ", response.data);
    ClientsData.value = response.data.map((Application) => ({
      id: Application.id,
      raisonSocial: Application.raisonSocial,
    }));
    //console.log("data of client id and raisonSocial ! ", ClientsData.value);
    //AppName.value = response.data.nom;
  } catch (error) {
    console.error(error);
  }
};
const getPartenaires = async () => {
  try {
    const response = await axios.get(`http://localhost:5252/api/partenaire`);
    PartenairesData.value = response.data.map((partenaire) => ({
      id: partenaire.id,
      raisonSocial: partenaire.raisonSocial,
    }));
    console.log("testt", PartenairesData.value);
  } catch (error) {
    console.error(error);
  }
};
const UpdateLicence = async () => {
  loading.value = true;
  v$.value.$touch();
  if (v$.value.$pending || v$.value.$error) {
    loading.value = false;
  } else {
    setTimeout(async () => {
      try {
        const userId = id.value;
        console.log("idddddd", userId);
        // If selectedPartenaire is an empty string, set it to null or 0
        let partenaireId = selectedPartenaire.value;
        if (partenaireId === "") {
          partenaireId = null; // or partenaireId = 0;
        }
        
        const data = {
          id: userId,
          dateExp: selectedDate.value,
          clientId: selectedClient.value,
          partenaireId: partenaireId,
        };
        const response = await axios.post(
          `http://localhost:5252/api/licence/update`,
          data
        );
        if (response.status >= 200 && response.status < 300) {
          // router.push('/users/');
          showSnackbar.value = true;
          keyToast.value++;
          snackbarMessage.value = "Added successfully.";
        } else {
          alert("erorrooor", response.message);
        }
        loading.value = false;
        emit("dataChanged");
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        await new Promise((resolve) => setTimeout(resolve, 2510));
        close();
      }
    }, 1500);
  }
};
onMounted(async () => {
  console.log("edit dialog", props);
  await getClients();
  await getPartenaires();
  if (props.Licence) {
    AppName.value = props.Licence.applicationNom;
    userFirstName.value = props.Licence.user;
    selectedDate.value = new Date(props.Licence.dateExp); //   (email.value = props.Licence.email),

    //   (ville.value = props.Licence.ville),
    //   (adresse.value = props.Licence.adresse),
    //   (codePostal.value = props.Licence.codePostal),
    //   (pays.value = props.Licence.pays),
    id.value = props.Licence.id;
    editDialog.value = true;
    console.log("props.Licence.id", props.Licence.id);
  }
  //await store.ReadRoles();
});
const close = () => {
  v$.value.$reset();
  editDialog.value = false;
  emit("close-dialog");
};
</script>
