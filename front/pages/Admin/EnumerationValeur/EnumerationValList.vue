<template>
  <div>
    <SnackBar
      :key="keyToast"
      v-if="showSnackbar"
      :message="snackbarMessage"
      :showSnackBar="showSnackbar"
    />
    <v-list v-if="data.length > 0">
      <EditEnumVal
        :user="selectedUser"
        @dataChanged="reloadData"
        v-if="editDialog"
        @close-dialog="editDialog = false"
      />

      <v-virtual-scroll :items="data" height="150">
        <template #default="{ item }">
          <v-list-item
            :key="item.id"
            :value="item"
            color="primary"
            rounded="shaped"
          >
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
      </v-virtual-scroll>

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
      <strong class="justify-center">{{ $t("NoData") }}</strong>

      <v-virtual-scroll height="150"> </v-virtual-scroll>
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
    const response = await axios.get(
      `http://localhost:5252/api/enumerationvaleur/getenumval/${props.enumerationId}`
    );
    data.value = response.data;
    // console.log(data);
  } catch (error) {
    console.error(error);
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