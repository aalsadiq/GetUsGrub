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
    ],
    Tokens: [ //Added token info [-Angelica]
      {
        username: '',
        iat:'',
        exp:'',
        claims:''
      }
    ],
    Claims: [//Added claims [-Angelica]
      {
        ReadUser: 'ReadUser',
        ReadIndividualProfile: 'ReadIndividualProfile',
        ReadPreferences: 'ReadPreferences',
        ReadBillSplitter: 'ReadBillSplitter',
        ReadMenu: 'ReadMenu',
        ReadDictionary: 'ReadDictionary',
        ReadRestaurantProfile: 'ReadRestaurantProfile'
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
    },
    checkClaims: () => { //check claims here..
      ReadUser: 'ReadUser'
      ReadIndividualProfile: 'ReadIndividualProfile'
      ReadPreferences: 'ReadPreferences'
      ReadBillSplitter: 'ReadBillSplitter'
      ReadMenu: 'ReadMenu'
      ReadDictionary: 'ReadDictionary'
      ReadRestaurantProfile: 'ReadRestaurantProfile'
    },
    StoreToken: () => {//Added [-Angelica]
    },
    RemoveToken: () => {//Added [-Angelica]
      
    },
    StoreImage: () => {//Added [-Angelica]
      imagePath: ''//The path that the image is found on
      username: ''//the usersimage
      imageName: username + 'profile'
    },
    RemoveImage: () => {//Added [-Angelica]
      imagePath: ''
      username: ''
      imageName: ''
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
