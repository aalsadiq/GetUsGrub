<template>
    <div id="app">
      {{ responseDataStatus }} {{ responseData }}
      <v-flex xs6 sm3 offset-sm5>
        <v-form v-model="validIdentificationInput">
          <v-text-field label="Enter user to edit" v-model="editUser.username" :rules="$store.state.rules.usernameRules"></v-text-field>
          <v-text-field label="Enter new username" v-model="editUser.newUsername" :rules="$store.state.rules.usernameNotRequiredRule"></v-text-field>
          <v-text-field label="Enter new display name" v-model="editUser.newDisplayName"></v-text-field>
        </v-form>
        <v-btn id ="submit-button" color="info" v-on:click="userSubmit()">Submit</v-btn>
      </v-flex>
    </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'
export default {
  name: 'UserValidation',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter
  },
  data: () => ({
    validIdentificationInput: false,
    validSecurityInput: false,
    responseData: '',
    responseDataStatus: '',
    editUser: {
      username: '',
      newUsername: null,
      newDisplayName: null
    }
  }),
  methods: {
  userSubmit () {
      axios.put(this.$store.state.urls.userManagement.editUser, {
        username: this.editUser.username,
        newUsername: this.editUser.newUsername,
        newDisplayName: this.editUser.newDisplayName
      }).then(response => {
        this.responseDataStatus = 'Success! User has been edited: '
        this.responseData = response.data
        this.newUsername = null
        this.newDisplayname = null
        console.log(response)
      }).catch(error => {
        this.responseDataStatus = 'An error has occured.'
        this.responseData = error.response.data
        this.newUsername = null
        this.newDisplayName = null
        console.log(error.response.data)
      })
    }
  }
}
</script>
