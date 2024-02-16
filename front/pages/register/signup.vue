<template>
  <v-container class="">
    <v-row justify="center">
      <v-col cols="12" sm="8" md="6">
        <v-card class="elevation-12">
          <v-card-title class="headline grey lighten-2">
            <v-icon color="primary">mdi-account-plus</v-icon>
            Register!
          </v-card-title>

          <!-- <Notification :message="error" v-if="error"/> -->

          <v-form @submit.prevent="signup">
            <v-text-field
              v-model="firstName"
              label="firstName"
              required
            ></v-text-field>

            <v-text-field
              v-model="lastName"
              label="lastName"
              required
            ></v-text-field>
            

            <v-text-field
              v-model="email"
              label="Email"
              type="email"
              required
            ></v-text-field>

            <v-text-field
              v-model="password"
              label="Password"
              type="password"
              required
            ></v-text-field>

            <v-text-field
              v-model="role"
              label="role"
              required
            ></v-text-field>

            <v-btn type="submit" color="black" block> Register </v-btn>
          </v-form>

          <div class="text-center mt-4">
            Already got an account?
            <nuxt-link to="/register/login" class="primary--text"
              >Login</nuxt-link
            >
          </div>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>
  
  <script >
import { mapActions } from 'vuex'; 
export default {
  middleware: "guest",
  data() {
    return {
      firstName: "",
      lastName:"",
      email: "",
      password: "",
      role :"",
    };
  },
  methods: {
    ...mapActions(["createUser"]),
    async  signup() {
      if (!this.firstName || !this.email || !this.password) {
        alert("Please Fill the Field");
      } else {
        const data = {
          firstName: this.firstName,
          lastName:this.lastName,
          email: this.email,
          password: this.password,
          role : this.role
        };
 

        await this.$store.dispatch('createUser', data);

     
      }
    },
  },
};
</script>
  