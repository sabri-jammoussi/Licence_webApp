<template>
  <v-list>
    <v-banner>
      <EditEnumVal
        :user="selectedUser"
        @dataChanged="reloadData"
        v-if="editDialog"
        @close-dialog="editDialog = false"
      />
      <template v-slot:text> No Internet connection </template>

      <template v-slot:actions>
        <AddEnumVal :enumId="enumerationId" @dataChanged="reloadData" />
      </template>
    </v-banner>

    <v-list-item
      v-for="(item, i) in data"
      :key="i"
      :value="item"
      color="primary"
      rounded="shaped"
    >
      <v-list-item-title>{{ item.valeur }}</v-list-item-title>
      <template v-slot:append>
        <v-icon
          size="small"
          class="me-3 mt-3"
          @click="openEditDialog(item)"
          color="green"
          variant="tonal"
        >
          mdi-pencil
        </v-icon>

        <v-icon
          size="small"
          class="mt-3"
          @click.stop="deleteItem(item.id)"
          color="red"
        >
          mdi-delete
        </v-icon>
      </template>
    </v-list-item>
    <v-dialog v-model="dialogDelete" max-width="420">
      <v-card>
        <v-card-title>{{ $t("deleteconfirme") }}</v-card-title>
        <v-card-text>{{ $t("deletemsg") }}</v-card-text>
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
</template>
  
  <script setup>
import { ref, onMounted, defineProps } from "vue";
import axios from "axios";
import AddEnumVal from "./AddEnumVal.vue";
import EditEnumVal from "./EditEnumVal.vue";
const dialogDelete = ref(false);
const loading = ref(false);
const editDialog = ref(false);
const selectedUser = ref("");

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
    console.log(data);
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
    const token = window.localStorage.getItem("token");
    if (token) {
      axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
    } else {
      console.log("unauthorized");
      alert("unauthorized");
    }
    await axios.delete(
      `http://localhost:5252/api/enumerationvaleur?id=${utilisateurId}`
    );
    loading.value = true;
    try {
      await getEnumVal();
    } catch (error) {
      console.error(error);
    } finally {
      loading.value = false;
    }
  } catch (err) {
    console.error(err);
  } finally {
    closeDelete();
  }
};
const reloadData = async () => {
  return await getEnumVal();
};
const closeDelete = () => {
  dialogDelete.value = false;
};
const openEditDialog = (item) => {
  selectedUser.value = item;
  console.log("selected user 2 ", selectedUser.value);
  editDialog.value = true;
};
</script>