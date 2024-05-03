<template>
  <div>
    <SnackBar
      :key="keyToast"
      v-if="showSnackbar"
      :message="snackbarMessage"
      :showSnackBar="showSnackbar"
    />
    <v-list
      v-if="data.length > 0"
      class="custom-scrollbar"
      style="height: 160px; overflow-y: scroll "
    >
      <EditEnumVal
        :user="selectedUser"
        @dataChanged="reloadData"
        v-if="editDialog"
        @close-dialog="editDialog = false"
      />

      <template v-for="item in data" :key="item.id">
        <v-list-item color="primary" rounded="shaped">
          <v-list-item-title>{{ item.valeur }}</v-list-item-title>
          <template v-slot:append>
            <v-tooltip text="Tooltip" location="bottom">
              <template v-slot:activator="{ props }">
                <v-icon
                  size="small"
                  class="me-3 mt-3"
                  @click="openEditDialog(item)"
                  color="green"
                  variant="tonal"
                  v-bind="props"
                >
                  mdi-pencil
                </v-icon>
              </template>
              <span>{{ $t("updateEnumVal") }}</span>
            </v-tooltip>
            <v-tooltip text="Tooltip" location="bottom">
              <template v-slot:activator="{ props }">
                <v-icon
                  size="small"
                  class="mt-3"
                  @click.stop="deleteItem(item.id)"
                  color="red"
                  v-bind="props"
                >
                  mdi-delete
                </v-icon>
              </template>
              <span>{{ $t("deleteEnumVal") }}</span>
            </v-tooltip>
          </template>
        </v-list-item>
      </template>

      <v-dialog v-model="dialogDelete" max-width="420">
        <v-card>
          <v-card-title>{{ $t("deleteconfirme") }}</v-card-title>
          <v-card-text>{{ $t("deletemsgEnumVAl") }}</v-card-text>
          <v-divider class="my-2"></v-divider>

          <v-card-actions>
            <v-spacer></v-spacer>

            <v-btn color="red" text @click="deleteItemConfirm">{{
              $t("delete")
            }}</v-btn>
            <v-btn color="grey" text @click="closeDelete">{{
              $t("cancel")
            }}</v-btn>
          </v-card-actions>
        </v-card>
      </v-dialog>
    </v-list>
    <div v-else>
      <v-list style="height: 160px; overflow-y: scroll" class="custom-scrollbar"
        ><strong class="d-flex justify-center">{{
          $t("NoData")
        }}</strong></v-list
      >
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, defineProps } from "vue";
import axios from "axios";
import EditEnumVal from "./EditEnumVal.vue";
import SnackBar from "~/components/SnackBar.vue";

const dialogDelete = ref(false);
const loading = ref(false);
const editDialog = ref(false);
const selectedUser = ref("");
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);

const props = defineProps({
  enumerationId: {
    type: Number,
    required: true,
  },
});

const data = ref([]);

onMounted(async () => {
  await getEnumVal();
});

const getEnumVal = async () => {
  try {
    console.log(props.enumerationId);
    const response = await axios.get(
      `http://localhost:5252/api/enumerationvaleur/getenumval/${props.enumerationId}`
    );
    console.log(response)
    data.value = response.data;
    console.log(data.value);
  } catch (error) {
    //console.error(error);
  }
};

const editedIndex = ref(-1);

const deleteItem = (utilisateurId) => {
  editedIndex.value = utilisateurId;
  dialogDelete.value = true;
};

const deleteItemConfirm = async () => {
  const utilisateurId = editedIndex.value;

  try {
    await axios.delete(
      `http://localhost:5252/api/enumerationvaleur?id=${utilisateurId}`
    );
    loading.value = true;
    try {
      showSnackbar.value = true;
      keyToast.value++;
      snackbarMessage.value = "Item deleted successfully.";
    } catch (error) {
      console.error(error);
    } finally {
      loading.value = false;
    }
  } catch (err) {
    console.error(err);
  } finally {
    closeDelete();
    await new Promise((resolve) => setTimeout(resolve, 2510)); // Adjust time as needed
    await getEnumVal();
  }
};

const reloadData = async () => {
  try {
    await getEnumVal();
  } catch (error) {
    console.error("Error reloading data:", error);
  }
};

const closeDelete = () => {
  dialogDelete.value = false;
};

const openEditDialog = (item) => {
  selectedUser.value = item;
  console.log("selected user  ", selectedUser.value);
  editDialog.value = true;
};
</script>
<style>

/* Styles pour la barre de défilement */
.custom-scrollbar::-webkit-scrollbar {
  width: 1px; /* Largeur de la barre de défilement */
}

.custom-scrollbar::-webkit-scrollbar-thumb {
  background-color: #888; /* Couleur du curseur */
  border-radius: 5px; /* Rayon des coins du curseur */
}

.custom-scrollbar::-webkit-scrollbar-track {
  background-color: #f1f1f1; /* Couleur de l'arrière-plan de la barre de défilement */
}

/* Pour Firefox et autres navigateurs */
.custom-scrollbar {
  scrollbar-width: thin;
  scrollbar-color: #888 #f1f1f1;
}
</style>

  