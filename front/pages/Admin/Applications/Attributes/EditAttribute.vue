<template>
  <v-dialog v-model="editDialog" max-width="500px">
    <v-card>
      <v-card-text>
        <v-container>
          <v-spacer></v-spacer>
          <v-row no-gutters>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                base-color="green"
                :label="$t('intutile')"
                variant="outlined"
                v-model="intutile"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                base-color="green"
                variant="outlined"
                label="Description"
                v-model="description"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
              <v-select
                variant="outlined"
                v-model="type"
                label="Type"
                base-color="green"
                :items="TypeOptions"
                @blur="v$.type.touch"
                @input="v$.type.$touch"
                :error-messages="v$.type.$errors.map((e) => e.$message)"
              ></v-select>
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
          @click="updateAttribute"
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
    :message="snackbarMessage"
    :showSnackBar="showSnackbar"
  />
</template>
  <script setup>
import { ref, onMounted } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import axios from "axios";
import SnackBar from "~/components/SnackBar.vue";
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const { emit } = getCurrentInstance();
const { withMessage } = helpers;
const editDialog = ref(false);
const loading = ref(false);
const intutile = ref("");
const description = ref("");
const type = ref("");
const id = ref("");
const TypeOptions = ref([]);

defineEmits(["dataChanged", "close-dialog"]);
const props = defineProps({
  attribute: {
    type: Object,
    default: null,
  },
});
const rules = {
  intutile: {
    required: withMessage("valeur obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  description: {
    required: withMessage("valeur obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  type: {
    required: withMessage("valeur obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
};
const v$ = useVuelidate(
  rules,
  {
    intutile,
    description,
    type,
  },
  { $stopPropagation: true }
);
watch(editDialog, (val) => {
  val || close();
});

const updateAttribute = async () => {
  loading.value = true;
  v$.value.$touch();
  if (v$.value.$pending || v$.value.$error) {
    loading.value = false;
  } else {
    setTimeout(async () => {
      try {
        const userId = id.value;
        console.log("idddddd", userId);
        const data = {
          id: userId,
          intutile: intutile.value,
          description: description.value,
          type: type.value,
        };
        const response = await axios.post(
          `http://localhost:5252/api/attributelicence/update`,
          data
        );
        if (response.status >= 200 && response.status < 300) {
          showSnackbar.value = true;
          keyToast.value++;
          snackbarMessage.value = "Edited successfully.";
          // router.push('/users/');
        } else {
          alert("erorrooor", response.message);
        }
        emit("dataChanged");
        loading.value = false;
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        close();
      }
    }, 1500);
  }
};
onMounted(async () => {
  //console.log("edit dialog", props);
  if (props.attribute) {
    intutile.value = props.attribute.intutile;
    description.value = props.attribute.description;
    type.value = props.attribute.type;
    id.value = props.attribute.id;
    editDialog.value = true;

    //console.log("props.attribute.id", props.attribute.id);
  }
  await getAttributeType();
  //await store.getEnumerations();
});
const getAttributeType = async () => {
  try {
    const response = await axios.get(
      "http://localhost:5252/api/attributelicence/type"
    );
    TypeOptions.value = response.data.types;
   // console.log("type from edit ", TypeOptions.value);
  } catch (error) {
    //console.error(error);
  }
};
const close = () => {
  v$.value.$reset();
  editDialog.value = false;
  emit("close-dialog");
};
</script>