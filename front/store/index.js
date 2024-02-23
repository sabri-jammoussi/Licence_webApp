import { defineStore } from 'pinia';
import axios from 'axios';
import { jwtDecode } from 'jwt-decode';

export const useMyStore = defineStore('userStore', {
  state: () => ({
    token: '',
    user: null,
  }),

  getters: {
    userLogged: (state) => state.user,
    token: (state) => state.token,
  },
  // mutations: {
  //   setToken(state, token) {
  //     state.token = token;
  //   },

  //   setUser(state, users) {
  //     state.user = users;
  //   },
  // },

  actions: {
    async createUser({ router }, data) {
      try {
        console.log('Logging in with data:', data);
        const res = await axios.post('http://localhost:5252/api/account/register', data);
        console.log('Response:', res);
        if (res.status >= 200 && res.status < 300) {
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
      }
    },

    async loginUser({ router }, data) {
      try {
        console.log('Logging in with data:', data);
        const res = await axios.post('http://localhost:5252/api/account/login', data);
        console.log('Response:', res);

        if (res.status >= 200 && res.status < 300) {
          // Now, you can safely use localStorage since this code is inside an action.
          localStorage.setItem('token', res.data.token);
          router.push('/');
        } else {
          alert(res.message);
        }
      } catch (error) {
        console.error('Error:', error);
      }
    },

    async logoutUser({ router }) {
      localStorage.removeItem('token');
      router.push("/register/login")
    },
    //task :  function to get the token then read the name and do it in the logout button !!  
    async ReadUser() {
      const token = window.localStorage.getItem('token');

      try {
        if (token) {
          // Decode the JWT to access its payload
          const decodedToken = jwtDecode(token);
          console.log('decodedToken',decodedToken);
          const userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

          console.log('User ID from token:', userId);
          const response = await axios.get(`http://localhost:5252/api/Users/${userId}`, {
            headers: {
              'Authorization': `Bearer ${token}`,
            },
          });
          if (response.status >= 200 && response.status < 300) {
            // Update the user state directly or do something with the data
            const userData= response.data
            console.log('User data:', userData);
            this.user=userData;
            console.log('dataaaaaaaaaaa', this.user);

          } else {
            alert('No data or invalid response status.');
          }
        } else {
          alert('No token found.');
        }
      } catch (error) {
        console.error('Error fetching user data:', error);
        // Handle errors appropriately
      }
    },

    async CreateUser({ router }, data) {
      try {
        const token = window.localStorage.getItem('token');

        if (token) {
          // If there is a token, set the authorization header
          axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
          console.log('Token checked:', axios.defaults.headers.common);
        }

        const response = await axios.post('http://localhost:5252/api/Users', data);
        console.log('dataRecivied', data);
        if (response.status >= 200 && response.status < 300) {
          router.push('/users/');
        } else {
          alert(response.message);
        }
      } catch (error) {
        console.error('Error:', error);
      }
    }

  },



});
