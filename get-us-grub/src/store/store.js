import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    MenuItems: [
    ],
    BillItems: [
    ],
    BillUsers: [
    ]
  },
  getters: {
    totalPrice: state => {
      var temp = 0
      state.BillItems.forEach(function (element) {
        temp = temp + element.price
      })
      return temp
    }
  },
  mutations: {
    AddToDictionary: (state, payload) => {
      state.MenuItems.push({
        name: payload[0],
        price: payload[1],
        selected: []
      })
    },
    AddBillUser: (state, payload) => {
      state.BillUsers.push({
        name: payload
      })
    },
    EditDictionaryItem: (state, payload) => {
      state.MenuItems[payload[0]].name = payload[1]
      state.MenuItems[payload[0]].price = payload[2]
    },
    EditBillItem: (state, payload) => {
      state.BillItems[payload[0]].name = payload[1]
      state.BillItems[payload[0]].price = payload[2]
    },
    RemoveFromDictionary: (state, payload) => {
      console.log('Dictionary Store Mutation: ' + payload)
      state.MenuItems.splice(payload, 1)
    },
    RemoveFromBillTable: (state, payload) => {
      console.log('Bill Store Mutation: ' + payload)
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
    AddBillUser: (context, payload) => {
      setTimeout(function () {
        console.log('Added New Bill User: ' + payload)
        context.commit('AddBillUser', payload)
      }, 250)
    },
    EditDictionaryItem: (context, payload) => {
      setTimeout(function () {
        context.commit('EditDictionaryItem', payload)
      }, 250)
    },
    EditBillItem: (context, payload) => {
      setTimeout(function () {
        context.commit('EditBillItem', payload)
      }, 250)
    },
    RemoveFromDictionary: (context, payload) => {
      setTimeout(function () {
        context.commit('RemoveFromDictionary', payload)
      }, 500)
    },
    RemoveFromBillTable: (context, payload) => {
      setTimeout(function () {
        context.commit('RemoveFromBillTable', payload)
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
