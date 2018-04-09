<template>
    <div id="app">
      {{ responseDataStatus }} {{ responseData }}
      <v-flex xs6 sm3 offset-sm5>
        <v-form v-model="validIdentificationInput">
          <v-text-field label="Enter new username" v-model="userAccount.username" :rules="$store.state.rules.usernameRules" required></v-text-field>
          <v-text-field label="Enter new display name" v-model="userProfile.displayname" :rules="$store.state.rules.displayName" required></v-text-field>
          <v-text-field label="Enter new password" v-model="userAccount.password" :rules="$store.state.rules.password" :min="8" :counter="64" :append-icon="visibile ? 'visibility' : 'visibility_off'" :append-icon-cb="() => (visibile = !visibile)" :type=" visibile ? 'text' : 'password'" required></v-text-field>
        </v-form>
        <v-form v-model="validSecurityInput">
          <v-layout row wrap>
            <v-flex xs12>
              <v-select :items="$store.state.constants.securityQuestionsSet1" item-text="question" item-value="id" v-model="securityQuestions[0].question" label="Select a security question" single-line auto append-icon="https" hide-details required></v-select>
              <v-text-field label="Enter an answer to the above security question" v-model="securityQuestions[0].answer" :rules="$store.state.rules.securityAnswerRules" required></v-text-field>
              <v-select :items="$store.state.constants.securityQuestionsSet2" item-text="question" item-value="id" v-model="securityQuestions[1].question" label="Select a security question" single-line auto append-icon="https" hide-details required></v-select>
              <v-text-field label="Enter an answer to the above security question" v-model="securityQuestions[1].answer" :rules="$store.state.rules.securityAnswerRules" required></v-text-field>
              <v-select :items="$store.state.constants.securityQuestionsSet3" item-text="question" item-value="id" v-model="securityQuestions[2].question" label="Select a security question" single-line auto append-icon="https" hide-details required></v-select>
              <v-text-field label="Enter an answer to the above security question" v-model="securityQuestions[2].answer" :rules="$store.state.rules.securityAnswerRules" required></v-text-field>
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
    userSubmit (viewType) {
      if (viewType === 'CreateAdmin') {
        axios.post('http://localhost:8081/User/CreateAdmin', {
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
        })
      }
    }
  }
}
</script>
