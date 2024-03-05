
export default defineNuxtRouteMiddleware((to, from) => {
    if (process.client) {
        const token = window.localStorage.getItem('token');

        if (!token && from.fullPath !=='/register/login') {
            return navigateTo('/register/login')
        }
        // return navigateTo('register/login')
    }

});