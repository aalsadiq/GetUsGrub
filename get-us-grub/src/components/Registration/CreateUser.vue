<template>
  <v-content>
    <v-layout justify-center>
    <v-container xs12 id="main-content">
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
        <v-tab-item v-for="content in ['user', 'restaurant']" :key="content" :id="content">
          <div v-if="content === 'user'">
            <v-stepper v-model="userStep" :vertical="windowSize.width <= mobileScreenWidth">
              <v-stepper-header v-if="windowSize.width > mobileScreenWidth">
                <v-divider></v-divider>
                <v-stepper-step step="1" :complete="userStep > 1">Identification</v-stepper-step>
                <v-divider></v-divider>
                <v-stepper-step step="2" :complete="userStep > 2">Security Questions</v-stepper-step>
                <v-divider></v-divider>
              </v-stepper-header>
              <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="1" :complete="userStep > 1">
                Identification
              </v-stepper-step>
              <v-stepper-content step="1">
                  <v-form v-model="validIdentificationInput">
                    <v-text-field
                      label="Enter a username"
                      v-model="userAccount.username"
                      :rules="$store.state.rules.usernameRules"
                      required
                      :disabled=disable
                    ></v-text-field>
                    <v-text-field
                      label="Enter a display name"
                      v-model="userProfile.displayName"
                      :rules="$store.state.rules.displayNameRules"
                      required
                      :disabled=disable
                    ></v-text-field>
                    <v-text-field
                        label="Enter a password"
                        v-model="userAccount.password"
                        :rules="$store.state.rules.passwordRules"
                        :min="8"
                        :counter="64"
                        :append-icon="visible ? 'visibility' : 'visibility_off'"
                        :append-icon-cb="() => (visible = !visible)"
                        :type=" visible ? 'text' : 'password'"
                        :error-messages="passwordErrorMessages"
                        @input="validateDelayed"
                        required
                        :disabled=disable
                      ></v-text-field>
                  </v-form>
                <v-btn color="primary" @click="userStep = 2" :disabled="!isPasswordValid || !validIdentificationInput || disable">Next</v-btn>
              </v-stepper-content>
              <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="2" :complete="userStep > 2">
                Security Questions
              </v-stepper-step>
                <v-stepper-content step="2">
                  <v-form v-model="validSecurityInput">
                    <div v-for="set in $store.state.constants.securityQuestions" :key="set.id">
                      <v-select
                        :items="set.questions"
                        item-text="question"
                        item-value="id"
                        v-model="securityQuestions[set.id].question"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        :rules="$store.state.rules.securityQuestionRules"
                        required
                        :disabled=disable
                      ></v-select>
                      <v-text-field
                        label="Enter an answer to the above security question"
                        v-model="securityQuestions[set.id].answer"
                        :rules="$store.state.rules.securityAnswerRules"
                        required
                        :disabled=disable
                      ></v-text-field>
                    </div>
                  </v-form>
                  <v-btn color="grey lighten-5" @click="userStep = 1">Previous</v-btn>
                  <v-btn color="primary" @click="userSubmit" :disabled="!validSecurityInput" :loading="loading">Submit</v-btn>
                </v-stepper-content>
            </v-stepper>
          </div>
          <div v-if="content === 'restaurant'">
            <v-stepper v-model="restaurantStep" :vertical="windowSize.width <= mobileScreenWidth">
              <v-stepper-header v-if="windowSize.width > mobileScreenWidth">
                <v-divider></v-divider>
                <v-stepper-step step="1" :complete="restaurantStep > 1">Identification</v-stepper-step>
                <v-divider></v-divider>
                <v-stepper-step step="2" :complete="restaurantStep > 2">Security Questions</v-stepper-step>
                <v-divider></v-divider>
                <v-stepper-step step="3" :complete="restaurantStep > 3">Restaurant Details</v-stepper-step>
                <v-divider></v-divider>
                <v-stepper-step step="4" :complete="restaurantStep > 4">Business Hours</v-stepper-step>
                <v-divider></v-divider>
                <v-stepper-step step="5" :complete="restaurantStep > 5">Contact Information</v-stepper-step>
                <v-divider></v-divider>
              </v-stepper-header>
              <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="1" :complete="restaurantStep > 1">
                Identification
              </v-stepper-step>
              <v-stepper-content step="1">
                  <v-form v-model="validIdentificationInput">
                    <v-text-field
                      label="Enter a username"
                      v-model="userAccount.username"
                      :rules="$store.state.rules.usernameRules"
                      required
                      :disabled=disable
                    ></v-text-field>
                    <v-text-field
                      label="Enter a display name"
                      v-model="userProfile.displayName"
                      :rules="$store.state.rules.displayNameRules"
                      required
                      :disabled=disable
                    ></v-text-field>
                    <v-text-field
                        label="Enter a password"
                        v-model="userAccount.password"
                        :rules="$store.state.rules.passwordRules"
                        :min="8"
                        :counter="64"
                        :append-icon="visible ? 'visibility' : 'visibility_off'"
                        :append-icon-cb="() => (visible = !visible)"
                        :type=" visible ? 'text' : 'password'"
                        :error-messages="passwordErrorMessages"
                        @input="validateDelayed"
                        required
                        :disabled=disable
                      ></v-text-field>
                  </v-form>
                <v-btn color="primary" @click="restaurantStep = 2" :disabled="!isPasswordValid || !validIdentificationInput || disable">Next</v-btn>
              </v-stepper-content>
              <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="2" :complete="restaurantStep > 2">
                Security Questions
              </v-stepper-step>
              <v-stepper-content step="2">
                <v-form v-model="validSecurityInput">
                  <div v-for="set in $store.state.constants.securityQuestions" :key="set.id">
                    <v-select
                      :items="set.questions"
                      item-text="question"
                      item-value="id"
                      v-model="securityQuestions[set.id].question"
                      label="Select a security question"
                      single-line
                      auto
                      append-icon="https"
                      hide-details
                      :rules="$store.state.rules.securityQuestionRules"
                      required
                      :disabled=disable
                    ></v-select>
                    <v-text-field
                      label="Enter an answer to the above security question"
                      v-model="securityQuestions[set.id].answer"
                      :rules="$store.state.rules.securityAnswerRules"
                      required
                      :disabled=disable
                    ></v-text-field>
                  </div>
                </v-form>
                <v-btn color="grey lighten-5" @click="restaurantStep = 1" :disabled=disable>Previous</v-btn>
                <v-btn color="primary" :disabled="!validSecurityInput || disable" @click="restaurantStep = 3">Next</v-btn>
              </v-stepper-content>
              <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="3" :complete="restaurantStep > 3">
                Restaurant Details
              </v-stepper-step>
              <v-stepper-content step="3">
                <v-form v-model="validRestaurantDetailsInput">
                  <v-flex xs12>
                    <v-select
                      :items="$store.state.constants.foodTypes"
                      item-text="type"
                      item-value="type"
                      v-model="restaurantProfile.details.foodType"
                      label="Select a food type associated with your restaurant"
                      single-line
                      auto
                      prepend-icon="restaurant"
                      hide-details
                      :rules="$store.state.rules.foodTypeRules"
                      required
                      :disabled=disable
                    ></v-select>
                  </v-flex>
                  <v-flex xs12>
                    <v-select
                      :items="$store.state.constants.avgFoodPrices"
                      item-text="price"
                      item-value="id"
                      v-model="restaurantProfile.details.avgFoodPrice"
                      label="Select average food price"
                      single-line
                      auto
                      prepend-icon="money"
                      hide-details
                      :rules="$store.state.rules.avgFoodPriceRules"
                      required
                      :disabled=disable
                    ></v-select>
                  </v-flex>
                  <v-flex xs12>
                    <v-select
                      label="Select a food preference"
                      :items="$store.state.constants.foodPreferences"
                      v-model="foodPreferences"
                      multiple
                      chips
                      prepend-icon="done"
                      persistent-hint
                      :rules="$store.state.rules.foodPreferenceRules"
                      required
                      :disabled=disable
                    ></v-select>
                  </v-flex>
                </v-form>
                <v-btn color="grey lighten-5" @click="restaurantStep = 2" :disabled=disable>Previous</v-btn>
                <v-btn color="primary" :disabled="!validRestaurantDetailsInput || disable" @click="restaurantStep = 4">Next</v-btn>
              </v-stepper-content>
              <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="4" :complete="restaurantStep > 4">
                Business Hours
              </v-stepper-step>
              <v-stepper-content step="4">
                <v-form v-model="validAddBusinessHour">
                  <v-select
                    :items="$store.state.constants.timeZones"
                    item-text="displayString"
                    item-value="timeZoneName"
                    v-model="timeZone"
                    label="Select your time zone"
                    single-line
                    auto
                    hide-details
                    :rules="$store.state.rules.timeZoneRules"
                    required
                    :disabled=disable
                  ></v-select>
                  <v-select
                    :items="$store.state.constants.dayOfWeek"
                    v-model="businessHour.day"
                    label="Select a day"
                    single-line
                    auto
                    hide-details
                    :rules="$store.state.rules.businessDayRules"
                    required
                    :disabled=disable
                  ></v-select>
                  <v-menu
                    ref="menu"
                    lazy
                    :close-on-content-click="false"
                    v-model="openTimeSync"
                    transition="scale-transition"
                    offset-y
                    full-width
                    :nudge-right="40"
                    max-width="290px"
                    min-width="290px"
                    :return-value.sync="time"
                  >
                    <v-text-field
                      slot="activator"
                      label="Select opening time (24hr format)"
                      v-model="businessHour.openTime"
                      :rules="$store.state.rules.businessHourRules"
                      prepend-icon="access_time"
                      readonly
                      required
                      :disabled=disable
                    ></v-text-field>
                    <v-time-picker
                      format="24hr"
                      v-model="businessHour.openTime"
                      :max="businessHour.closeTime"
                    >
                    </v-time-picker>
                  </v-menu>
                  <v-menu
                    ref="menu"
                    lazy
                    :close-on-content-click="false"
                    v-model="closeTimeSync"
                    transition="scale-transition"
                    offset-y
                    full-width
                    :nudge-right="40"
                    max-width="290px"
                    min-width="290px"
                    :return-value.sync="time"
                  >
                    <v-text-field
                      slot="activator"
                      label="Select closing time (24hr format)"
                      v-model="businessHour.closeTime"
                      :rules="$store.state.rules.businessHourRules"
                      prepend-icon="access_time"
                      readonly
                      required
                      :disabled=disable
                    ></v-text-field>
                    <v-time-picker
                      format="24hr"
                      scrollable
                      v-model="businessHour.closeTime"
                      :min="businessHour.openTime"
                    >
                    </v-time-picker>
                  </v-menu>
                </v-form>
                <v-btn @click.prevent="addBusinessHour" id="add-btn" color="black" :disabled="!validAddBusinessHour || disable">Add</v-btn>
                <div id="business-hours-list">
                  <v-toolbar color="teal" dark v-if="businessHours.length > 0">
                    <v-spacer/>
                    <v-toolbar-title>Business Hours</v-toolbar-title>
                    <v-spacer/>
                </v-toolbar>
                  <v-list two-line>
                  <template v-for="(businessHour, index) in businessHours">
                    <v-list-tile
                      avatar
                      ripple
                      @click="toggle()"
                      :key="index"
                    >
                      <v-list-tile-content>
                        <!-- Business hour day of week -->
                        <v-list-tile-title>{{ businessHour.day }}</v-list-tile-title>
                        <v-list-tile-sub-title class="text--primary">
                          {{ businessHour.openTime }} - {{ businessHour.closeTime }}
                        </v-list-tile-sub-title>
                      </v-list-tile-content>
                      <v-list-tile-action>
                        <!-- Delete a particular business hour -->
                        <v-btn icon class="mx-0" @click="deleteBusinessHour(businessHour)">
                          <v-icon color="pink">delete</v-icon>
                        </v-btn>
                      </v-list-tile-action>
                    </v-list-tile>
                    <v-divider
                      v-if="index !== businessHours.length"
                      :key="businessHour.id"
                    >
                    </v-divider>
                  </template>
                </v-list>
                </div>
                <v-btn color="grey lighten-5" @click="restaurantStep = 3" :disabled=disable>Previous</v-btn>
                <v-btn color="primary" :disabled="!validBusinessHourInput || disable" @click="restaurantStep = 5">Next</v-btn>
              </v-stepper-content>
              <v-stepper-step v-if="windowSize.width <= mobileScreenWidth" step="5" :complete="restaurantStep > 5">
                Contact Information
              </v-stepper-step>
              <v-stepper-content step="5">
                <v-form v-model="validContactInput">
                  <v-flex xs12>
                    <v-subheader>Enter the address of your restaurant</v-subheader>
                  </v-flex>
                  <v-layout id="contact-layout" row justify-space-between>
                    <v-flex xs12>
                      <v-text-field
                        label="Street 1"
                        placeholder="1111 Snowy Rock Pl"
                        v-model="restaurantProfile.address.street1"
                        :rules="$store.state.rules.addressStreet1Rules"
                        required
                        :disabled=disable
                      ></v-text-field>
                      <v-text-field
                        label="Street 2"
                        placeholder="Unit 2"
                        v-model="restaurantProfile.address.street2"
                        :disabled=disable
                      ></v-text-field>
                      <v-text-field
                        label="City"
                        placeholder="Long Beach"
                        v-model="restaurantProfile.address.city"
                        :rules="$store.state.constants.addressCityRules"
                        required
                        :disabled=disable
                      ></v-text-field>
                      <v-select
                        :items="$store.state.constants.states"
                        v-model="restaurantProfile.address.state"
                        item-text="name"
                        item-value="abbreviation"
                        label="Select a state"
                        :rules="$store.state.rules.addressStateRules"
                        single-line
                        auto
                        append-icon="map"
                        hide-details
                        required
                        :disabled=disable
                      ></v-select>
                        <v-text-field
                        label="Zip"
                        placeholder="92812"
                        :rules="$store.state.rules.addressZipRules"
                        type="number"
                        v-model.number="restaurantProfile.address.zip"
                        required
                        :disabled=disable
                      ></v-text-field>
                    </v-flex>
                  </v-layout>
                  <v-flex xs12>
                    <v-subheader>Enter a phone number</v-subheader>
                  </v-flex>
                  <v-layout id="phone-number">
                  <v-flex xs12 sm2>
                    <v-text-field
                      v-model="restaurantProfile.phoneNumber"
                      placeholder="(562)111-5555"
                      prepend-icon="phone"
                      :rules="$store.state.rules.phoneNumberRules"
                      single-line
                      :disabled=disable
                    ></v-text-field>
                  </v-flex>
                  </v-layout>
                </v-form>
                <v-btn color="grey lighten-5" @click="restaurantStep = 4" :disabled=disable>Previous</v-btn>
                <v-btn color="primary" @click="restaurantSubmit" :disabled="!validContactInput || disable" :loading="loading">Submit</v-btn>
              </v-stepper-content>
            </v-stepper>
          </div>
        </v-tab-item>
      </v-tabs-items>
    </v-container>
    </v-layout>
    <div id="success">
      <!-- Success messages for registration -->
      <v-alert type="success" :value="showSuccess">
        <span>
          Success! User <code>{{ username }}</code> has been created.
          <router-link to="/Login">
            <v-btn small id='login-redirect-btn' dark color="blue darken-2">
              <span class="btn-text" id="login-redirect-text">Login</span></v-btn>
          </router-link>
        </span>
      </v-alert>
    </div>
    <div v-show="showError" id="error-div">
      <!-- Error messages for registration -->
      <v-alert id="registration-error" :value=true icon='warning'>
        <span id="error-title">
          An error has occurred.
        </span>
      </v-alert>
      <!-- Card to show error messages -->
      <v-card id="error-card">
        {{ this.errors }}
      </v-card>
    </div>
  </v-content>
</template>

<script>
import axios from 'axios'
import PasswordValidation from '@/components/PasswordValidation/PasswordValidation'
export default {
  name: 'CreateUser',
  components: {},
  data: () => ({
    tabs: null,
    userStep: 0,
    restaurantStep: 0,
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
    showError: false,
    showSuccess: false,
    errors: [],
    counter: 0,
    timeZone: '',
    time: '',
    username: '',
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
    validationTimer: null,
    windowSize: {
      width: 0,
      height: 0
    },
    mobileScreenWidth: 600
  }),
  watch: {
    // Loading animation on buttons
    loader () {
      const l = this.loader
      this[l] = !this[l]
      setTimeout(() => (this[l] = false), 1000)
      this.loader = null
    }
  },
  mounted () {
    this.mobileScreenWidth = this.$store.state.constants.mobileScreenWidth
    this.onResize()
  },
  methods: {
    onResize () {
      this.windowSize = { width: window.innerWidth, height: window.innerHeight }
    },
    // Delays the validation of the password until user stops typing
    validateDelayed () {
      clearTimeout(this.validationTimer)
      this.validationTimer = setTimeout(() => { this.validatePassword() }, this.$store.state.constants.inputValidationDelay)
    },
    // Calls PasswordValidation to check the password.
    validatePassword () {
      if (this.userAccount.password.length < 8) {
        this.passwordErrorMessages = []
        return
      }

      var context = this

      PasswordValidation.methods.validatePassword(context, this.userAccount.password)
        .then(response => {
          this.isPasswordValid = response.isValid
          this.passwordErrorMessages = response.message
          if (response === []) {
            this.isPasswordValid = true
          }
        })
    },
    addBusinessHour () {
      this.businessHours.push({day: this.businessHour.day, openTime: this.businessHour.openTime, closeTime: this.businessHour.closeTime})
      this.businessHour.day = ''
      this.businessHour.openTime = null
      this.businessHour.closeTime = null
      this.counter = this.counter + 1
      if (this.counter > 0) {
        this.validBusinessHourInput = true
      }
    },
    deleteBusinessHour (businessHour) {
      const index = this.businessHours.indexOf(businessHour)
      this.businessHours.splice(index, 1)
      this.counter = this.counter - 1
      if (this.counter <= 0) {
        this.validBusinessHourInput = false
      }
    },
    userSubmit () {
      this.showError = false
      this.validSecurityInput = false
      this.disable = true
      this.loader = 'loading'
      axios.post(this.$store.state.urls.userManagement.createIndividualUser, {
        userAccountDto: this.userAccount,
        securityQuestionDtos: this.securityQuestions,
        userProfileDto: this.userProfile
      }).then(response => {
        this.validSecurityInput = true
        this.disable = false
        this.username = response.data
        this.showSuccess = true
      }).catch(error => {
        this.showSuccess = false
        this.showError = true
        this.validSecurityInput = true
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
          Promise.reject(this.errors)
        } catch (ex) {
          this.errors = ex
          Promise.reject(this.errors)
        }
      })
    },
    restaurantSubmit () {
      this.showError = false
      this.validContactInput = false
      this.disable = true
      this.loader = 'loading'
      axios.post(this.$store.state.urls.userManagement.createRestaurantUser, {
        userAccountDto: this.userAccount,
        securityQuestionDtos: this.securityQuestions,
        userProfileDto: this.userProfile,
        restaurantProfileDto: this.restaurantProfile,
        foodPreferences: this.foodPreferences,
        timeZone: this.timeZone,
        businessHourDtos: this.businessHours
      }).then(response => {
        this.validContactInput = true
        this.disable = false
        this.username = response.data
        this.showSuccess = true
      }).catch(error => {
        this.showSuccess = false
        this.showError = true
        this.validContactInput = true
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
    toggle () {}
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
#add-btn {
  color: white;
}
#main-content {
  max-width: 1500px;
}
#login-redirect {
  font-weight: bold;
  font-size: small;
}
#login-redirect-text {
  font-weight: bold;
}
</style>
