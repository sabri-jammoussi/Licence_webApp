<template>
  <v-card elevation="4">
    <SnackBar
      :key="keyToast"
      v-if="showSnackbar"
      :message="snackbarMessage"
      :showSnackBar="showSnackbar"
    />
    <EditAttribute
      :attribute="selectedAttribute"
      v-if="editDialog"
      @close-dialog="editDialog = false"
      @dataChanged="reloadData"
    />

    <v-list
      v-if="data.length > 0"
      class="custom-scrollbar"
      style="height: 350px; overflow-y: scroll; width: 100%"
    >
      <v-row>
        <v-col v-for="item in data" :key="item.intutile" cols="" sm="12" xl="1">
          <div class="attribute-list">
            <div class="d-flex align-items-center">
              <div class="p-2">
                <div class="d-flex flex-column">
                  <div class="mb-auto p-2">
                    <strong>{{ $t("intutile") }} : {{ item.intutile }}</strong>
                  </div>
                  <div class="p-2">
                    <small>Description : {{ item.description }}</small>
                  </div>
                  <div class="p-2">
                    <small>Type : {{ item.type }}</small>
                  </div>
                </div>
              </div>
              <div class="ml-auto p-2">
                <v-tooltip text="Tooltip" location="bottom">
                  <template v-slot:activator="{ props }">
                    <v-icon
                      size="small"
                      class="me-1"
                      @click="openEditDialog(item)"
                      color="green"
                      variant="tonal"
                      v-bind="props"
                    >
                      mdi-pencil
                    </v-icon>
                  </template>
                  <span>{{ $t("updateATT") }}</span>
                </v-tooltip>
                <v-tooltip text="Tooltip" location="bottom">
                  <template v-slot:activator="{ props }">
                    <v-icon
                      size="small"
                      class="ms-1"
                      @click.stop="deleteItem(item.id)"
                      color="red"
                      v-bind="props"
                    >
                      mdi-delete
                    </v-icon>
                  </template>
                  <span>{{ $t("deleteATT") }}</span>
                </v-tooltip>
              </div>
            </div>
            <v-divider></v-divider>
          </div>
        </v-col>
      </v-row>
    </v-list>
    <div v-else>
      <v-list style="height: 160px; overflow-y: scroll" class="custom-scrollbar"
        ><strong class="d-flex justify-center">{{
          $t("NoData")
        }}</strong></v-list
      >
    </div>
    <v-dialog v-model="dialogDelete" max-width="420">
      <v-card>
        <v-card-title>{{ $t("deleteconfirme") }}</v-card-title>
        <v-card-text>{{ $t("deletemsgEnum") }}</v-card-text>
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
  </v-card>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
import SnackBar from "~/components/SnackBar.vue";
import EditAttribute from "./EditAttribute.vue";
import AddAttribute from "./AddAttribute.vue";
const editedIndex = ref(-1);
const loading = ref(false);
const selectedAttribute = ref("");
const editDialog = ref(false);

const intutile = ref("");
const dialogDelete = ref(false);
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const data = ref([]);
const props = defineProps({
  attributeId: {
    type: Number,
    required: true,
  },
});
const id = props.attributeId;
onMounted(async () => {
  console.log("id from attributeList Mounted", id);
  if (!props.attributeId) return;
  await getAttributes();
  console.log("id from attributeList Mounted", id);
});
const getAttributes = async () => {
  try {
    console.log("id from AttributList", props.attributeId);
    const res = await axios.get(
      `http://localhost:5252/api/attributelicence/getattributevalue/${props.attributeId}`
    );
    data.value = res.data;
    console.log("attribute List from getVAlue ", data.value);
  } catch (error) {}
};
const reloadData = async () => {
  return await getAttributes();
};
const openEditDialog = (item) => {
  selectedAttribute.value = item;
  console.log("selected attribute  ", selectedAttribute.value);
  editDialog.value = true;
};
const deleteItem = (attributeId) => {
  editedIndex.value = attributeId;
  dialogDelete.value = true;
};
const deleteItemConfirm = async () => {
  const attributeId = editedIndex.value;

  try {
    const res = await axios.delete(
      `http://localhost:5252/api/attributelicence?id=${attributeId}`
    );
    console.log("deleting ", res.data);
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
    await getAttributes();
    //location.reload();
  }
};
const closeDelete = () => {
  dialogDelete.value = false;
};
</script>
<style>
/* Styles pour la barre de défilement */
.custom-scrollbar::-webkit-scrollbar {
  width: 1px; /* Largeur de la barre de défilement */
}

.custom-scrollbar::-webkit-scrollbar-thumb {
  background-color: #888; /* Couleur du curseur */
  border-radius: 10px; /* Rayon des coins du curseur */
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