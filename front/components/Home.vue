<template>
<div>
  <div v-if="pending" class="text-center">
    <Loading/>
</div>

<div v-else>

    <legend class="legend">List Utilisateur</legend>
    <v-table class="bordered">
      <thead>
        <tr>
          <th class="text-left">FirstName</th>
          <th class="text-left">LastName</th>
          <th class="text-left">Email</th>
          <th class="text-left">Password</th>
          <th class="text-left">Role</th>
          

        </tr>
      </thead>
      <tbody >
        <tr v-for="(user, index) in users" :key="index">
          <td>{{ user.firstName }}</td>
          <td>{{ user.lastName }}</td>
          <td>{{ user.email }}</td>
          <td>{{ user.password }}</td>
          <td>{{ user.role }}</td>
     
        </tr>
      </tbody>
     
    </v-table>
    <!-- <v-pagination v-model="currentPage" :length="totalPages" @input="changePage"></v-pagination>
    <v-dialog v-model="deleteDialog" max-width="400">
      <v-card>
        <v-card-title>Delete Confirmation</v-card-title>
        <v-card-text>Voulez-vous vraiment supprimer cet utilisateur ?</v-card-text>
        <v-card-actions>
          <v-btn color="red" text @click="confirmDelete">Supprimer</v-btn>
          <v-btn color="primary" text @click="deleteDialog = false">Annuler</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog> -->
  </div>
</div>

</template>
<script>
import axios from 'axios';

export default {
  name: "users",
  data() {
    return {
      users: [],
      pending: true, // Add a pending state
    };
  },
  mounted() {
    this.getUsers();
  },
  methods: {
    async getUsers() {
      try {
        const response = await axios.get('http://localhost:5252/api/Users');
        this.users = response.data;
        this.pending = false; // Set pending to false after data is loaded
        console.log('data', response);
      } catch (error) {
        this.pending = false; // Set pending to false in case of an error
        console.error('Error fetching data:', error);
      }
    },
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

.text-center {
  text-align: center;
}
</style>
