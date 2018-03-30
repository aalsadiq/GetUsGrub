<template>
    <div id="app">
      <v-flex xs6 sm3 offset-sm5>
        <v-form v-model="validIdentificationInput">
          <v-text-field label="Enter user to edit" v-model="userAccount.username" :rules="usernameRules"></v-text-field>
          <v-text-field label="Enter new username" v-model="userAccount.newUsername" :rules="newUsernameRules"></v-text-field>
          <v-text-field label="Enter new display name" v-model="userProfile.newDisplayName"></v-text-field>
          <!-- @Ahmed Password reset vue -->
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
    responseData: '',
    responseDataStatus: '',
    userAccount: {
      username: '',
      newUsername: ''
    },
    userProfile: {
      newDisplayName: ''
    },
    newUsernameRules: [
      newUsername => /^[A-Za-z\d]+$/.test(newUsername) || 'Username must contain only letters and numbers'
    ],
    usernameRules: [
      username => !!username || 'Username is required'
    ]
  }),
  methods: {
    userSubmit (viewType) {
      if (viewType === 'EditUser') {
        axios.put('http://localhost:8081/User/EditUser', {
          userAccountDto: this.userAccount,
          userProfileDto: this.userProfile
        }).then(response => {
          this.responseDataStatus = 'Success! User has been edited: '
          this.responseData = response.data
          console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has edited: '
          this.responseData = error.response.data
          console.log(this.responseData)
        })
      }
    }
  }
}
</script>
