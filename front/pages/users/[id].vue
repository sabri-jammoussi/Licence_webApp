<template>
    <v-container>
      <v-row justify="space-around">
        <v-card width="1000" title="Ajouter Utilisateur">
          <v-sheet width="900" height="560" class="mx-auto">
            <div v-if="isLoading" class="text-center">
              <loading :title="isLoadingTitle" />
            </div>
            <div v-else>
                <form @submit.prevent="UpdateUser">
                <v-row >
                  <v-col>
                    <v-alert type="error" v-for="(eror, index) in errorList.Password" :key="index">
                      {{ eror }}
                      
                    </v-alert>
                  </v-col>
                </v-row>
                <v-text-field v-model="users.firstName" label="Nom"></v-text-field>
                <v-text-field v-model="users.lastName" label="Prenom"></v-text-field>
                <v-text-field v-model="users.email" label="Email"></v-text-field>
                <v-text-field v-model="users.role" label="role"></v-text-field>
  
                <v-row justify="flex-end">
                  <v-btn type="submit" class="button-85 green-btn">Modifier</v-btn>
                  <Nuxt-link to="/users/">
                    <v-btn type="submit" class="button-85 red-btn">Annuler</v-btn>
                  </Nuxt-link>
                </v-row>
              </form>
            </div>
          </v-sheet>
        </v-card>
      </v-row>
    </v-container>
  </template>

 
<script>
import Loading from "~/components/Loading.vue";
import axios from "axios";

export default {
  components: { Loading },
  name: "Edituser",
  data() {
    return {
        userId : '',
      users: {
        firstName: "",
        lastName: "",
        email: "",
        password: "",
        role: "",
      },
      isLoading: false,
      isLoadingTitle: "Loading",
      errorList: [],
    };
  },
  mounted(){
    this.userId=this.$route.params.id
    this.getUser(this.userId);
  },
  methods: {
    getUser(userId){
        axios.get(`http://localhost:5252/api/Users/${userId}`).then((res) => {
            this.users=res.data;
            console.log('ressssssssss',res);
        });
    },
    UpdateUser() {
      this.isLoading = true;
      this.isLoadingTitle = "Saving";

      axios
        .put(`http://localhost:5252/api/Users/${this.userId}`, this.users)
        .then((result) => {
          console.log("resultat", result);
          this.users.firstName = "";
          this.users.lastName = "";
          this.users.email = "";
          this.users.password = "";
          this.users.role = "";
          this.isLoading = false;
          this.isLoadingTitle = "Loading";
          this.$router.push('/users/');

        })
        .catch((error) => {
          console.error("Error fetching data:", error);
          if (error.response) {
            if (error.response.status == 400) {
              this.errorList = error.response.data.errors;
              console.log('aaaaaaaaaaa',this.errorList);
              this.isLoading = false;
            }
          }
          this.isLoading=false;
        });
    },
  },
//     setup() {
//     onMounted(() => {
//        setTimeout(() => {
//         // Call the addUtilisateur function after 1000 milliseconds
//         addUtilisateur();
//       }, 5000);
//      })
//  }
};
</script>

<style>

</style>