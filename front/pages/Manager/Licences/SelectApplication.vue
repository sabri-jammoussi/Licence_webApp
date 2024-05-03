<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template v-slot:activator="{ props }">
      <v-btn color="primary" dark class="mb-2" v-bind="props">
        <v-tooltip location="bottom" activator="parent">{{
          $t("newLicence")
        }}</v-tooltip>

        <v-icon color="green" style="margin: auto">mdi-plus</v-icon>
        {{ $t("new") }}
      </v-btn>
    </template>
    <v-card>
      <v-card-text>
        <v-container>
          <v-row no-gutters>
            <v-col cols="12" sm="6" md="12">
              <div  class="grey--text text-h6 text-lg-h6">
                <v-icon left color="green" size="37">mdi-apps </v-icon>
                {{ $t("selectApp") }}
              </div>
              <v-divider></v-divider>
              <v-select
                variant="outlined"
                v-model="SelectedApp"
                label="Application"
                base-color="green"
                :items="AppName"
                item-value="id"
                item-title="nom"
                @blur="v$.SelectedApp.touch"
                @input="v$.SelectedApp.$touch"
                :error-messages="v$.SelectedApp.$errors.map((e) => e.$message)"
              ></v-select>
            </v-col>
          </v-row>
        </v-container>
      </v-card-text>
      <v-divider class="my-2"></v-divider>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn
          color="green"
          variant="text"
          @click="emitSelectedApplication"
          :loading="loading"
        >
          {{ $t("select") }}
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
  <AddLicence v-if="false" :App="SelectedApp" />
</template>
    <script setup>
import axios from "axios";
import { ref, onMounted } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import SnackBar from "~/components/SnackBar.vue";
import AddLicence from "./AddLicence.vue";
import { useRouter } from "vue-router";

const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const SelectedApp = ref("");
const dialog = ref(false);
const loading = ref(false);
const AppName = ref([]);
const router = useRouter();

const { emit } = getCurrentInstance();
const { withMessage } = helpers;

const rules = {
  SelectedApp: {
    required: withMessage("SelectedApp obligatoire", required),
  },
};
const v$ = useVuelidate(
  rules,
  {
    SelectedApp,
  },
  { $stopPropagation: true }
);
onMounted(async () => {
  await SelectApplication();
});
//stopPropagation bech thot rules 7ad page heki
watch(dialog, (val) => {
  val || close();
});

const SelectApplication = async () => {
  try {
    const response = await axios.get("http://localhost:5252/api/appliction");
    //console.log("Response data:", response.data[0].attributes[0].type);

    AppName.value = response.data.map((Application) => ({
      id: Application.id,
      nom: Application.nom,
    }));
    loading.value = false;
    // console.log("dataaaa", AppName.value);
  } catch (error) {
    console.error("Error fetching data:", error);
  }
};

const emitSelectedApplication = () => {
  if (v$.value.$pending) return;

  if (v$.value.$invalid) {
    v$.value.$touch();
    return;
  }
  // router.push('/Manager/Licences/AddLicence');
  router.push({
    name: "AddLicence",
    params: {
      // or params:
      selectedApp: SelectedApp.value,
    },
  });
  // Emit event with the selected application
  //console.log("emit data from the ListLicence : ", SelectedApp.value);
  // Close the dialog
  close();
};

const close = () => {
  // Reset form fields and validation
  (SelectedApp.value = ""), v$.value.$reset();
  dialog.value = false;
  loading.value = false;
};
</script> 