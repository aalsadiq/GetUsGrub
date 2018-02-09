import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    isAuthenticated: false
  },
  getters: {
    isAuth: function (state) {
      return state.isAuthenticated
    }
  },
  mutations: {
    signIn: function (state)_{
      state.isAuthenticated = true
    }
  },
  actions: {
    signIn: function (context, payload) {
      context.commit('signIn')
    }
  }
})
