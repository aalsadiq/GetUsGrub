import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    MenuItems: [
    ],
    BillItems: [
      {
        menuItemName: 'Test',
        menuItemPrice: 2.00,
        menuItemEdit: false
      }
    ]
  },
  getters: {
    totalPrice: state => {
      var temp = 0
      state.BillItems.forEach(function (element) {
        temp += element.menuItemPrice
      })
      return temp
    }
  },
  mutations: {
    AddToDictionary: (state, payload) => {
      state.MenuItems.push({
        menuItemName: payload[0],
        menuItemPrice: payload[1],
        menuItemEdit: false
      })
    },
    ToggleEdit: (state, payload) => {
      var temp = state.MenuItems[payload].menuItemEdit
      state.MenuItems.forEach(function (element) {
        element.menuItemEdit = false
      })
      if (temp === false) {
        state.MenuItems[payload].menuItemEdit = true
      }
    },
    RemoveFromDictionary: (state, payload) => {
      console.log('Store Mutation: ' + payload)
      state.MenuItems.splice(payload, 1)
    },
    RemoveFromBill: (state, payload) => {
      console.log('Store Mutation: ' + payload)
      state.BillItems.splice(payload, 1)
    }
  },
  // Actions are necessary when performing asynchronous methods.
  actions: {
    AddToDictionary: (context, payload) => {
      setTimeout(function () {
        console.log('Added Food Item Name: ' + payload[0])
        console.log('Added Food Item Price: ' + payload[1])
        context.commit('AddToDictionary', payload)
      }, 500)
    },
    ToggleEdit: (context, payload) => {
      setTimeout(function () {
        context.commit('ToggleEdit', payload)
      }, 250)
    },
    RemoveFromDictionary: (context, payload) => {
      setTimeout(function () {
        context.commit('RemoveFromDictionary', payload)
      }, 500)
    },
    RemoveFromBill: (context, payload) => {
      setTimeout(function () {
        context.commit('RemoveFromBill', payload)
      }, 500)
    }
  }
})

// export default new Vuex.Store({
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
// })
