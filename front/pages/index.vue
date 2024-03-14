<template>
<Home/>
</template>
<script setup>
definePageMeta({
  middleware: ["auth"],
});
</script>