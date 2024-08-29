<template>
  <v-container>
    <v-row justify="center">
      <v-col cols="12" md="8" lg="6">
        <v-card>
          <div class="grey--text text-h6 text-lg-h6 mt-2">
            <v-icon left color="green" size="35" class="ml-2">mdi-key </v-icon>
            {{ $t("newLicenses") }}
          </div>
          <v-divider></v-divider>
          <v-sheet class="mx-auto">
            <form @submit.prevent="AddLicence">
              <v-card-text>
                <v-text-field
                  v-model="AppName"
                  label="Application"
                  :disabled="true"
                  base-color="green"
                  variant="outlined"
                  item-title="aaafdl,"
                  item-value="selectedApps"
                ></v-text-field>
                <v-select
                  variant="outlined"
                  v-model="selectedClient"
                  label="Clients"
                  base-color="green"
                  item-title="raisonSocial"
                  :items="ClientsData"
                  item-value="id"
                  @blur="v$.selectedClient.touch"
                  @input="v$.selectedClient.$touch"
                  :error-messages="
                    v$.selectedClient.$errors.map((e) => e.$message)
                  "
                ></v-select>
                <v-select
                  variant="outlined"
                  v-model="selectedPartenaire"
                  :label="t('partner')"
                  base-color="green"
                  item-title="raisonSocial"
                  :items="PartenairesData"
                  item-value="id"
              
                ></v-select>
                <v-menu v-model="isMenuOpen" :close-on-content-click="false">
                  <template v-slot:activator="{ props }">
                    <v-text-field
                      variant="outlined"
                      base-color="green"
                      label="Selected date"
                      :model-value="formattedDate"
                      readonly
                      v-bind="props"
                    ></v-text-field>
                  </template>
                  <v-date-picker
                    v-model="selectedDate"
                    :min="minDate"
                    @blur="v$.selectedDate.touch"
                    @input="v$.selectedDate.$touch"
                    :error-messages="
                      v$.selectedDate.$errors.map((e) => e.$message)
                    "
                  ></v-date-picker>
                </v-menu>
                <div v-for="(item, index) in type" :key="index">
                  <div
                    v-if="item.type === 'Texte' && item.Obligatoire === true"
                  >
                    <AttributeLicenceText
                      @textData="
                        (dataa) => {
                          textValues[index] = dataa;
                          // console.log(
                          //   'Data received from child component:',
                          //   textValues
                          // );
                        }
                      "
                      :textDescription="description[index]"
                    />
                  </div>
                  <div
                    v-if="
                      item.type === 'Numerique' && item.Obligatoire === true
                    "
                  >
                    <AttributeLicenceNumber
                      @NumberData="
                        (numberData) => {
                          numberValues[index] = numberData;
                          // console.log(
                          //   'number received from the child ',
                          //   numberData
                          // );
                        }
                      "
                      :textDescription="description[index]"
                    />
                  </div>
                  <div
                    v-if="item.type === 'Boolean' && item.Obligatoire === true"
                  >
                    <AttributeLicenceBool
                      @boolData="
                        (BoolData) => {
                          boolValues[index] = BoolData;
                          // console.log(
                          //   'Bool received from the child ',
                          //   BoolData
                          // );
                        }
                      "
                      :textDescription="description[index]"
                    />
                  </div>
                  <div v-if="item.type === 'Date' && item.Obligatoire === true">
                    <AttributeLicenceDate
                      @DateData="
                        (dateData) => {
                          DateValeur[index] = dateData;
                          // console.log(
                          //   'Dateeeeeee received from the child ',
                          //   dateData
                          // );
                        }
                      "
                      :textDescription="description[index]"
                    />
                  </div>
                  <div
                    v-if="
                      item.type === 'Enumeration' && item.Obligatoire === true
                    "
                  >
                    <AttributeLicenceEnum
                      :enumerationId="item.EnumerationId"
                      @EnumData="
                        (enumData) => {
                          Enumeration[index] = enumData;
                          // console.log(
                          //   'enumData received from the child ',
                          //   enumData
                          // );
                        }
                      "
                      :textDescription="description[index]"
                    />
                  </div>
                </div>
              </v-card-text>
              <v-divider class="my-1"></v-divider>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn type="submit" color="green" :loading="loading">{{
                  $t("add")
                }}</v-btn>
                <Nuxt-link to="/Manager/Licences/LicenceList">
                  <v-btn type="submit" color="grey">{{ $t("cancel") }}</v-btn>
                </Nuxt-link>
              </v-card-actions>
            </form>
          </v-sheet>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
  <SnackBar
    :key="keyToast"
    v-if="showSnackbar"
    :message="snackbarMessage"
    :showSnackBar="showSnackbar"
  />
</template>
<script setup>
import { ref, computed, watch, onMounted } from "vue";
import { useRoute, useRouter } from "vue-router";
import axios from "axios";
import { required, helpers } from "@vuelidate/validators";
import { useVuelidate } from "@vuelidate/core";
import SnackBar from "~/components/SnackBar.vue";
import { useMyStore } from "@/store/index.js";
import AttributeLicenceText from "./LicenceValeur/AttributeLicenceText.vue";
import AttributeLicenceNumber from "./LicenceValeur/AttributeLicenceNumber.vue";
import AttributeLicenceBool from "./LicenceValeur/AttributeLicenceBool.vue";
import AttributeLicenceEnum from "./LicenceValeur/AttributeLicenceEnum.vue";
import AttributeLicenceDate from "./LicenceValeur/AttributeLicenceDate.vue";
const { withMessage } = helpers;
const Enumeration = ref([]);
const DateValeur = ref([]);
const isMenuOpen = ref(false);
const selectedDate = ref(new Date());
const route = useRoute();
const router = useRouter();
const selectedApps = route.params.selectedApp;
const AppName = ref("");
const ClientsData = ref([]);
const PartenairesData = ref([]);
const selectedClient = ref("");
const selectedPartenaire = ref("");
const Userid = computed(() => store.user?.idd);
const store = useMyStore();
const showSnackbar = ref(false);
const snackbarMessage = ref("");
const keyToast = ref(0);
const loading = ref(false);
const type = ref("");
const IdOfAttribute = ref("");
const Obligatoire = ref("");
const EnumerationId = ref("");
const isTexte = ref("");
const isBool = ref("");
const isNumber = ref("");
const textValues = ref([]);
const numberValues = ref([]);
const boolValues = ref([]);
const description = ref([]);
const { emit } = getCurrentInstance();
let { t } = useI18n();

let data = null;
const rules = {
  selectedClient: {
    required: withMessage("Client obligatoire", required),
  },
  selectedPartenaire: {
    required: withMessage("Partenaire obligatoire", required),
  },
  selectedDate: {
    required: withMessage("Date obligatoire", required),
  },
};
const v$ = useVuelidate(
  rules,
  {
    selectedPartenaire,
    selectedClient,
    selectedDate,
  },
  { $stopPropagation: true }
);
const minDate = new Date();
const formattedDate = computed(() => {
  const date = selectedDate.value;
  const year = date.getFullYear() + 1;
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
});
watch(selectedDate, (val) => {
  val || close();
});
onMounted(async () => {
  await getApplications();
  await getClients();
  await getPartenaires();
  await store.loadTokenFromLocalStorage();
});
const AddLicence = async () => {
  v$.value.$reset();
  // Validate the form
  v$.value.$touch();
  if (v$.value.$invalid) {
  } else {
    try {
      // if (v$.value.$pending) return;
      // if (v$.value.$invalid) {
      //   v$.value.$touch();
      //   return;
      // }
      const attributesValues = [];

      numberValues.value.forEach((value, index) => {
        attributesValues.push({
          valeur: value,
          attributeId: IdOfAttribute.value[index].IdOfAttribute,
        });
      });
      textValues.value.forEach((value, index) => {
        attributesValues.push({
          valeur: value,
          attributeId: IdOfAttribute.value[index].IdOfAttribute,
        });
      });
      Enumeration.value.forEach((value, index) => {
        attributesValues.push({
          valeur: value,
          attributeId: IdOfAttribute.value[index].IdOfAttribute,
        });
      });
      DateValeur.value.forEach((value, index) => {
        attributesValues.push({
          valeur: value,
          attributeId: IdOfAttribute.value[index].IdOfAttribute,
        });
      });
      boolValues.value.forEach((value, index) => {
        attributesValues.push({
          valeur: value.toString(),
          attributeId: IdOfAttribute.value[index].IdOfAttribute,
        });
      });

      data = {
        dateExp: formattedDate.value,
        userId: parseInt(Userid.value),
        applicationId: parseInt(selectedApps),
        clientId: selectedClient.value,
        attributesValues: attributesValues,
        partenaireId: selectedPartenaire.value,
      };

      const response = await axios.post(
        "http://localhost:5252/api/licence",
        data
      );
      if (response.status >= 200 && response.status < 300) {
        showSnackbar.value = true;
        keyToast.value++;
        snackbarMessage.value = t("snackBarMsg");
      } else {
        throw new Error("Request failed with status code " + response.status);
      }
    } catch (err) {
      console.log("error ", err);
    } finally {
      await new Promise((resolve) => setTimeout(resolve, 2510));
      router.push("/Manager/Licences/LicenceList");
    }
  }
};
const getApplications = async () => {
  try {
    const response = await axios.get(
      `http://localhost:5252/api/appliction/${selectedApps}`
    );
    //console.log("datta of application ", response.data);
    type.value = response.data.attributes.map((key) => ({
      type: key.type,
      Obligatoire: key.obligations,
      EnumerationId: key.enumerationId,
      description: key.description,
    }));
    IdOfAttribute.value = response.data.attributes.map((key) => ({
      IdOfAttribute: key.id,
    }));
    EnumerationId.value = response.data.attributes.map(
      (key) => key.enumerationId
    );
    description.value = response.data.attributes.map((key) => key.description);
    //console.log("description", description.value);
    isTexte.value = type.value.some((obj) => obj.type === "Texte");
    isBool.value = type.value.some((obj) => obj.type === "Boolean");
    isNumber.value = type.value.some((obj) => obj.type === "Numerique");
    AppName.value = response.data.nom;
  } catch (error) {
    console.error(error);
  }
};
const getClients = async () => {
  try {
    const response = await axios.get(`http://localhost:5252/api/client`);
    ClientsData.value = response.data.map((Application) => ({
      id: Application.id,
      raisonSocial: Application.raisonSocial,
    }));
  } catch (error) {
    console.error(error);
  }
};
const getPartenaires = async () => {
  try {
    const response = await axios.get(`http://localhost:5252/api/partenaire`);
    PartenairesData.value = response.data.map((partenaire) => ({
      id: partenaire.id,
      raisonSocial: partenaire.raisonSocial,
    }));
    // console.log('testt',PartenairesData.value);
  } catch (error) {
    console.error(error);
  }
};
</script>
