export default defineNuxtRouteMiddleware((to, from) => {
    if (process.client) {
        const token = window.localStorage.getItem('token');

        if (!token) {
            return navigateTo('register/login')
        }
    }

});