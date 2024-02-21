import { defineStore } from 'pinia';
import axios from 'axios';

export const useMyStore = defineStore('userStore', {
  state: () => ({
    token: '',
    user: null,
  }),

  getters: {
    userLogged: (state) => state.user,
    token: (state) => state.token,
  },
  mutations: {
    setToken(state, token) {
      state.token = token;
    },

    setUser(state, users) {
      state.user = users;
    },
  },

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
    async logoutUser({ router}) {
      localStorage.removeItem('token');
      router.push("/register/login")
    },
  },
 


});
