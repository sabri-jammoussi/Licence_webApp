<template>
  <div>
    <div v-if="pending" class="text-center">
      <v-progress-circular
        :size="30"
        color="primary"
        indeterminate
      ></v-progress-circular>
      Loading ...
    </div>

    <div v-else>
      <Nuxt-link to="/users/ajouterpersonnel">
        <v-btn class="ajoutbtn">

          <v-icon color="green" style="margin: auto">mdi-plus</v-icon>

          <v-tooltip activator="parent">Ajouter Utilisateur </v-tooltip>
        </v-btn>
      </Nuxt-link>
      <legend class="legend">List Utilisateur</legend>
      <v-table class="bordered">
        <thead>
          <tr>
            <th class="text-left">FirstName</th>
            <th class="text-left">LastName</th>
            <th class="text-left">Email</th>
            <!-- <th class="text-left">Password</th> -->
            <th class="text-left">Role</th>
            <th class="text-left">Action</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(user, index) in paginatedUsers" :key="index">
            <td>{{ user.firstName }}</td>
            <td>{{ user.lastName }}</td>
            <td>{{ user.email }}</td>
            <!-- <td>{{ user.password }}</td> -->
            <td>{{ user.role }}</td>
            <td>
              <NuxtLink :to="`/users/${user.id}`">
                <v-btn  v-bind="props" v-on="on" style="margin-right: 20px">
                <v-tooltip activator="parent" location="end">Modifier</v-tooltip>
              <v-icon color="blue" style="margin: auto">mdi-pencil</v-icon>
             </v-btn> 
              </NuxtLink>
              <v-btn  
              v-bind="props"
              v-on="on"
              @click.stop="deleteUtilisateur(user.id)"
              >
              <v-tooltip activator="parent" location="end">Supprimer</v-tooltip>

              <v-icon color="red" style="margin: auto">mdi-delete</v-icon>
            </v-btn>
            </td>
          </tr>
        </tbody>
      </v-table>
      <v-pagination
        v-model="currentPage"
        :length="totalPages"
        @input="changePage"
      ></v-pagination>
      <v-dialog v-model="deleteDialog" max-width="400">
          <v-card>
            <v-card-title>Delete Confirmation</v-card-title>
            <v-card-text>Voulez-vous vraiment supprimer cet utilisateur ?</v-card-text>
            <v-card-actions>
              <v-btn color="red" text @click="confirmDelete">Supprimer</v-btn>
              <v-btn color="primary" text @click="deleteDialog = false">Annuler</v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog> 
    </div>
  </div>
</template>
    <script>
import axios from "axios";

export default {
  name: "users",
  data() {
    return {
      users: [],
      itemsPerPage: 7,
      currentPage: 1,
      totalPages: 1,
      utilisateurToDelete: null, 
      deleteDialog: false,
      pending: true,
    };
  },

  methods: {
    async getUsers() {
      try {
        const response = await axios.get("http://localhost:5252/api/Users");
        this.users = response.data;
        this.totalPages = Math.ceil(this.users.length / this.itemsPerPage);
        this.pending = false; // Set pending to false after data is loaded
        console.log("data", response);
      } catch (error) {
        this.pending = false; // Set pending to false in case of an error
        console.error("Error fetching data:", error);
        alert("error connection avec la base de données  ")
      }
    },
    changePage(page) {
      this.currentPage = page;
    },
    deleteUtilisateur(utilisateurId) {
      this.utilisateurToDelete = utilisateurId;
      this.deleteDialog = true;
    },
    async confirmDelete() {
      const utilisateurId = this.utilisateurToDelete;
      try {
        const res = await axios.delete(`http://localhost:5252/api/Users/${utilisateurId}`);
        console.log('deleting user', res);
        // Handle successful deletion if needed
        // Refresh the list of utilisateurs
        await this.getUsers();
      } catch (err) {
        console.error(err);
      } finally {
        this.deleteDialog = false;
      }
    },
  },
  computed: {
    paginatedUsers() {
      const startIndex = (this.currentPage - 1) * this.itemsPerPage;
      const endIndex = startIndex + this.itemsPerPage;
      return this.users.slice(startIndex, endIndex);
    },
  },
  mounted() {
    this.getUsers();
  },
  

    
};
</script>
    
    <style scoped>
.bordered {
  border-collapse: collapse;
  border: 1px solid #e0e0e0;
  padding: 10px;
}

.bordered th,
.bordered td {
  border: 1px solid #e0e0e0;
  padding: 8px;
}

.legend {
  font-size: large;
}

.text-left {
  width: 200px;
}
.ajoutbtn {
  display: flex;
  margin-right: 12px;
  margin-left: auto;
  width: 194px;
  margin-top: 10px;
  margin-bottom: -20px;
}
.text-center {
  text-align: center;
}
</style>
    