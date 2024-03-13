<script setup>
import i18n from "~/plugins/i18n";
const items = ["en", "fr"];
const { locale } = useI18n();
</script>

<template>
  <div>
    <form>
      <v-select v-model="locale" :items="items"> </v-select>
      <p>{{ $t("hello") }}</p>
    </form>
  </div>
</template>
