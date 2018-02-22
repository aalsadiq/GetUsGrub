import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    MenuItems: [
      {
        menuItemName: 'Big Mac',
        menuItemPrice: 4.00
      },
      {
        menuItemName: 'Large Fries',
        menuItemPrice: 2.50
      }
    ],
    BillItems: [
      {
        menuItemName: 'Big Mac',
        menuItemPrice: 4.00
      },
      {
        menuItemName: 'Large Fries',
        menuItemPrice: 2.50
      }
    ]
  },
  getters: {
    value: (state) => state.MenuItems
  },
  mutations: {
    AddToDictionary: (state, payload) => {
      state.MenuItems.push({
        menuItemName: payload[0],
        menuItemPrice: payload[1]
      });
    },

    INPUT (state, { MenuItems }) {
      state.MenuItems = [...MenuItems];
    }
  },
  // Actions are necessary when performing asynchronous methods.
  actions: {
    AddToDictionary: (context, payload) => {
      setTimeout(function () {
        console.log(payload[0]);
        console.log(payload[1]);
        context.commit('AddToDictionary', payload)
      }, 1000)
    }
  }
})

//export default new Vuex.Store({
//  state: {
//    isAuthenticated: false
//  },
//  getters: {
//    isAuth: function (state) {
//      return state.isAuthenticated
//    }
//  },
//  mutations: {
//    signIn: function (state)_{
//      state.isAuthenticated = true
//    }
//  },
//  actions: {
//    signIn: function (context, payload) {
//      context.commit('signIn')
//    }
//  }
//})
