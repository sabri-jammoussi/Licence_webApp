<template>
  <SnackBar
    :key="keyToast"
    :message="snackbarMessage"
    :showSnackBar="showSnackbar"
  />
  <v-dialog v-model="addDialog" max-width="500px">
    <!-- <template v-slot:activator="{ props }">
      <v-btn icon class="mb-2" v-bind="props">
        <v-icon color="blue">mdi-plus</v-icon>
      </v-btn>
    </template> -->
    <v-card>
      <v-card-text>
        <v-container>
          <v-row no-gutters>
            <v-col cols="12" sm="4" md="12">
              <v-text-field
                variant="outlined"
                base-color="green"
                label="Valeur"
                v-model="valeur"
                @blur="v$.valeur.touch"
                @input="v$.valeur.$touch"
                :error-messages="v$.valeur.$errors.map((e) => e.$message)"
              ></v-text-field>
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
          @click="addEnumVAl"
          :loading="loading"
        >
          {{ $t("add") }}
        </v-btn>
        <v-btn color="grey" variant="text" @click="close">
          {{ $t("cancel") }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script setup>
const { emit } = getCurrentInstance();

import axios from "axios";
import { ref } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import SnackBar from "~/components/SnackBar.vue";

const valeur = ref("");
const addDialog = ref(false);
const loading = ref(false);
const { withMessage } = helpers;
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const props = defineProps({
  enumId: {
    type: Number,
    required: true,
  },
  showDialog: {
    type: Boolean,
    required: true,
  },
});
defineEmits(["reload-data", "close-dialog"]);

const rules = {
  valeur: {
    required: withMessage("valeur obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
};
const v$ = useVuelidate(
  rules,
  {
    valeur,
  },
  { $stopPropagation: true }
);
//stopPropagation bech thot rules 7ad page heki
watch(
  () => addDialog.value,
  (val) => {
    val || close();
  }
);

// watch bech ki nenzel bara mel dialog yakraha
// const getEnumVall = async () => {
//    try {
//      const id = props.enumId;
//      console.log('idd',id);
//      const response = await axios.get(
//        `http://localhost:5252/api/enumerationvaleur/getenumval/${id}`
//     );
//     data.value = response.data;
//     console.log(data);
//   } catch (error) {
//     console.error(error);
//   }
// };
const addEnumVAl = () => {
  loading.value = true;
  // Clear previous validation errors
  v$.value.$reset();
  // Validate the form
  v$.value.$touch();

  if (v$.value.$invalid) {
    loading.value = false;
  } else {
    setTimeout(async () => {
      try {
        const data = {
          valeur: valeur.value,
          enumerationId: props.enumId,
        };
        //console.log("idd from addenum", props.enumId);
        const response = await axios.post(
          "http://localhost:5252/api/enumerationvaleur",
          data
        );
        // await store.getApplications();
        if (response.status >= 200 && response.status < 300) {
          showSnackbar.value = true;
          keyToast.value++;
          snackbarMessage.value = "Added successfully.";
          // router.push('/users/');
        } else {
          alert("erorrooor", response.message);
        }
       // emit("reload-data");
        //await emit("reloadData");
        //await getEnumVall();
        loading.value = false;
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        emit("reload-data");

        await new Promise((resolve) => setTimeout(resolve, 2510)); // Adjust time as needed
        close();



        //await props.reloadData();
      }
    }, 1500);
  }
};
const close = () => {
  // Reset form fields and validation
  (valeur.value = ""), v$.value.$reset();
  addDialog.value = false;
  loading.value = false;
  emit("close-dialog");
};

onMounted(() => {
  //   //console.log("add", props);
  //console.log("props.user.id", props.enumId);

  addDialog.value = props.showDialog || false;

  //dialog.value=true;
  //console.log('enumid',props.enumId);
});
</script> 