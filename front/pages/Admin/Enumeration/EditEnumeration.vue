<template>
  <v-dialog v-model="editDialog" max-width="500px">
    <v-card>
      <v-card-text>
        <v-container>
          <v-spacer></v-spacer>
          <v-row no-gutters>
            <v-col cols="12" sm="4" md="12">
              <v-text-field
                variant="outlined"
                base-color="green"
                label="Code"
                v-model="code"
                @blur="v$.code.touch"
                @input="v$.code.$touch"
                :error-messages="v$.code.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="4" md="12">
              <v-text-field
                variant="outlined"
                v-model="nom"
                :label="$t('name')"
                base-color="green"
                @blur="v$.nom.touch"
                @input="v$.nom.$touch"
                :error-messages="v$.nom.$errors.map((e) => e.$message)"
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
          @click="updateEnumeration"
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
</template>
    <script setup>
import { ref, onMounted } from "vue";
import { useMyStore } from "@/store/index.js";
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import axios from "axios";

const { emit } = getCurrentInstance();
const { withMessage } = helpers;
const editDialog = ref(false);
const store = useMyStore();
const loading = ref(false);
const code = ref("");
const nom = ref("");
const id = ref("");
const props = defineProps(["user"]);

const rules = {
  code: {
    required: withMessage("code obligatoire", required),
    min: withMessage("Min 13 caractères", minLength(3)),
  },
  nom: {
    required: withMessage("Nom obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
};
const v$ = useVuelidate(
  rules,
  {
    code,
    nom,
  },
  { $stopPropagation: true }
);
watch(editDialog, (val) => {
  val || close();
});

const updateEnumeration = async () => {
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
          code: code.value,
          nom: nom.value,
        };
        const token = window.localStorage.getItem("token");
        if (token) {
          // If there is a token, set the authorization header
          axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
          //console.log('Token checked:', axios.defaults.headers.common);
        } else {
          console.log("unauthorized");
          alert("unauthorized");
        }
        const response = await axios.post(
          `http://localhost:5252/api/enumeration/update`,
          data
        );
        if (response.status >= 200 && response.status < 300) {
          // router.push('/users/');
        } else {
          alert("erorrooor", response.message);
        }
        loading.value = false;
        await store.getEnumerations();
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        close();
      }
    }, 1500);
  }
};
onMounted(async () => {
  console.log("edit dialog", props);
  if (props.user) {
    code.value = props.user.code;
    nom.value = props.user.nom;
    id.value = props.user.id;
    editDialog.value = true;
    console.log("props.user.id", props.user.id);
  }
  await store.getEnumerations();
});
const close = () => {
  v$.value.$reset();
  editDialog.value = false;
  emit("close-dialog");
};
</script>