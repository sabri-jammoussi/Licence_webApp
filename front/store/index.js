export const state = () => ({
    token: localStorage.getItem('token') || '',
    users: {},

})

export const getters = {
    users: (state) => {
        return state.users
    },
    token: (state) => {
        return state.token
    },
    
}

export const mutations = {
    setUser(state, users) {
        state.users = users;
    },
    setToken(state, token) {
        state.token = token
    },
 
}
export const actions = {
    async createUser(_, data) {
        const res = await this.$axios.post('http://localhost:5252/api/Users', data);
        console.log('aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa',res)
         if (res.data.status == 1) {
             alert(res.data.message);
             data.firstName = "";
             data.email = "";
             data.password = "";
             this.$router.push('/')
         } else {
             alert(res.data.message)
         }
    },
    async loginUser({ commit }, data) {
        const res = await this.$axios.post('http://localhost:5252/api/Users', data);
        if (res.data.status == 1) {
            data.email = "";
            data.password = "";
            localStorage.setItem('token', res.data.jwt);
            commit('setToken', res.data.jwt);
            alert(res.data.message)
            this.$router.push("/");

        } else {
            alert(res.data.message)
        }
    },
}