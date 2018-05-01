import Vue from 'vue'
import Vuex from 'vuex'
import { setTimeout } from 'timers'
import createPersistedState from 'vuex-persistedstate'

Vue.use(Vuex)

export const store = new Vuex.Store({
  plugins: [createPersistedState({
    paths: [
      'isAuthenticated',
      'authenticationToken',
      'username',
      'timer',
      'uniqueCounter',
      'menuItems',
      'restaurantMenus',
      'showRestaurantMenuItems',
      'billItems',
      'billUsers',
      'restaurantSelection.selectedRestaurant'
    ]
  })],
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
    uniqueCounter: 0,
    menuItemImagePath: 'http://localhost:64525/DefaultImages/DefaultMenuItemImage.png',
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
        businessHours: [],
        foodPreferences: null
      }
    },
    // Uniform Resource Locations for Axios requests
    urls: {
      userManagement: {
        createIndividualUser: 'http://localhost:8081/api/v1/User/Registration/Individual',
        createRestaurantUser: 'http://localhost:8081/api/v1/User/Registration/Restaurant',
        createAdminUser: 'http://localhost:8081/api/v1/User/CreateAdmin',
        deactivateUser: 'http://localhost:8081/api/v1/User/DeactivateUser',
        reactivateUser: 'http://localhost:8081/api/v1/User/ReactivateUser',
        editUser: 'http://localhost:8081/api/v1/User/EditUser',
        deleteUser: 'http://localhost:8081/api/v1/User/DeleteUser'
      },
      foodPreferences: {
        getPreferences: 'http://localhost:8081/api/v1/FoodPreferences/GetPreferences',
        editPreferences: 'http://localhost:8081/api/v1/FoodPreferences/Edit'
      },
      restaurantSelection: {
        unregisteredUser: 'http://localhost:8081/api/v1/RestaurantSelection/Unregistered/',
        registeredUser: 'http://localhost:8081/api/v1/RestaurantSelection/Registered/'
      },
      profileManagement: {
        userProfile: 'http://localhost:8081/api/v1/Profile/User',
        updateUserProfile: 'http://localhost:8081/api/v1/Profile/User/Edit',
        restaurantProfile: 'http://localhost:8081/api/v1/Profile/Restaurant',
        updateRestaurantProfile: 'http://localhost:8081/api/v1/Profile/Restaurant/Edit',
        menuItemUpload: 'http://localhost:8081/api/v1/Profile/Restaurant/Edit/MenuItemImageUpload',
        profileImageUpload: 'http://localhost:8081/api/v1/Profile/User/Edit/ProfileImageUpload'
      },
      resetPassword: {
        getSecurityQuestions: 'http://localhost:8081/api/v1/ResetPassword/GetSecurityQuestions',
        confirmSecurityAnswers: 'http://localhost:8081/api/v1/ResetPassword/ConfirmSecurityAnswers',
        updatePassword: 'http://localhost:8081/api/v1/ResetPassword/UpdatePassword'
      },
      sso: {
        login: 'http://localhost:8081/api/v1/Sso/Login',
        createIndividualUser: 'http://localhost:8081/api/v1/User/FirstTimeRegistration/Individual',
        createRestaurantUser: 'http://localhost:8081/api/v1/User/FirstTimeRegistration/Restaurant'
      },
      restaurantBillSplitter: {
        getRestaurantMenus: 'http://localhost:8081/api/v1/RestaurantBillSplitter/Restaurant'
      },
      login: {
        loginUser: 'http://localhost:8081/api/v1/Login'
      },
      logout: {
        logoutUser: 'http://localhost:8081/api/v1/Logout'
      },
      renewSession: {
        requestNewToken: 'http://localhost:8081/api/v1/RenewSession'
      },
      pwnedPassword: {
        range: 'https://api.pwnedpasswords.com/range/'
      }
    },
    // Rules for validations
    rules: {
      addBillUserRules: [
        billUser => !!billUser || 'Required'
      ],
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
      loginPasswordRules: [
        password => !!password || 'Password is required',
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
      ],
      timeZoneRules: [
        timeZone => !!timeZone || 'Time zone is required'
      ],
      menuNameRules: [
        menuName => !!menuName || 'Menu name is required'
      ],
      itemNameRules: [
        itemName => !!itemName || 'Menu item name is required'
      ],
      itemPriceRules: [
        itemPrice => !!itemPrice || 'Item price is required',
        itemPrice => /^[0-9]\d*(((,\d{3}){1})?(\.\d{0,2})?)$/.test(itemPrice) || 'Incorrect price format'
      ],
      tagRules: [
        tag => !!tag || 'Tag is required'
      ]
    },
    // Constants are data that are non-changing
    constants: {
      genericErrorMessage: 'An unexpected error has occured.',
      mobileScreenWidth: 1000,
      inputValidationDelay: 250,
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
    },
    convertFromUSDtoInt: (state) => (usDollars) => {
      return parseInt(usDollars * 100.00)
    },
    convertFromInttoUSD: (state) => (usDollars) => {
      return usDollars / 100
    },
    getUniqueCounter: state => {
      return state.uniqueCounter
    },
    getTotalPercentage: (state) => (billItemIndex) => {
      var temp = 0
      state.billItems[billItemIndex].manual.forEach(function (element, index) {
        temp += Number(state.billItems[billItemIndex].manual[index].percentage)
      })
      return temp
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
        itemPrice: payload[1]
      })
    },
    addBillUser: (state, payload) => {
      state.billUsers.push({
        name: payload[0],
        uID: payload[1],
        billItemsIn: [],
        moneyOwes: 0,
        tipAssigned: 0
      })
    },
    populateRestaurantMenus: (state, payload) => {
      payload.forEach(function (element, index) {
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
          itemPrice: element.itemPrice
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
    },
    removeFromDictionary: (state, payload) => {
      state.menuItems.splice(payload, 1)
    },
    removeFromBillTable: (state, payload) => {
      state.billItems.splice(payload, 1)
    },
    removeUser: (state, payload) => {
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
    updateUserMoneyOwesFromSelected: (state, payload) => {
      var oldSplit
      var newSplit
      if (payload.oldSelected.length === 0 && payload.newSelected.length === 1) { // When the first new user is ADDED to selected list
        state.billUsers[state.billUsers.findIndex(x => x.uID === payload.newSelected[0])].moneyOwes += payload.billItem.itemPrice
      } else if (payload.oldSelected.length === 1 && payload.newSelected.length === 0) { // When the last user is REMOVED from the selected list
        state.billUsers[state.billUsers.findIndex(x => x.uID === payload.oldSelected[0])].moneyOwes -= payload.billItem.itemPrice
      } else if (payload.oldSelected.length < payload.newSelected.length) { // When a new user is ADDED to selected list
        oldSplit = Math.ceil(Math.ceil(payload.billItem.itemPrice * ((100 - payload.totalPercentage) / 100)) / (payload.oldSelected.length - payload.billItem.selectedManual.length))
        newSplit = Math.ceil(Math.ceil(payload.billItem.itemPrice * ((100 - payload.totalPercentage) / 100)) / (payload.newSelected.length - payload.billItem.selectedManual.length))
        payload.oldSelected.forEach(function (element, index) {
          if (!state.billItems[payload.billItemIndex].selectedManual.includes(element)) {
            state.billUsers[state.billUsers.findIndex(x => x.uID === element)].moneyOwes -= oldSplit
          }
        })
        payload.newSelected.forEach(function (element, index) {
          if (!state.billItems[payload.billItemIndex].selectedManual.includes(element)) {
            state.billUsers[state.billUsers.findIndex(x => x.uID === element)].moneyOwes += newSplit
          }
        })
      } else if (payload.oldSelected.length > payload.newSelected.length) { // When a user is REMOVED from the selected list
        oldSplit = Math.ceil(Math.ceil(payload.billItem.itemPrice * ((100 - payload.totalPercentage) / 100)) / payload.oldSelected.length)
        newSplit = Math.ceil(Math.ceil(payload.billItem.itemPrice * ((100 - payload.totalPercentage) / 100)) / payload.newSelected.length)
        payload.oldSelected.forEach(function (element, index) {
          if (!state.billItems[payload.billItemIndex].selectedManual.includes(element)) {
            state.billUsers[state.billUsers.findIndex(x => x.uID === element)].moneyOwes -= oldSplit
          }
        })
        payload.newSelected.forEach(function (element, index) {
          if (!state.billItems[payload.billItemIndex].selectedManual.includes(element)) {
            state.billUsers[state.billUsers.findIndex(x => x.uID === element)].moneyOwes += newSplit
          }
        })
      }
    },
    addManual: (state, payload) => {
      state.billUsers[payload.billUserIndex].moneyOwes -= Math.ceil(Math.ceil(payload.billItem.itemPrice * ((100 - payload.totalPercentage) / 100)) / (payload.billItem.selected.length - payload.billItem.manual.length))
      state.billItems[payload.billItemIndex].manual.push({
        manualUID: payload.newUID,
        percentage: 0
      })
    },
    removeManual: (state, payload) => {
      var index = payload.billItem.manual.findIndex(x => x.manualUID === payload.removedUID)
      state.billItems[payload.billItemIndex].manual.splice(index, 1)
      state.billUsers[payload.billUserIndex].moneyOwes -= Math.ceil(Math.ceil(payload.billItem.itemPrice * (payload.oldPercentage / 100)) / (payload.billItem.selected.length - (payload.billItem.selectedManual.length - 1)))
      state.billUsers[payload.billUserIndex].moneyOwes += Math.ceil(Math.ceil(payload.billItem.itemPrice * ((100 - (payload.totalPercentage - payload.oldPercentage)) / 100)) / (payload.billItem.selected.length - (payload.billItem.selectedManual.length)))
    },
    removeSelectedManual: (state, payload) => {
      var index = payload.billItem.selectedManual.findIndex(x => x === payload.removedUID)
      state.billItems[payload.billItemIndex].selectedManual.splice(index, 1)
    },
    updateSelectedManual: (state, payload) => {
    },
    updateUserMoneyOwesFromEditItem: (state, payload) => {
      var oldSplit = Math.ceil(payload.Item.itemPrice / payload.Item.selected.length)
      var newSplit = Math.ceil(payload.newItemPrice / payload.Item.selected.length)
      state.billItems[payload.itemIndex].selected.forEach(function (select, selectedIndex) {
        state.billUsers[state.billUsers.findIndex(x => x.uID === select)].moneyOwes -= oldSplit
        state.billUsers[state.billUsers.findIndex(x => x.uID === select)].moneyOwes += newSplit
      })
    },
    updateUserMoneyOwesFromDeleteItem: (state, payload) => {
      var oldSplit = Math.ceil(state.billItems[payload.itemIndex].itemPrice / state.billItems[payload.itemIndex].selected.length)
      state.billItems[payload.itemIndex].selected.forEach(function (select, selectIndex) {
        state.billUsers[state.billUsers.findIndex(x => x.uID === select)].moneyOwes -= oldSplit
      })
    },
    updateBillUsersTipAssigned: (state, payload) => {
      state.billUsers.forEach(function (element, index) {
        Vue.set(state.billUsers[index], 'tipAssigned', payload[index].tip)
      })
    },
    updateUserMoneyOwesFromDeleteUser: (state, payload) => {
      var oldSplit
      var newSplit
      state.billItems.forEach(function (element, index) {
        if (state.billItems[index].selected.includes(payload.billUserUID)) {
          oldSplit = Math.ceil(state.billItems[index].itemPrice / state.billItems[index].selected.length)
          newSplit = Math.ceil(state.billItems[index].itemPrice / (state.billItems[index].selected.length - 1))
          state.billItems[index].selected.forEach(function (select, selectIndex) {
            if (select !== payload.billUserUID) {
              state.billUsers[state.billUsers.findIndex(x => x.uID === select)].moneyOwes -= oldSplit
              state.billUsers[state.billUsers.findIndex(x => x.uID === select)].moneyOwes += newSplit
            }
          })
        }
      })
    },
    updatePercentage: (state, payload) => {
      state.billUsers[payload.billUserIndex].moneyOwes -= Math.ceil(payload.billItem.itemPrice * (payload.oldPercentage / 100))
      state.billUsers[payload.billUserIndex].moneyOwes += Math.ceil(payload.billItem.itemPrice * (payload.newPercentage / 100))
    },
    incrementUniqueCounter: (state) => {
      state.uniqueCounter++
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
    setIsAuthenticated: (state, payload) => {
      state.isAuthenticated = payload
    },
    setAuthenticationToken: (state, payload) => {
      state.authenticationToken = payload
    },
    setUsername: (state, payload) => {
      state.username = payload
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
        context.commit('addToDictionary', payload)
      }, 250)
    },
    addBillUser: (context, payload) => {
      setTimeout(function () {
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
    updateUserMoneyOwesFromSelected: (context, payload) => {
      setTimeout(function () {
        context.commit('updateUserMoneyOwesFromSelected', payload)
      }, 250)
    },
    updateUserMoneyOwesFromEditItem: (context, payload) => {
      setTimeout(function () {
        context.commit('updateUserMoneyOwesFromEditItem', payload)
      }, 250)
    },
    updateUserMoneyOwesFromDeleteItem: (context, payload) => {
      setTimeout(function () {
        context.commit('updateUserMoneyOwesFromDeleteItem', payload)
      }, 250)
    },
    updateUserMoneyOwesFromDeleteUser: (context, payload) => {
      setTimeout(function () {
        context.commit('updateUserMoneyOwesFromDeleteUser', payload)
      }, 250)
    },
    updateBillUsersTipAssigned: (context, payload) => {
      setTimeout(function () {
        context.commit('updateBillUsersTipAssigned', payload)
      }, 250)
    },
    incrementUniqueCounter: (context) => {
      context.commit('incrementUniqueCounter')
    },
    setSelectedRestaurant: (context, payload) => {
      setTimeout(function () {
        context.commit('setSelectedRestaurant', payload)
      }, 250)
    },
    setAuthenticationToken: (context, payload) => {
      setTimeout(function () {
        context.commit('setAuthenticationToken', payload)
      }, 250)
    },
    setIsAuthenticated: (context, payload) => {
      setTimeout(function () {
        context.commit('setIsAuthenticated', payload)
      }, 250)
    },
    setUsername: (context, payload) => {
      setTimeout(function () {
        context.commit('setUsername', payload)
      }, 250)
    }
  }
})
