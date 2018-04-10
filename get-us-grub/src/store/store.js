import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    restaurantID: 26,
    originAddress: 'Los Angeles, CA',
    destinationAddress: '1250 Bellflower Blvd, Long Beach, CA',
    googleMapsBaseUrl: 'https://www.google.com/maps/embed/v1/directions?key=AIzaSyCfKElVtKARYlgvCdQXBImfjRH5rmUF0mg',
    restaurantDisplayName: 'displayName26',
    restaurantLatitude: '34.047041',
    restaurantLongitude: '-118.256578',
    uniqueUserCounter: 0,
    MenuItems: [
    ],
    BillItems: [
    ],
    BillUsers: [
    ],
    isAuthenticated: true,
    restaurantSelection: {
      request: {
        foodType: '',
        city: '',
        state: '',
        distance: 1,
        avgFoodPrice: 1
      },
      selectedRestaurant: {
        isConfirmed: false,
        restaurantGeoCoordinates: {
          latitude: null,
          longitude: null
        },
        clientUserGeoCoordinates: {
          latitude: null,
          longitude: null
        },
        restaurantId: null,
        displayName: '',
        address: {
          street1: '',
          street2: '',
          city: '',
          state: '',
          zip: null
        },
        phoneNumber: '',
        businessHours: []
      },
      showRestaurantSelectionSection: true
    },
    rules: {
      usernameRules: [
        username => !!username || 'Username is required',
        username => /^[A-Za-z\d]+$/.test(username) || 'Username must contain only letters and numbers'
      ],
      displayNameRules: [
        displayName => !!displayName || 'Display name is required'
      ],
      passwordRules: [
        password => !!password || 'Password is required',
        password => password.length >= 8 || 'Password must be at least 8 characters',
        password => password.length < 64 || 'Password must be at most 64 characters'
      ],
      securityAnswerRules: [
        securityAnswer => !!securityAnswer || 'Security answer is required'
      ],
      addressStreet1Rules: [
        street1 => !!street1 || 'Street 1 is required'
      ],
      addressCityRules: [
        city => !!city || 'City is required'
      ],
      addressStateRules: [
        state => !!state || 'State is required'
      ],
      addressZipRules: [
        zip => !!zip || 'Zip code is required',
        zip => /^\d{5}$/.test(zip) || 'Zip code must contain 5 numbers'
      ],
      phoneNumberRules: [
        phone => !!phone || 'Phone number is required',
        phone => /^\([2-9]\d{2}\)\d{3}-\d{4}$/.test(phone) || 'Phone number must be in (XXX)XXX-XXXX format and not start with 0 or 1'
      ],
      foodTypeRules: [
        type => !!type || 'Food type is required'
      ],
      avgFoodPriceRules: [
        price => !!price || 'Food price is required'
      ],
      foodPreferenceRules: [
        foodPreference => !!foodPreference || 'Food preference is required'
      ]
    },
    constants: {
      securityQuestionsSet1: [{
        id: 1,
        question: 'Who was the company you first worked for?'
      },
      {
        id: 2,
        question: 'Where did you go to highschool or college?'
      },
      {
        id: 3,
        question: 'What was the name of the teacher who gave you your first failing grade?'
      }],
      securityQuestionsSet2: [{
        id: 4,
        question: 'What is your favorite song?'
      },
      {
        id: 5,
        question: 'What is your mother\'s maiden name?'
      },
      {
        id: 6,
        question: 'What is your favorite sports team?'
      }],
      securityQuestionsSet3: [{
        id: 7,
        question: 'What was the name of your first crush?'
      },
      {
        id: 8,
        question: 'What is the name of your hometown?'
      },
      {
        id: 9,
        question: 'What was the name of your first pet?'
      }],
      timeZones: [{
        displayString: 'Pacific Standard Time',
        timeZoneName: 'Pacific Standard Time'
      }],
      dayOfWeek: [
        'Sunday',
        'Monday',
        'Tuesday',
        'Wednesday',
        'Thursday',
        'Friday',
        'Saturday'
      ],
      avgFoodPrices: [{
        id: 1,
        price: '$0.00 to $10.00'
      },
      {
        id: 2,
        price: '$10.01 to $50.00'
      },
      {
        id: 3,
        price: '$50.01+'
      }],
      foodTypes: [{
        id: 0,
        type: 'Mexican Food'
      },
      {
        id: 1,
        type: 'Italian Cuisine'
      },
      {
        id: 2,
        type: 'Thai Food'
      },
      {
        id: 3,
        type: 'Greek Cuisine'
      },
      {
        id: 4,
        type: 'Chinese Food'
      },
      {
        id: 5,
        type: 'Japanese Cuisine'
      },
      {
        id: 6,
        type: 'American Food'
      },
      {
        id: 7,
        type: 'Mediterranean Cuisine'
      },
      {
        id: 8,
        type: 'French Food'
      },
      {
        id: 9,
        type: 'Spanish Cuisine'
      },
      {
        id: 10,
        type: 'German Food'
      },
      {
        id: 11,
        type: 'Korean Food'
      },
      {
        id: 12,
        type: 'Vietnamese Food'
      },
      {
        id: 13,
        type: 'Turkish Cuisine'
      },
      {
        id: 14,
        type: 'Caribbean Food'
      }],
      foodPreferences: [
        'Gluten-free',
        'Halal',
        'Kosher',
        'Lacto-vegetarian',
        'Pescetarian',
        'Vegan',
        'Vegetarian'
      ],
      states: [{
        id: 1,
        name: 'California',
        abbreviation: 'CA'
      }],
      distanceRestaurantSelections: [
        1,
        5,
        10,
        15
      ]
    }
  },
  getters: {
    mapUrl: state => {
      var origin = state.originAddress
      var destination = state.destinationAddress
      var url = state.googleMapsBaseUrl
      url += '&origin=' + origin.replace(' ', '+')
      url += '&destination=' + destination.replace(' ', '+')

      return url
    },
    totalPrice: state => {
      var temp = 0
      state.BillItems.forEach(function (element) {
        temp += element.menuItemPrice
      })
      return temp
    }
  },
  // @Ryan Methods should be lowercase in javascript [-Jenn]
  mutations: {
    setOriginAddress: (state, payload) => {
      state.originAddress.push({
        originAddress: payload
      })
    },
    setDestinationAddress: (state, payload) => {
      state.destinationAddress.push({
        destinationAddress: payload
      })
    },
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
    },
    setSelectedRestaurant: (state, payload) => {
      state.restaurantSelection.selectedRestaurant.restaurantId = payload.restaurantId
      state.restaurantSelection.selectedRestaurant.restaurantGeoCoordinates = payload.restaurantGeoCoordinates
      state.restaurantSelection.selectedRestaurant.clientUserGeoCoordinates = payload.clientUserGeoCoordinates
      state.restaurantSelection.selectedRestaurant.displayName = payload.displayName
      state.restaurantSelection.selectedRestaurant.address = payload.address
      state.restaurantSelection.selectedRestaurant.phoneNumber = payload.phoneNumber
      state.restaurantSelection.selectedRestaurant.businessHours = payload.businessHourDtos
    },
    updateShowSelectedRestaurant: (state, payload) => {
      state.restaurantSelection.showRestaurantSelectionSection = payload
    }
  },
  // Actions are necessary when performing asynchronous methods.
  actions: {
    setOriginAddress: (context, payload) => {
      setTimeout(function () {
        context.commit('setOriginAddress', payload)
      }, 250)
    },
    setDestinationAddress: (context, payload) => {
      setTimeout(function () {
        context.commit('setDestinationAddress', payload)
      }, 250)
    },
    AddToDictionary: (context, payload) => {
      setTimeout(function () {
        console.log('Added Food Item Name: ' + payload[0])
        console.log('Added Food Item Price: ' + payload[1])
        context.commit('AddToDictionary', payload)
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
    },
    setSelectedRestaurant: (context, payload) => {
      setTimeout(function () {
        context.commit('setSelectedRestaurant', payload)
      }, 250)
    },
    updateShowSelectedRestaurant: (context, payload) => {
      setTimeout(function () {
        context.commit('updateShowSelectedRestaurant', payload)
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
