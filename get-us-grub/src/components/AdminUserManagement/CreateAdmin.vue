<template>
    <div>
      <app-admin-header/>
      <v-flex xs3 sm10 offset-sm2>
        <v-text-field label="Enter a username" v-model="username" :rules="usernameRules" required></v-text-field>
        <v-text-field label="Enter a display name" v-model="displayName" :rules="displayNameRules" required></v-text-field>
        <v-text-field label="Enter a password" v-model="password" :rules="passwordRules" :min="8" :counter="64" :append-icon="visibile ? 'visibility' : 'visibility_off'" :append-icon-cb="() => (visibile = !visibile)" :type=" visibile ? 'text' : 'password'" required></v-text-field>
        <v-select :items="firstSecurityQuestionsSet" v-model="firstSecurityQuestion" label="Select a security question" single-line auto append-icon="https" hide-details required></v-select>
        <v-text-field label="Enter an answer to the above security question" v-model="firstSecurityAnswer" :rules="securityAnswerRules" required></v-text-field>
        <v-select :items="secondSecurityQuestionsSet" v-model="secondSecurityQuestion" label="Select a security question" single-line auto append-icon="https" hide-details required></v-select>
        <v-text-field label="Enter an answer to the above security question" v-model="secondSecurityAnswer" :rules="securityAnswerRules" required></v-text-field>
        <v-select :items="thirdSecurityQuestionsSet" v-model="thirdSecurityQuestion" label="Select a security question" single-line auto append-icon="https" hide-details required></v-select>
        <v-text-field label="Enter an answer to the above security question" v-model="thirdSecurityAnswer" :rules="securityAnswerRules" required></v-text-field>
    </v-flex>
    <app-footer/>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'
export default {
  name: 'CreateAdmin',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter
  },
  data: () => ({
    tabs: null,
    userStep: 0,
    restaurantStep: 0,
    validIdentificationInput: false,
    validSecurityInput: false,
    visibile: false,
    username: '',
    usernameRules: [
      v => !!v || 'Username is required',
      v => /^[A-Za-z\d]+$/.test(v) || 'Username must contain only letters and numbers'
    ],
    displayName: '',
    displayNameRules: [
      v => !!v || 'Display name is required'
      // Should display name have spaces and special characters?
    ],
    password: '',
    passwordRules: [
      v => !!v || 'Password is required',
      v => v.length > 8 || 'Password must be at least 8 characters',
      v => v.length < 64 || 'Password must be at most 64 characters'
    ],
    firstSecurityQuestion: null,
    firstSecurityQuestionsSet: [
      'Who was the company you first worked for?',
      'Where did you go to highschool or college?',
      'What was the name of the teacher who gave you your first failing grade?'
    ],
    secondSecurityQuestion: null,
    secondSecurityQuestionsSet: [
      'What is your favorite song?',
      'What is your mother\'s maiden name?',
      'What is your favorite sports team?'
    ],
    thirdSecurityQuestion: null,
    thirdSecurityQuestionsSet: [
      'What was the name of your first crush?',
      'What is the name of your hometown?',
      'What was the name of your first pet?'
    ],
    firstSecurityAnswer: '',
    secondSecurityAnswer: '',
    thirdSecurityAnswer: '',
    securityAnswerRules: [
      v => !!v || 'Security answer is required'
    ]
  }),
  methods: {
    EditUser () {
      axios.put('/User/EditUser')
        .then(response => {
          console.log('EditUser!!!!')
        })
    }
  }
}
</script>
