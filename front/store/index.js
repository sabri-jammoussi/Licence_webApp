import { defineStore } from 'pinia';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';

export const useMyStore = defineStore('userStore', {
  state: () => ({
    token: '',
    user: '',
    eroorlistee: null,
    isLoggedin :false,
    EmailError: null,
    roleOptions: null,
    users: [],
    apps:[]
  }),
  getters: {
    userLogged: (state) => state.user,
    token: (state) => state.token,
  },
  actions: {
    async Register({ router }, data) {
      try {
        console.log('Logging in with data:', data);
        const res = await axios.post('http://localhost:5252/api/account/register', data);
        console.log('Response:', res);
        if (res.status >= 200 && res.status < 300) {
          this.EmailError = null;
          data.firstName = '';
          data.lastName = '';
          data.email = '';
          data.password = '';
          data.role = '';
          router.push('/register/login');
        } else {
          alert("error ", res.data.message);
        }
      } catch (error) {
        console.error('Error:', error);
        if (error.response) {
          if (error.response.status == 400) {
            this.EmailError = error.response.data;
            console.log('aaaaaaaaaaa', this.EmailError);
          }
        }
      }
    },

    async loginUser({ router }, data) {
      try {
        console.log('Logging in with data:', data);
        const res = await axios.post('http://localhost:5252/api/account/login', data);
        console.log('Response:', res);
        if (res.status >= 200 && res.status < 300) {
          this.eroorlistee = null;
          localStorage.setItem('token', res.data.token);
          this.isLoggedin = true;
          console.log('islogeedin ',this.isLoggedin);
          router.push('/');
        } else {
          alert(res.message);
        }
      } catch (error) {
        console.error('Error:', error);
        if (error.response) {
          if (error.response.status == 400) {
            this.eroorlistee = error.response.data;
            console.log('aaaaaaaaaaa', this.eroorlistee);
          }
        }
      }
    },
    async loadTokenFromLocalStorage() {
      const token = window.localStorage.getItem('token');
      try {
        if (token) {
          // Decode the JWT to access its payload
          const decodedToken = jwtDecode(token);
          //console.log('decodedToken  loadTokenFromLocalStorage',decodedToken);
          const userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
          const userFirstName = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"];
          const userLastName = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname"];
          const userEmail = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"];
          const userRole = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
          this.user = {
            idd: userId,
            firstName: userFirstName,
            lastName: userLastName,
            email: userEmail,
            role: userRole,
          };
          //console.log('user Data from store loadTokenFromLocalStorage', this.user);
        }
      } catch (error) {
        console.error('Error:', error);
      }
    },

    async logoutUser({ router }) {
      localStorage.removeItem('token');
      this.isLoggedin = false;
      console.log('islogeedin ',this.isLoggedin);
      router.push("/register/login");
    },

    async CreateUser(data) {
      try {
        const token = window.localStorage.getItem('token');

        if (token) {
          // If there is a token, set the authorization header
          axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
          console.log('Token checked:', axios.defaults.headers.common);
        } else {
          console.log('unauthorized');
          alert('unauthorized');
        }
        console.log('dataRecivied', data);
        const response = await axios.post('http://localhost:5252/api/Users', data);
        await this.getUsers();
        if (response.status >= 200 && response.status < 300) {
          // router.push('/users/');
          this.EmailError = null;
        } else {
          alert('erorrooor', response.message);
        }
      } catch (error) {
        console.error('Error:', error);
      }
    },
    async UpdateUser(id, data) {
      try {
        const token = window.localStorage.getItem('token');
        if (token) {
          // If there is a token, set the authorization header
          axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
          console.log('Token checked:', axios.defaults.headers.common);
        } else {
          console.log('unauthorized');
          alert('unauthorized');
        }
        const response = await axios.put(`http://localhost:5252/api/Users/${id}`, data);
        if (response.status >= 200 && response.status < 300) {
          // router.push('/users/');
        } else {
          alert('erorrooor', response.message);
        }
      } catch (error) {
        console.error('Error of update :', error);
      }
    },  
    // async getUsersById(id) {
    //   try {
    //     const response = await axios.get(`http://localhost:5252/api/Users/${id}`);
    //     this.user = response.data;
    //     console.log("data from store ", this.user);
    //   } catch (error) {
    //     console.error("Error fetching data:", error);
    //     alert("Error connecting to the database");
    //   }
    // },
    async getUsers() {
      try {
        const response = await axios.get("http://localhost:5252/api/Users");
        this.users = response.data;
        // console.log("data from store ", this.users);
      } catch (error) {
        console.error("Error fetching data:", error);
        alert("Error connecting to the database");
      }
    },
    async getApplications (){
      try {
        const token = window.localStorage.getItem('token');

        if (token) {
          // If there is a token, set the authorization header
          axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
          //console.log('Token checked:', axios.defaults.headers.common);
        } else {
          console.log('unauthorized');
          alert('unauthorized');
        }
        const response = await axios.get("http://localhost:5252/api/appliction");
        this.apps = response.data;
        // console.log("data from store ", this.apps);
      } catch (error) {
        console.error("Error fetching data:", error);
        alert("Error connecting to the database");
      }
    },
    async ReadRoles() {
      try {
        const response = await axios.get('http://localhost:5252/api/users/roles');
        this.roleOptions = response.data.roles;
        // console.log('rolessssss',this.roleOptions);
      } catch (error) {
        console.log('error', error);
      }
    }
  },

});
