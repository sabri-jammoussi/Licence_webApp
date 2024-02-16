<template>
    <div>
        <v-progress-circular :size="30" color="primary" indeterminate></v-progress-circular>
        {{ title }} 
    </div>
</template>

<script>
export default {
    name:'Loading',
    props :{
        title:String
    }

}
</script>

<style>

</style>