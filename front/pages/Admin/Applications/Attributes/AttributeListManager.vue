<template>
  <v-card>
    <v-data-table
      :items-per-page="5"
      :headers="headers"
      :items="data"
      :search="search"
      :loading="loading"
      v-if="data.length > 0"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-toolbar-title>
            <div class="d-flex">
              <div class="p-2">
                {{ $t("listATT") }}
                ({{ countedAttribut }})
              </div>
              <v-text-field
                v-model="search"
                density="compact"
                :label="$t('search')"
                prepend-inner-icon="mdi-magnify"
                variant="solo-filled"
                flat
                hide-details
                clearable
                single-line
                class="justify-content-start"
              ></v-text-field>
            </div>
          </v-toolbar-title>
          <v-divider class="mx-4" inset vertical></v-divider>
   
        </v-toolbar>
      </template>
    </v-data-table>
    <v-data-table v-else>
      <strong class="d-flex justify-center">{{ $t("NoData") }}</strong>
    </v-data-table>

  </v-card>
</template>
  
  <script setup>
import { ref, onMounted } from "vue";
import axios from "axios";
const editedIndex = ref(-1);
const loading = ref(false);
const selectedAttribute = ref("");
const editDialog = ref(false);
const addDialog = ref(false);
const countedAttribut = computed(() => data.value.length);
const dialogDelete = ref(false);
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const data = ref([]);
const search = ref("");
let { t } = useI18n();
const headers = computed(() => [
  { title: t("intutile"), key: "intutile" },
  { title: "Description", key: "description" },
  { title: "Type ", key: "type" },

]);
const props = defineProps({
  attributeId: {
    type: Number,
    required: true,
  },
});
const emits = defineEmits(["reloadData"]);

const id = props.attributeId;
onMounted(async () => {
  //console.log("id from attributeList Mounted", id);
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
    // console.log("attribute List from getVAlue ", data.value);
  } catch (error) {}
};
const reloadData = async () => {
  return await getAttributes();
};
const openEditDialog = (item) => {
  selectedAttribute.value = item;
  // console.log("selected attribute  ", selectedAttribute.value);
  editDialog.value = true;
};
const openAddDialog = (item) => {
  //console.log("Selected from the add ", item);
  //selectedID.value = item;
  //console.log("Selected from the add 22", selectedID.value);

  addDialog.value = true;
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
    console.log("deleting ", attributeId);
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
  