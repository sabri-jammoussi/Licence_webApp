// plugins/axios-auth.js
import axios from "axios";
import ErrorNotFound from "~/components/ErrorNotFound.vue";
import defu from 'defu';

export default function () {
   
    if (process.client) {
        const token = window.localStorage.getItem('token');

        if (token) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;
        } else {
            console.log('unauthorized');
            // You might want to redirect to the login page or handle unauthorized access here
            //alert("unauthorized");
            
             navigateTo('/') // Directly access router instance from SSR context
        }
    }
}
