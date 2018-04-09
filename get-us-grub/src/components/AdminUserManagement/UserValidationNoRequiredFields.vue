<template>
    <div id="app">
      {{ responseDataStatus }} {{ responseData }}
      <v-flex xs6 sm3 offset-sm5>
        <v-form v-model="validIdentificationInput">
          <v-text-field label="Enter user to edit" v-model="username" :rules="$store.state.rules.usernameRules"></v-text-field>
          <v-text-field label="Enter new username" v-model="newUsername" :rules="$store.state.rules.usernameNotRequiredRule"></v-text-field>
          <v-text-field label="Enter new display name" v-model="newDisplayName"></v-text-field>
          <!-- @Andrews Password reset vue -->
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
    username: '',
    newUsername: '',
    newDisplayName: ''
  }),
  methods: {
    userSubmit (viewType) {
      if (viewType === 'EditUser') {
        axios.put('http://localhost:8081/User/EditUser', {
          username: this.username,
          newUsername: this.newUsername,
          newDisplayName: this.newDisplayName
        }).then(response => {
          this.responseDataStatus = 'Success! User has been edited: '
          this.responseData = response.data
          console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has edited: '
          this.responseData = error.response.data
          console.log(error.response.data)
        })
      }
    }
  }
}
</script>
