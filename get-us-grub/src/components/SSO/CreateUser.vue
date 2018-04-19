<template>
  <div id='create-user'>
    <v-layout id='response'>
      <v-flex xs12>
        {{ responseDataStatus }} {{ responseData }}
      </v-flex>
    </v-layout>
    <v-layout>
      <v-flex xs12>
        <v-toolbar dark tabs flat>
          <v-tabs
            v-model='tabs'
            icons-and-text centered dark color='deep-orange accent-2'
            @input='switchAccountType(tabs)'
          >
            <v-spacer/>
            <v-tab href='#user'>
              User
              <v-icon>face</v-icon>
            </v-tab>
            <v-spacer/>
            <v-tab href='#restaurant'>
              Restaurant
            <v-icon>store</v-icon>
            </v-tab>
            <v-spacer/>
          </v-tabs>
        </v-toolbar>
        <v-tabs-items v-model="tabs">
          <v-tab-item v-for="content in ['user', 'restaurant']" :key='content' :id='content'>
            <v-stepper v-model='formStep' vertical>
              <v-stepper-header>
                <v-divider />
                <v-stepper-step step='1' :complete='formStep > 1'>Welcome</v-stepper-step>
                <v-divider />
                <v-stepper-step step='2' :complete='formStep > 2'>Authorization</v-stepper-step>
                <v-divider />
                <v-stepper-step step='3' :complete='formStep > 3'>Security Questions</v-stepper-step>
                <v-divider />
                <v-stepper-step step='4' :complete='formStep > 4'>Profile</v-stepper-step>
                <v-divider />
                <v-stepper-step v-if="tabs == 'restaurant'" step='5' :complete='formStep > 5'>Business Hours</v-stepper-step>
                <v-divider  v-if="tabs == 'restaurant'"/>
                <v-stepper-step v-if="tabs == 'restaurant'" step='6' :complete='formStep > 6'>Contact Information</v-stepper-step>
                <v-divider  v-if="tabs == 'restaurant'"/>
              </v-stepper-header>
              <v-stepper-items>
                <v-stepper-content step='1'>
                  <welcome-message />
                  <v-btn color="primary" @click="nextStep(content)">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step='2'>
                  <auth-form v-model='authentication'/>
                  <v-btn color='grey lighten-5' @click='previousStep'>Previous</v-btn>
                  <v-btn color="primary" :disabled="!authentication.isValid" @click="nextStep(content)">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step='3'>
                  <security-questions-form v-model='securityQuestions'/>
                  <v-btn color='grey lighten-5' @click='previousStep'>Previous</v-btn>
                  <v-btn color="primary" :disabled="!securityQuestions.isValid" @click="nextStep(content)">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step='4'>
                  <profile-form v-model='profile' :type='content' />
                  <v-btn color='grey lighten-5' @click='previousStep'>Previous</v-btn>
                  <v-btn color="primary" v-if="content === 'user'" :disabled="!isProfileValid(content)" @click="submitUser">Submit</v-btn>
                  <v-btn color="primary" v-if="content === 'restaurant'" :disabled="!isProfileValid(content)" @click="nextStep(content)">Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step='5'>
                  <business-hours-form v-model='businessHours' />
                  <v-btn color='grey lighten-5' @click='formStep = 3'>Previous</v-btn>
                  <v-btn color='primary' :disabled='!businessHours.isValid' @click='nextStep(content)'>Next</v-btn>
                </v-stepper-content>
                <v-stepper-content step='6'>
                  <contact-info-form v-model='profile' />
                  <v-btn color='grey lighten-5' @click='formStep = 4'>Previous</v-btn>
                  <v-btn color="primary" :disabled="!profile.isValidContactInfo" @click="submitUser">Submit</v-btn>
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
import WelcomeMessage from './WelcomeMessage'
import AuthenticationForm from './AuthenticationForm'
import SecurityQuestionsForm from './SecurityQuestionsForm'
import ProfileForm from './ProfileForm'
import BusinessHoursForm from './BusinessHoursForm'
import ContactInfoForm from './ContactInfoForm'

export default {
  name: 'SsoCreateUser',
  components: {
    'welcome-message': WelcomeMessage,
    'auth-form': AuthenticationForm,
    'security-questions-form': SecurityQuestionsForm,
    'profile-form': ProfileForm,
    'business-hours-form': BusinessHoursForm,
    'contact-info-form': ContactInfoForm
  },
  props: [],
  data: () => ({
    tabs: null,
    formStep: 0,
    lastRestaurantStep: 0,
    passwordErrorMessages: [],
    isPasswordValid: false,
    visible: false,
    disable: false,
    loader: null,
    loading: false,
    counter: 0,
    authentication: {
      userAccount: {
        username: '',
        password: ''
      },
      isValid: false
    },
    securityQuestions: {
      questions: [{
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
      isValid: false
    },
    profile: {
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
      isValidContactInfo: false,
      isValidRestaurantDetails: false,
      isValidUserDetails: false
    },
    businessHours: {
      businessHours: [],
      timeZone: '',
      isValid: false
    },
    responseDataStatus: '',
    responseData: ''
  }),
  methods: {
    nextStep (type) {
      var totalSteps = 4

      if (type === 'restaurant') {
        totalSteps = 6
      }
      // Increment form step, but bind by the total number of steps
      if (++this.formStep > totalSteps) {
        this.formStep = totalSteps
      }
      console.log(this.formStep)
    },
    submitUser () {
      var dto = {
        userAccountDto: this.authentication.userAccount,
        securityQuestionDtos: this.securityQuestions.questions,
        userProfileDto: this.profile.userProfile
      }

      // axios.post('http://localhost:8081/User/Registration/Individual', {
      //   userAccountDto: this.userAccount,
      //   securityQuestionDtos: this.securityQuestions,
      //   userProfileDto: this.userProfile
      // }).then(response => {
      //   this.validSecurityInput = true
      //   this.disable = false
      //   this.responseDataStatus = 'Success! User has been created: '
      //   this.responseData = response.data
      // }).catch(error => {
      //   this.validSecurityInput = true
      //   this.disable = false
      //   this.responseDataStatus = 'An error has occurred: '
      //   if (error.message === 'Network Error') {
      //     this.responseData = error.message
      //   } else {
      //     this.responseData = error.response
      //   }
      // })
      console.log(dto)
    },
    submitRestaurant () {
      var dto = {
        userAccountDto: this.authentication.userAccount,
        securityQuestionDtos: this.securityQuestions.questions,
        userProfileDto: this.profile.userProfile,
        restaurantProfileDto: this.profile.restaurantProfile,
        foodPreferences: this.profile.foodPreferences,
        timeZone: this.businessHours.timeZone,
        businessHourDtos: this.businessHours.businessHours
      }
      console.log(dto)
    },
    previousStep () {
      if (this.formStep > 1) {
        this.formStep--
      }
    },
    switchAccountType (type) {
      console.log('switched: ' + type)
      if (type === 'user') {
        this.lastRestaurantStep = this.step
        this.step = this.step % 3
      } else if (type === 'restaurant') {
        this.step = this.lastRestaurantStep
      }
    },
    isProfileValid (type) {
      if (type === 'user') {
        return this.profile.isValidUserDetails
      } else if (type === 'restaurant') {
        return this.profile.isValidRestaurantDetails
      } else {
        return false
      }
    }
  },
  created () {
    console.log('created create user')
  }
}
</script>
