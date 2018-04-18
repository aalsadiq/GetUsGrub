<template>
  <div id="create-user">
    <v-layout id="response">
      <v-flex xs12>
        {{ responseDataStatus }} {{ responseData }}
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex xs12>
        <v-toolbar dark tabs flat>
          <v-tabs v-model="tabs" icons-and-text centered dark color="deep-orange accent-2">
            <v-spacer/>
            <v-tab href="#user">
              User
              <v-icon>face</v-icon>
            </v-tab>
            <v-spacer/>
            <v-tab href="#restaurant">
              Restaurant
              <v-icon>store</v-icon>
            </v-tab>
            <v-spacer/>
          </v-tabs>
        </v-toolbar>
        <v-tabs-items v-model="tabs">
          <v-tab-item v-for="content in ['user', 'restaurant']" :key="content" :id="content">
            <v-stepper v-model="formStep" vertical>
              <v-stepper-header>
                <v-divider />
                <v-stepper-step step="1" :complete="formStep > 1">Identification</v-stepper-step>
                <v-divider />
                <v-stepper-step step="2" :complete="formStep > 2">Security Questions</v-stepper-step>
                <v-divider />
                <v-stepper-step v-if="content == 'restaurant'" step="3" :complete="formStep > 3">Restaurant Details</v-stepper-step>
                <v-divider  v-if="content == 'restaurant'"/>
                <v-stepper-step v-if="content == 'restaurant'" step="4" :complete="formStep > 4">Business Hours</v-stepper-step>
                <v-divider  v-if="content == 'restaurant'"/>
                <v-stepper-step v-if="content == 'restaurant'" step="5" :complete="formStep > 5">Contact Information</v-stepper-step>
                <v-divider  v-if="content == 'restaurant'"/>
              </v-stepper-header>
              <v-stepper-items>
                <v-stepper-content step="1">
                  <auth-form />
                </v-stepper-content>
                <v-stepper-content step="2">
                  <security-questions />
                </v-stepper-content>
                <v-stepper-content step="3">
                  <v-btn color="grey lighten-5" @click="formStep = 2">Previous</v-btn>
                  <v-btn color="primary" :disabled="!validRestaurantDetailsInput" @click="formStep = 4">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step="4">
                  <v-btn color="grey lighten-5" @click="formStep = 3">Previous</v-btn>
                  <v-btn color="primary" :disabled="!validBusinessHourInput" @click="formStep = 5">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step="5">
                  <v-btn color="grey lighten-5" @click="formStep = 4">Previous</v-btn>
                </v-stepper-content>
              </v-stepper-items>
            </v-stepper>
          </v-tab-item>
        </v-tabs-items>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import AuthenticationForm from './AuthenticationForm'
import SecurityQuestionsForm from './SecurityQuestionsForm'
export default {
  name: 'SSOCreateUser',
  components: {
    'auth-form': AuthenticationForm,
    'security-questions': SecurityQuestionsForm
  },
  props: [],
  data: () => ({
    tabs: null,
    formStep: 0,
    lastRestaurantStep: 0,
    passwordErrorMessages: [],
    isPasswordValid: false,
    validIdentificationInput: false,
    validSecurityInput: false,
    validBusinessHourInput: false,
    validRestaurantDetailsInput: false,
    validAddBusinessHour: false,
    validContactInput: false,
    visible: false,
    openMenu: false,
    closeMenu: false,
    openTimeSync: false,
    closeTimeSync: false,
    disable: false,
    loader: null,
    loading: false,
    counter: 0,
    timeZone: '',
    time: '',
    userAccount: {
      username: '',
      password: ''
    },
    securityQuestions: [{
      question: 0,
      answer: ''
    },
    {
      question: 0,
      answer: ''
    },
    {
      question: 0,
      answer: ''
    }],
    userProfile: {
      displayName: ''
    },
    restaurantProfile: {
      address: {
        street1: '',
        street2: '',
        city: '',
        state: '',
        zip: null
      },
      phoneNumber: '',
      details: {
        foodType: '',
        avgFoodPrice: null
      }
    },
    foodPreferences: [],
    businessHours: [],
    businessHour: {
      day: '',
      openTime: null,
      closeTime: null
    },
    responseDataStatus: '',
    responseData: ''
  }),
  methods: {
    switchAccountType (type) {
      alert(type)
      if (type === 'user') {
        this.lastRestaurantStep = this.step
        this.step = this.step % 2
      } else if (type === 'restaurant') {
        this.step = this.lastRestaurantStep
      }
    }
  }
}
</script>
