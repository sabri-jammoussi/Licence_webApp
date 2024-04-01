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
                variant="outlined"
                :label="$t('identifier')"
                v-model="identifiant"
                @blur="v$.identifiant.touch"
                @input="v$.identifiant.$touch"
                :error-messages="v$.identifiant.$errors.map((e) => e.$message)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="12">
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
            <v-col cols="12" sm="6" md="12">
              <v-text-field
                variant="outlined"
                v-model="description"
                label="Description"
                base-color="green"
                @blur="v$.description.touch"
                @input="v$.description.$touch"
                :error-messages="v$.description.$errors.map((e) => e.$message)"
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
          @click="updateApplication"
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
import { useVuelidate } from "@vuelidate/core";
import { required, helpers, minLength } from "@vuelidate/validators";
import axios from "axios";

const { emit } = getCurrentInstance();
const { withMessage } = helpers;
const editDialog = ref(false);
const loading = ref(false);
const identifiant = ref("");
const nom = ref("");
const description = ref("");
const id = ref("");
const props = defineProps(["user"]);

const rules = {
  identifiant: {
    required: withMessage("Identifiant obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  nom: {
    required: withMessage("Nom obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
  description: {
    required: withMessage("description obligatoire", required),
    min: withMessage("Min 3 caractères", minLength(3)),
  },
};
const v$ = useVuelidate(
  rules,
  {
    identifiant,
    nom,
    description,
  },
  { $stopPropagation: true }
);
watch(editDialog, (val) => {
  val || close();
});

const updateApplication = async () => {
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
          identifiant: identifiant.value,
          nom: nom.value,
          description: description.value,
        };
        const response = await axios.post(
          `http://localhost:5252/api/appliction/update`,
          data
        );
        if (response.status >= 200 && response.status < 300) {
          // router.push('/users/');
        } else {
          alert("erorrooor", response.message);
        }
        loading.value = false;
        emit("dataChanged");
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        close();
      }
    }, 1500);
  }
};
onMounted(async () => {
  console.log("edit dialog",props)
  if (props.user) {
    identifiant.value = props.user.identifiant;
    nom.value = props.user.nom;
    description.value = props.user.description;
    id.value = props.user.id;
    editDialog.value = true;
    console.log("props.user.id", props.user.id);
  }
  await store.ReadRoles();
});
const close = () => {
  v$.value.$reset();
  editDialog.value = false;
  emit("close-dialog");
};
</script>