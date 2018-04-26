export default {
  // Default state for the Vuex store
  defaultState: {
    isAuthenticated: true,
    authenticationToken: null,
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
    // Header values for Axios requests
    headers: {
      accessControlAllowOrigin: 'http://localhost:8080'
    },
    // Uniform Resource Locations for Axios requests
    urls: {
      userManagement: {
        createIndividualUser: 'http://localhost:8081/User/Registration/Individual',
        createRestaurantUser: 'http://localhost:8081/User/Registration/Restaurant'
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
        updateRestaurantProfile: 'http://localhost:8081/Profile/Restaurant/Edit'
      },
      sso: {
        login: 'http://localhost:8081/Sso/Login'
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
  }
}
