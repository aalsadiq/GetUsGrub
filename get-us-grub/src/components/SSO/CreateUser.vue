<template>
  <div id="create-user">
    <div id="success">
      <!-- Success messages for registration -->
      <v-alert type="success" :value="showSuccess">
        <span>
          Success! User <code>{{ authentication.userAccount.username }}</code> has been created.
        </span>
      </v-alert>
    </div>
    <div v-show="showError" id="error-div">
      <!-- Error messages for registration -->
      <v-alert id="registration-error" :value=true icon='warning'>
        <span id="error-title">
          An error has occurred
        </span>
      </v-alert>
      <!-- Card to show error messages -->
        <v-card id="error-card">
          {{ errors }}
        </v-card>
    </div>
    <v-toolbar dark tabs flat>
      <v-tabs v-model="tabs" icons-and-text centered dark color="deep-orange darken-1">
        <v-spacer/>
        <v-tab href="#user">User
          <v-icon>face</v-icon>
        </v-tab>
        <v-spacer/>
        <v-tab href="#restaurant">Restaurant
          <v-icon>store</v-icon>
        </v-tab>
        <v-spacer/>
      </v-tabs>
    </v-toolbar>
    <v-tabs-items v-model="tabs" v-resize="onResize">
      <v-tab-item v-for="content in ['user', 'restaurant']" :key='content' :id='content'>
        <v-stepper v-model='formStep' :vertical="windowSize.width <= mobileScreenWidth">
          <v-stepper-header v-if="windowSize.width > mobileScreenWidth">
            <!-- USER SECTIONS-->
            <v-divider />
            <v-stepper-step step='1' :complete='formStep > 1'>Welcome</v-stepper-step>
            <v-divider />
            <v-stepper-step step='2' :complete='formStep > 2'>Authentication</v-stepper-step>
            <v-divider />
            <v-stepper-step step='3' :complete='formStep > 3'>Security Questions</v-stepper-step>
            <v-divider />
            <v-stepper-step step='4' :complete='formStep > 4'>Profile</v-stepper-step>
            <v-divider />
            <!-- ADDITIONAL RESTAURANT SECTIONS-->
            <v-stepper-step v-if="tabs == 'restaurant'" step='5' :complete='formStep > 5'>Business Hours</v-stepper-step>
            <v-divider  v-if="tabs == 'restaurant'"/>
            <v-stepper-step v-if="tabs == 'restaurant'" step='6' :complete='formStep > 6'>Contact Information</v-stepper-step>
            <v-divider  v-if="tabs == 'restaurant'"/>
          </v-stepper-header>
          <v-stepper-items>
            <!-- WELCOME MESSAGE -->
            <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="1" :complete="formStep > 1">
              Welcome
            </v-stepper-step>
            <v-stepper-content step='1'>
              <welcome-message />
              <v-btn color="primary" @click="nextStep(content)">Next</v-btn>
            </v-stepper-content>
            <!-- AUTHENTICATION FORM -->
            <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="2" :complete="formStep > 2">
              Authentication
            </v-stepper-step>
            <v-stepper-content step='2'>
              <auth-form v-model='authentication' :disabled="disable" />
              <v-btn color='grey lighten-5' @click='previousStep'>Previous</v-btn>
              <v-btn color="primary" :disabled="!authentication.isValid" @click="nextStep(content)">Next</v-btn>
            </v-stepper-content>
            <!-- SECURITY QUESTION FORM -->
            <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="3" :complete="formStep > 3">
              Security Questions
            </v-stepper-step>
            <v-stepper-content step='3'>
              <security-questions-form v-model='securityQuestions' :disabled="disable" />
              <v-btn color='grey lighten-5' @click='previousStep'>Previous</v-btn>
              <v-btn color="primary" :disabled="!securityQuestions.isValid" @click="nextStep(content)">Next</v-btn>
            </v-stepper-content>
            <!-- PROFILE FORM -->
            <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="4" :complete="formStep > 4">
              Profile
            </v-stepper-step>
            <v-stepper-content step='4'>
              <profile-form v-model='profile' :type='content' :disabled="disable" />
              <v-btn color='grey lighten-5' @click='previousStep'>Previous</v-btn>
              <v-btn color="primary" v-if="content === 'user'" :disabled="!isProfileValid(content) || disable" @click="submitUser">Submit</v-btn>
              <v-btn color="primary" v-if="content === 'restaurant'" :disabled="!isProfileValid(content)" @click="nextStep(content)">Next</v-btn>
            </v-stepper-content>
            <!-- BUSINESS HOURS FORM -->
            <v-stepper-step v-if="windowSize.width <= mobileScreenWidth && content=='restaurant'" step="5" :complete="formStep > 5">
              Business Hours
            </v-stepper-step>
            <v-stepper-content step='5'>
              <business-hours-form v-model='businessHours' :disabled="disable" />
              <v-btn color='grey lighten-5' @click='formStep = 3'>Previous</v-btn>
              <v-btn color='primary' :disabled='!businessHours.isValid' @click='nextStep(content)'>Next</v-btn>
            </v-stepper-content>
            <!-- CONTACT INFO FORM -->
            <v-stepper-step v-if="windowSize.width <= mobileScreenWidth && content=='restaurant'" step="6" :complete="formStep > 6">
              Contact Information
            </v-stepper-step>
            <v-stepper-content step='6'>
              <contact-info-form v-model='profile' :disabled="disable" />
              <v-btn color='grey lighten-5' @click='formStep = 4'>Previous</v-btn>
              <v-btn color="primary" :disabled="!profile.isValidContactInfo || disable" @click="submitRestaurant">Submit</v-btn>
            </v-stepper-content>
          </v-stepper-items>
        </v-stepper>
      </v-tab-item>
    </v-tabs-items>
  </div>
</template>

<script>
import WelcomeMessage from './WelcomeMessage'
import AuthenticationForm from './AuthenticationForm'
import SecurityQuestionsForm from './SecurityQuestionsForm'
import ProfileForm from './ProfileForm'
import BusinessHoursForm from './BusinessHoursForm'
import ContactInfoForm from './ContactInfoForm'
import axios from 'axios'

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
    showError: false,
    errors: [],
    showSuccess: false,
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
    responseData: '',
    windowSize: {
      width: 0,
      height: 0
    },
    mobileScreenWidth: 600
  }),
  mounted () {
    this.mobileScreenWidth = this.$store.state.constants.mobileScreenWidth
    this.onResize()
  },
  methods: {
    onResize () {
      this.windowSize = { width: window.innerWidth, height: window.innerHeight }
    },
    nextStep (type) {
      var totalSteps = 4

      if (type === 'restaurant') {
        totalSteps = 6
      }
      // Increment form step, but bind by the total number of steps
      if (++this.formStep > totalSteps) {
        this.formStep = totalSteps
      }
    },
    submitUser () {
      this.disable = true
      var dto = {
        userAccountDto: this.authentication.userAccount,
        securityQuestionDtos: this.securityQuestions.questions,
        userProfileDto: this.profile.userProfile
      }
      axios.post(this.$store.state.urls.sso.createIndividualUser,
        dto,
        {
          headers: { Authorization: `Bearer ${this.$store.state.firstTimeUserToken}` }
        }).then(response => {
        this.disable = false
        this.showSuccess = true
      }).catch(error => {
        this.showSuccess = false
        this.showError = true
        this.disable = false
        try {
          if (error.status === 401) {
            // Route to Unauthorized page
            this.$router.push('Unauthorized')
          }
          if (error.status === 403) {
            // Route to Forbidden page
            this.$router.push('Forbidden')
          }
          if (error.status === 404) {
            // Route to ResourceNotFound page
            this.$router.push('ResourceNotFound')
          }
          if (error.status === 500) {
            // Route to InternalServerError page
            this.$router.push('InternalServerError')
          } else {
            this.errors = error.message
          }
          Promise.reject(error)
        } catch (ex) {
          this.errors = ex.message
          Promise.reject(this.errors)
        }
      })
    },
    submitRestaurant () {
      this.disable = true
      var dto = {
        userAccountDto: this.authentication.userAccount,
        securityQuestionDtos: this.securityQuestions.questions,
        userProfileDto: this.profile.userProfile,
        restaurantProfileDto: this.profile.restaurantProfile,
        foodPreferences: this.profile.foodPreferences,
        timeZone: this.businessHours.timeZone,
        businessHourDtos: this.businessHours.businessHours
      }
      axios({
        method: 'POST',
        url: this.$store.state.urls.sso.createRestaurantUser,
        headers: {
          Authorization: `bearer ${this.$store.state.firstTimeUserToken}`
        },
        data: dto
      }).then(response => {
        this.disable = false
        this.username = response.data
        this.showSuccess = true
      }).catch(error => {
        this.showSuccess = false
        this.showError = true
        this.disable = false
        try {
          if (error.response.status === 401) {
            // Route to Unauthorized page
            this.$router.push('Unauthorized')
          }
          if (error.response.status === 403) {
            // Route to Forbidden page
            this.$router.push('Forbidden')
          }
          if (error.response.status === 404) {
            // Route to ResourceNotFound page
            this.$router.push('ResourceNotFound')
          }
          if (error.response.status === 500) {
            // Route to InternalServerError page
            this.$router.push('InternalServerError')
          } else {
            this.errors = JSON.parse(JSON.parse(error.response.data.message))
          }
          Promise.reject(error)
        } catch (ex) {
          this.errors = error.response.data
          Promise.reject(error)
        }
      })
    },
    previousStep () {
      if (this.formStep > 1) {
        this.formStep--
      }
    },
    switchAccountType (type) {
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
  }
}
</script>

<style scoped>
.application .theme--light.stepper--vertical
.stepper__content:not(:last-child),
.theme--light .stepper--vertical
.stepper__content:not(:last-child) {
  border: none;
}
</style>
