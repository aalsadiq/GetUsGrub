<template>
    <div id="app">
      {{ responseDataStatus }} {{ responseData }}
      <v-flex xs6 sm3 offset-sm5>
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
            label="Enter new password"
            v-model="userAccount.password"
            :rules="$store.state.rules.passwordRules"
            :min="8"
            :counter="64"
            :append-icon="visible ? 'visibility' : 'visibility_off'"
            :append-icon-cb="() => (visible = !visible)"
            :type=" visible ? 'text' : 'password'"
            :error-messages="passwordErrorMessages"
            @input="validatePassword"
            required
            :disabled=disable
          ></v-text-field>
        </v-form>
        <v-form v-model="validSecurityInput">
          <v-layout row wrap>
            <v-flex xs12>
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
              </v-flex>
           </v-layout>
        </v-form>
           <v-btn id ="submit-button" color="info" v-on:click="userSubmit(viewType)">Submit</v-btn>
      </v-flex>
    </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'
import PasswordValidation from '@/components/PasswordValidation/PasswordValidation'
export default {
  name: 'UserValidation',
  props: ['viewType'],
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter
  },
  data: () => ({
    check: false,
    validIdentificationInput: false,
    validSecurityInput: false,
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
        category: '',
        avgFoodPrice: null
      },
      foodPreferences: [
      ]
    },
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
    validatePassword () {
      if (this.userAccount.password.length < 8) {
        this.passwordErrorMessages = []
        return
      }
      PasswordValidation.methods.validate(this.userAccount.password)
        .then(response => {
          this.isPasswordValid = response.isValid
          this.passwordErrorMessages = response.message
          if (response === []) {
            this.isPasswordValid = true
          }
        })
    },
    userSubmit (viewType) {
      if (viewType === 'CreateAdmin') {
        axios.post(this.$store.state.urls.userManagement.createAdminUser, {
          userAccountDto: this.userAccount,
          securityQuestionDtos: this.securityQuestions,
          userProfileDto: this.userProfile
        }).then(response => {
          this.responseDataStatus = 'Success! User has been created: '
          this.responseData = response.data
          console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.data
          console.log(error.response.data)
          try {
            if (error.response.status === 401) {
              // Route to Unauthorized page
              this.$router.push({path: '/Unauthorized'})
            }
            if (error.response.status === 403) {
              // Route to Forbidden page
              this.$router.push({path: '/Forbidden'})
            }
            if (error.response.status === 404) {
              // Route to ResourceNotFound page
              this.$router.push({path: '/ResourceNotFound'})
            }
            if (error.response.status === 500) {
              // Route to InternalServerError page
              this.$router.push({path: '/InternalServerError'})
            } else {
              this.errors = JSON.parse(JSON.parse(error.response.data.message))
            }
            Promise.reject(error)
          } catch (ex) {
            this.errors = error.response.data
            Promise.reject(error)
          }
        })
      }
    }
  }
}
</script>
