import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    restaurantDisplayName: '',
    restaurantLatitude: '',
    restaurantLongitude: '',
    uniqueUserCounter: 0,
    MenuItems: [
    ],
    BillItems: [
    ],
    BillUsers: [
    ],
    isAuthenticated: false,
    authenticationToken: ''
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
        name: payload[0],
        uID: payload[1]
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
      console.log('Dictionary Store Mutation Index: ' + payload)
      state.MenuItems.splice(payload, 1)
    },
    RemoveFromBillTable: (state, payload) => {
      console.log('Bill Store Mutation Index: ' + payload)
      state.BillItems.splice(payload, 1)
    },
    RemoveUser: (state, payload) => {
      console.log('User Store Mutation Index ' + payload)
      state.BillUsers.forEach(function (element, index) {
        if (element.uID === payload) {
          state.BillUsers.splice(index, 1)
        }
      })
      for (var i = 0, len1 = state.BillItems.length; i < len1; i++) {
        for (var j = 0, len2 = state.BillItems[i].selected.length; j < len2; j++) {
          if (state.BillItems[i].selected[j] === payload) {
            state.BillItems[i].selected.splice(j, 1)
          }
        }
      };
    }
  },
  // Actions are necessary when performing asynchronous methods.
  actions: {
    AddToDictionary: (context, payload) => {
      setTimeout(function () {
        console.log('Added Food Item Name: ' + payload[0])
        console.log('Added Food Item Price: ' + payload[1])
        context.commit('AddToDictionary', payload)
      }, 250)
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
      }, 250)
    },
    RemoveFromBillTable: (context, payload) => {
      setTimeout(function () {
        context.commit('RemoveFromBillTable', payload)
      }, 250)
    },
    RemoveUser: (context, payload) => {
      setTimeout(function () {
        context.commit('RemoveUser', payload)
      }, 250)
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
