import Vue from 'vue'
import Vuex from 'vuex'
import { setTimeout } from 'timers'

Vue.use(Vuex)

export const store = new Vuex.Store({
  // A state is a global variable that every Vue component can reference
  state: {
    isAuthenticated: true,
    authenticationToken: null,
    firstTimeUserToken: null,
    username: '',
    timer: null,
    originAddress: 'Los Angeles, CA',
    destinationAddress: '1250 Bellflower Blvd, Long Beach, CA',
    googleMapsBaseUrl: 'https://www.google.com/maps/embed/v1/directions?key=AIzaSyCfKElVtKARYlgvCdQXBImfjRH5rmUF0mg',
    uniqueUserCounter: 0,
    menuItems: [
    ],
    restaurantMenus: [
    ],
    showRestaurantMenuItems: false,
    restaurantMenuItems: [
    ],
    billItems: [
    ],
    billUsers: [
    ],
    // States pertaining to restaurant selection
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
        restaurantId: 26,
        displayName: 'Halal Guys',
        address: {
          street1: '',
          street2: '',
          city: '',
          state: '',
          zip: null
        },
        phoneNumber: '',
        businessHours: [],
        foodPreferences: null
      }
    },
    // Header values for Axios requests
    headers: {
      accessControlAllowOrigin: 'http://localhost:8080'
    },
    // Uniform Resource Locations for Axios requests
    urls: {
      userManagement: {
        createIndividualUser: 'http://localhost:8081/User/Registration/Individual',
        createRestaurantUser: 'http://localhost:8081/User/Registration/Restaurant',
        createAdminUser: 'http://localhost:8081/User/CreateAdmin',
        deactivateUser: 'http://localhost:8081/User/DeactivateUser',
        reactivateUser: 'http://localhost:8081/User/ReactivateUser',
        editUser: 'http://localhost:8081/User/EditUser',
        deleteUser: 'http://localhost:8081/User/DeleteUser'
      },
      foodPreferences: {
        getPreferences: 'http://localhost:8081/FoodPreferences/GetPreferences',
        editPreferences: 'http://localhost:8081/FoodPreferences/Edit'
      },
      restaurantSelection: {
        unregisteredUser: 'http://localhost:8081/RestaurantSelection/Unregistered/',
        registeredUser: 'http://localhost:8081/RestaurantSelection/Registered/'
      },
      profileManagement: {
        userProfile: 'http://localhost:8081/Profile/User',
        updateUserProfile: 'http://localhost:8081/Profile/User/Edit',
        restaurantProfile: 'http://localhost:8081/Profile/Restaurant',
        updateRestaurantProfile: 'http://localhost:8081/Profile/Restaurant/Edit',
        menuItemUpload: 'http://localhost:8081/Profile/Restaurant/Edit/MenuItemImageUpload',
        profileImageUpload: 'http://localhost:8081/Profile/User/Edit/ProfileImageUpload'
      },
      resetPassword: {
        getSecurityQuestions: 'http://localhost:8081/ResetPassword/GetSecurityQuestions',
        confirmSecurityAnswers: 'http://localhost:8081/ResetPassword/ConfirmSecurityAnswers',
        updatePassword: 'http://localhost:8081/ResetPassword/UpdatePassword'
      },
      sso: {
        login: 'http://localhost:8081/Sso/Login',
        createIndividualUser: 'http://localhost:8081/User/FirstTimeRegistration/Individual',
        createRestaurantUser: 'http://localhost:8081/User/FirstTimeRegistration/Restaurant'
      }
    },
    // Rules for validations
    rules: {
      usernameRules: [
        username => !!username || 'Username is required',
        username => /^[A-Za-z\d]+$/.test(username) || 'Username must contain only letters and numbers'
      ],
      usernameNotRequiredRule: [
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
      securityQuestionRules: [
        securityQuestion => !!securityQuestion || 'Security question is required'
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
      ],
      businessDayRules: [
        businessDay => !!businessDay || 'Business day is required'
      ],
      businessHourRules: [
        businessHour => !!businessHour || 'Business hour is required'
      ]
    },
    // Constants are data that are non-changing
    constants: {
      defaultProfilePicturePath: '@/assets/DefaultProfileImage.png',
      securityQuestions: [{
        id: 0,
        questions: [
          {
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
          }
        ]
      },
      {
        id: 1,
        questions: [
          {
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
          }
        ]
      },
      {
        id: 2,
        questions: [
          {
            id: 7,
            question: 'What was the name of your first crush?'
          },
          {
            id: 8,
            question: 'What is the name of your hometown?'
          },
          {
            id: 9,
            question: 'What is the name of your first pet?'
          }
        ]
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
      ],
      restaurantDetails: [{
        property: 'avgFoodPrice',
        displayString: 'Average Food Price'
      },
      {
        property: 'hasReservations',
        displayString: 'Reservations'
      },
      {
        property: 'hasDelivery',
        displayString: 'Delivery'
      },
      {
        property: 'hasTakeOut',
        displayString: 'Take-out'
      },
      {
        property: 'acceptCreditCards',
        displayString: 'Accept Credit Cards'
      },
      {
        property: 'attire',
        displayString: 'Attire'
      },
      {
        property: 'servesAlcohol',
        displayString: 'Serves Alcohol'
      },
      {
        property: 'hasOutdoorSeating',
        displayString: 'Outdoor Seating'
      },
      {
        property: 'hasTv',
        displayString: 'TV'
      },
      {
        property: 'hasDriveThru',
        displayString: 'Drive-Thru'
      },
      {
        property: 'caters',
        displayString: 'Caters'
      },
      {
        property: 'allowsPets',
        displayString: 'Allow Pets'
      },
      {
        property: 'foodType',
        displayString: 'Food Type'
      }]
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
      state.billItems.forEach(function (element) {
        temp = temp + element.itemPrice
      })
      return temp.toFixed(2)
    },
    getClaim: state => {
    }
  },
  // Mutations are called to change the states in the store synchronously
  mutations: {
    originAddress: (state, payload) => {
      state.originAddress = payload
    },
    destinationAddress: (state, payload) => {
      state.destinationAddress = payload
    },
    addToDictionary: (state, payload) => {
      state.menuItems.push({
        itemName: payload[0],
        itemPrice: payload[1],
        selected: []
      })
    },
    addBillUser: (state, payload) => {
      state.billUsers.push({
        name: payload[0],
        uID: payload[1],
        moneyOwes: 0.00
      })
    },
    populateRestaurantMenus: (state, payload) => {
      payload.forEach(function (element, index) {
        console.log(element)
        state.restaurantMenus[index] = element
      })
    },
    useCustomMenu: (state) => {
      state.showRestaurantMenuItems = false
    },
    populateDictionary: (state, payload) => {
      state.showRestaurantMenuItems = true
      state.restaurantMenuItems.splice(0, state.restaurantMenuItems.length)
      payload.forEach(function (element, index) {
        state.restaurantMenuItems.push({
          itemName: element.itemName,
          itemPrice: element.itemPrice,
          selected: []
        })
      })
    },
    editDictionaryItem: (state, payload) => {
      state.menuItems[payload[0]].itemName = payload[1]
      state.menuItems[payload[0]].itemPrice = payload[2]
    },
    editBillItem: (state, payload) => {
      Vue.set(state.billItems[payload[0]], 'itemName', payload[1])
      Vue.set(state.billItems[payload[0]], 'itemPrice', payload[2])
      // state.billItems[payload[0]].itemName = payload[1]
      // state.billItems[payload[0]].itemPrice = payload[2]
    },
    removeFromDictionary: (state, payload) => {
      console.log('Dictionary Store Mutation Index: ' + payload)
      state.menuItems.splice(payload, 1)
    },
    removeFromBillTable: (state, payload) => {
      console.log('Bill Store Mutation Index: ' + payload)
      state.billItems.splice(payload, 1)
    },
    removeUser: (state, payload) => {
      console.log('User Store Mutation Index ' + payload)
      state.billUsers.forEach(function (element, index) {
        if (element.uID === payload) {
          state.billUsers.splice(index, 1)
        }
      })
      for (var i = 0, len1 = state.billItems.length; i < len1; i++) {
        for (var j = 0, len2 = state.billItems[i].selected.length; j < len2; j++) {
          if (state.billItems[i].selected[j] === payload) {
            state.billItems[i].selected.splice(j, 1)
          }
        }
      };
    },
    updateUserMoneyOwes: (state, payload) => {
      var billItemPriceSplit = state.billItems[payload].itemPrice / state.billItems[payload].selected.length
      for (var i = 0; i < state.billItems[payload].selected.length; i++) {
        for (var j = 0; j < state.billUsers.length; j++) {
          if (state.billItems[payload].selected[i] === state.billUsers[j].uID) {
            state.billUsers[j].moneyOwes += Number(state.billItems[payload].itemPrice / state.billItems[payload].selected.length).toFixed(2)
          }
        }
      }
    },
    setSelectedRestaurant: (state, payload) => {
      state.originAddress = payload.clientCity + ',' + payload.clientState
      if (payload.address.street2 === '') {
        state.destinationAddress = payload.address.street1 + ',' + payload.address.city + ',' + payload.address.state + ',' + payload.address.zip
      } else {
        state.destinationAddress = payload.address.street1 + ',' + payload.address.street2 + ',' + payload.address.city + ',' + payload.address.state + ',' + payload.address.zip
      }
      state.restaurantSelection.selectedRestaurant.restaurantId = payload.restaurantId
      state.restaurantSelection.selectedRestaurant.restaurantGeoCoordinates = payload.restaurantGeoCoordinates
      state.restaurantSelection.selectedRestaurant.clientUserGeoCoordinates = payload.clientUserGeoCoordinates
      state.restaurantSelection.selectedRestaurant.displayName = payload.displayName
      state.restaurantSelection.selectedRestaurant.address = payload.address
      state.restaurantSelection.selectedRestaurant.phoneNumber = payload.phoneNumber
      state.restaurantSelection.selectedRestaurant.businessHours = payload.businessHourDtos
      state.restaurantSelection.selectedRestaurant.foodPreferences = payload.foodPreferences
    },
    getAuthenticationToken: (state, payload) => {
      state.isAuthenticated = true
      state.authenticationToken = payload.auth
    },
    setAuthenticationToken: (state, payload) => {
      state.authenticationToken = payload
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
    addToDictionary: (context, payload) => {
      setTimeout(function () {
        console.log('Added Food Item Name: ' + payload[0])
        console.log('Added Food Item Price: ' + payload[1])
        context.commit('addToDictionary', payload)
      }, 250)
    },
    addBillUser: (context, payload) => {
      setTimeout(function () {
        console.log('Added New Bill User: ' + payload)
        context.commit('addBillUser', payload)
      }, 250)
    },
    populateRestaurantMenus: (context, payload) => {
      setTimeout(function () {
        context.commit('populateRestaurantMenus', payload)
      }, 250)
    },
    useCustomMenu: (context) => {
      setTimeout(function () {
        context.commit('useCustomMenu')
      }, 250)
    },
    populateDictionary: (context, payload) => {
      setTimeout(function () {
        context.commit('populateDictionary', payload)
      }, 250)
    },
    editDictionaryItem: (context, payload) => {
      setTimeout(function () {
        context.commit('editDictionaryItem', payload)
      }, 250)
    },
    editBillItem: (context, payload) => {
      setTimeout(function () {
        context.commit('editBillItem', payload)
      }, 250)
    },
    removeFromDictionary: (context, payload) => {
      setTimeout(function () {
        context.commit('removeFromDictionary', payload)
      }, 250)
    },
    removeFromBillTable: (context, payload) => {
      setTimeout(function () {
        context.commit('removeFromBillTable', payload)
      }, 250)
    },
    removeUser: (context, payload) => {
      setTimeout(function () {
        context.commit('removeUser', payload)
      }, 250)
    },
    updateUserMoneyOwes: (context, payload) => {
      setTimeout(function () {
        context.commit('updateUserMoneyOwes', payload)
      }, 250)
    },
    setSelectedRestaurant: (context, payload) => {
      setTimeout(function () {
        context.commit('setSelectedRestaurant', payload)
      }, 250)
    },
    // TODO: @Ahmed same with this one. [-Jenn]
    getAuthenticationToken: (context, payload) => {
      setTimeout(function () {
        context.commit('getAuthenticationToken', payload)
      }, 250)
    },
    setAuthenticationToken: (context, payload) => {
      setTimeout(function () {
        context.commit('setAuthenticationToken', payload)
      }, 250)
    }
  }
})
