<template>
    <div id="app">
      <v-flex xs6 sm3 offset-sm5>
        <v-form v-model="validIdentificationInput">
          <v-text-field label="Enter new username" v-model="userAccount.username" :rules="usernameRules" required></v-text-field>
          <v-text-field label="Enter new display name" v-model="userProfile.displayname" :rules="displayNameRules" required></v-text-field>
          <v-text-field label="Enter new password" v-model="userAccount.password" :rules="passwordRules" :min="8" :counter="64" :append-icon="visibile ? 'visibility' : 'visibility_off'" :append-icon-cb="() => (visibile = !visibile)" :type=" visibile ? 'text' : 'password'" required></v-text-field>
        </v-form>
        <v-form v-model="validSecurityInput">
          <v-layout row wrap>
            <v-flex xs12>
              <v-select :items="securityQuestionsSet1" item-text="question" item-value="id" v-model="securityQuestions[0].question" label="Select a security question" single-line auto append-icon="https" hide-details required></v-select>
              <v-text-field label="Enter an answer to the above security question" v-model="securityQuestions[0].answer" :rules="securityAnswerRules" required></v-text-field>
              <v-select :items="securityQuestionsSet2" item-text="question" item-value="id" v-model="securityQuestions[1].question" label="Select a security question" single-line auto append-icon="https" hide-details required></v-select>
              <v-text-field label="Enter an answer to the above security question" v-model="securityQuestions[1].answer" :rules="securityAnswerRules" required></v-text-field>
              <v-select :items="securityQuestionsSet3" item-text="question" item-value="id" v-model="securityQuestions[2].question" label="Select a security question" single-line auto append-icon="https" hide-details required></v-select>
              <v-text-field label="Enter an answer to the above security question" v-model="securityQuestions[2].answer" :rules="securityAnswerRules" required></v-text-field>
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
    responseDataStatus: '',
    responseData: '',
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
    }]
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
