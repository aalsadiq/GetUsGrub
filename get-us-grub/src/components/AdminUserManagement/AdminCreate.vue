<template>
  <div>
    <app-admin-header/>
    <v-flex xs15order-lg2>
      <h1>Select your user!</h1>
      <v-btn v-on:click="normalUser" id ="NormalUser-button" color="info">Regular Registration</v-btn>
      <v-btn v-on:click="adminUser" id ="AdminUserr-button" color="info">Admin Registration</v-btn>
          <div v-if="check===false">
            <v-flex xs3 sm10 offset-sm2>
              <app-create-user/>
            </v-flex>
            </div>
          <div v-else>
            <v-flex xs6 sm3 offset-sm5>
              <!-- <app-user-validations/> -->
              <v-form v-model="validIdentificationInput">
              <v-text-field label="Enter new username" v-model="username" :rules="usernameRules" required></v-text-field>
              <v-text-field label="Enter new display name" v-model="displayName" :rules="displayNameRules" required></v-text-field>
              <v-text-field label="Enter new password" v-model="password" :rules="passwordRules" :min="8" :counter="64" :append-icon="visibile ? 'visibility' : 'visibility_off'" :append-icon-cb="() => (visibile = !visibile)" :type=" visibile ? 'text' : 'password'" required></v-text-field>
                </v-form>
               <v-form v-model="validSecurityInput">
                  <v-layout row wrap>
                    <v-flex xs12>
                      <v-select
                        :items="securityQuestionsSet1"
                        item-text="question"
                        item-value="id"
                        v-model="securityQuestions[0].question"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        required
                      ></v-select>
                      <v-text-field
                        label="Enter an answer to the above security question"
                        v-model="securityQuestions[0].answer"
                        :rules="securityAnswerRules"
                        required
                      ></v-text-field>
                      <v-select
                        :items="securityQuestionsSet2"
                        item-text="question"
                        item-value="id"
                        v-model="securityQuestions[1].question"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        required
                      ></v-select>
                      <v-text-field
                      label="Enter an answer to the above security question"
                      v-model="securityQuestions[1].answer"
                      :rules="securityAnswerRules"
                      required
                    ></v-text-field>
                    <v-select
                        :items="securityQuestionsSet3"
                        item-text="question"
                        item-value="id"
                        v-model="securityQuestions[2].question"
                        label="Select a security question"
                        single-line
                        auto
                        append-icon="https"
                        hide-details
                        required
                      ></v-select>
                      <v-text-field
                      label="Enter an answer to the above security question"
                      v-model="securityQuestions[2].answer"
                      :rules="securityAnswerRules"
                      required
                    ></v-text-field>
                    </v-flex>
                  </v-layout>
                  </v-form>
              <v-btn v-on:click="userSubmit" id ="submit-admin" color="info" >Submit</v-btn>
            </v-flex>
          </div>
    </v-flex>
    <app-footer/>
  </div>
</template>

<script>
import AppCreateUser from '@/components/CreateUser'
import AdminHeader from '@/components/AdminUserManagement/AdminHeader'
// import UserValidations from '@/components/AdminUserManagement/UserValidations'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'
export default {
  name: 'CreateUser',
  components: {
    'app-create-user': AppCreateUser,
    'app-admin-header': AdminHeader,
    'app-footer': AppFooter
    // 'app-user-validations': UserValidations
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
  // props: {
  //   userAccount: {
  //     sendUsername: this.username,
  //     sendDisplayname: this.displayName,
  //     sendPassword: this.passowrd
  //   }
  // },
  methods: {
    normalUser: function () {
      this.check = false
    },
    adminUser: function () {
      this.check = true
    },
    testingUserSubmit: function () {
      // console.log(this.$emit('grabUserAccount'),userName)
    },
    userSubmit () {
      axios.post('http://localhost:8081/User/CreateUser', {
        userAccountDto: this.userAccount,
        securityQuestionDtos: this.securityQuestions,
        userProfileDto: this.userProfile
      }).then(response => {
        this.responseDataStatus = 'Success! User has been created: '
        this.responseData = response.data
      }).catch(error => {
        this.responseDataStatus = 'An error has occurred: '
        this.responseData = error.response.data
      })
    }
  }
}
</script>
