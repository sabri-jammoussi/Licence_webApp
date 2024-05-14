<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" md="8" lg="6">
        <v-card>
          <div class="grey--text text-h6 text-lg-h6 mt-2">
            <v-icon left color="green" size="35" class="ml-2">mdi-key </v-icon>
            {{ $t("consultingLicenses") }}
          </div>
          <v-divider></v-divider>
          <v-sheet class="mx-auto">
            <v-card-text>
              <v-text-field
                v-model="AppName"
                label="Application"
                readonly
                base-color="green"
                variant="outlined"
              ></v-text-field>
              <v-select
                variant="outlined"
                v-model="selectedClient"
                label="Clients"
                base-color="green"
                readonly
                item-title="raisonSocial"
                :items="ClientsData"
                item-value="id"
              ></v-select>
              <v-select
                variant="outlined"
                v-model="selectedPartenaire"
                :label="t('partner')"
                base-color="green"
                item-title="raisonSocial"
                readonly
                item-value="id"
              ></v-select>
              <v-text-field
                variant="outlined"
                base-color="green"
                label="Selected date"
                readonly
                v-model="selectedDate"
              ></v-text-field>

              <div v-for="(item, index) in type" :key="index">
                <div
                  v-if="item.type === 'Numerique' && item.Obligatoire === true"
                >
                  <v-text-field
                    v-model="textValues[index]"
                    :label="description[index]"
                    readonly
                    type="number"
                    base-color="green"
                    variant="outlined"
                  ></v-text-field>
                </div>
                <div v-if="item.type === 'Texte' && item.Obligatoire === true">
                  <v-text-field
                    v-model="textValues[index]"
                    :label="description[index]"
                    readonly
                    base-color="green"
                    variant="outlined"
                  ></v-text-field>
                </div>

                <div v-if="item.type === 'Date' && item.Obligatoire === true">
                  <v-text-field
                    v-model="textValues[index]"
                    :label="description[index]"
                    readonly
                    base-color="green"
                    variant="outlined"
                  ></v-text-field>
                </div>
                <div
                  v-if="item.type === 'Boolean' && item.Obligatoire === true"
                >
                  <v-text-field
                    v-model="textValues[index]"
                    :label="description[index]"
                    readonly
                    base-color="green"
                    variant="outlined"
                  ></v-text-field>
                </div>
                <div
                  v-if="
                    item.type === 'Enumeration' && item.Obligatoire === true
                  "
                >
                  <v-text-field
                    v-model="textValues[index]"
                    :label="description[index]"
                    readonly
                    base-color="green"
                    variant="outlined"
                  ></v-text-field>
                </div>
              </div>
            </v-card-text>
            <v-divider class="my-1"></v-divider>
            <v-card-actions>
              <v-spacer></v-spacer>

              <Nuxt-link to="/Manager/Licences/LicenceList">
                <v-btn type="submit" color="grey">{{ $t("cancel") }}</v-btn>
              </Nuxt-link>
            </v-card-actions>
          </v-sheet>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
  <script setup>
import { ref, computed, onMounted } from "vue";
import { useRoute } from "vue-router";
import axios from "axios";
import { useMyStore } from "@/store/index.js";
const selectedDate = ref("");
const formattedDate = computed(() => {
  const date = selectedDate.value;
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
});
const selectedPartenaire = ref("");
const PartenairesData = ref([]);
const route = useRoute();
const AppName = ref("");
const ClientsData = ref([]);
const selectedClient = ref("");
const store = useMyStore();
const type = ref("");
const textValues = ref([]);
const ValeurAttributes = ref([]);
const description = ref([]);
const AppId = ref("");
const partenaireId = ref("");
const licenceId = ref("");
let { t } = useI18n();

onMounted(async () => {
  const id = route.params.id;
  await getLicencesById(id);
  await store.loadTokenFromLocalStorage();
});

const getLicencesById = async (id) => {
  axios.get(`http://localhost:5252/api/licence/${id}`).then((res) => {
    //console.log("data global ", res.data);
    // console.log(
    //   "hello textValues",
    //   res.data.attributesValues.map((u) => u.valeur)
    // );
    // console.log(
    //   "hello Licence 222222",
    //   res?.data?.attributesValues
    //     .map((x) => x.attributeLicenceDto)
    //     .map((u) => u.type)
    // );
    partenaireId.value = res.data.partenaireId;
    // console.log("idddddda", partenaireId.value);
    getPartenairesById(partenaireId.value);

    type.value = res?.data?.attributesValues
      .map((x) => x.attributeLicenceDto)
      .map((key) => ({
        type: key.type,
        Obligatoire: key.obligations,
        description: key.description,
      }));
    description.value = res?.data?.attributesValues
      .map((x) => x.attributeLicenceDto)
      .map((key) => key.description);
    textValues.value = res?.data?.attributesValues.map((key) => key.valeur);
    // console.log("valeur att ", ValeurAttributes.value);

    licenceId.value = res.data.attributesValues.map((key) => ({
      id: key.attributeId,
    }));
    AppName.value = res.data.applicationNom;
    selectedClient.value = res.data.clientRaison;
    const dateExp = new Date(res.data.dateExp);
    const formattedDateExp = `${dateExp.getFullYear()}-${String(
      dateExp.getMonth() + 1
    ).padStart(2, "0")}-${String(dateExp.getDate()).padStart(2, "0")}`;

    // Assign the formatted date to selectedDate
    selectedDate.value = formattedDateExp;
    AppId.value = res.data.applicationId;
  });
};
const getPartenairesById = async (partenaireId) => {
  try {
    if (partenaireId == null) return;
    const response = await axios.get(
      `http://localhost:5252/api/partenaire/${partenaireId}`
    );
    selectedPartenaire.value = response.data.raisonSocial;
    //console.log("testt", PartenairesData.value);
  } catch (error) {
    console.error(error);
  }
};
</script>
  