<template>
    <div id="app">
    <div id="success">
      <v-layout>
        <v-flex xs12>
          <v-alert type="success" :value="showSuccess">
            <span>
              {{ responseData }}
            </span>
          </v-alert>
        </v-flex>
      </v-layout>
    </div>
    <div v-show="showError" id="error-div">
      <v-layout>
      <v-flex xs12>
        <v-alert id="registration-error" :value=true icon='warning'>
          <span id="error-title">
            An error has occurred
          </span>
        </v-alert>
      </v-flex>
      </v-layout>
      <v-layout>
        <v-flex xs12>
          <v-card id="error-card">
            <p v-for="error in errors" :key="error">
              {{ error }}
            </p>
          </v-card>
        </v-flex>
      </v-layout>
    </div>
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
    showError: false,
    showSuccess: false,
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
      },
      {
        headers: { Authorization: `Bearer ${this.$store.state.authenticationToken}` }
      }
      ).then(response => {
        this.responseData = response.data
        this.newUsername = null
        this.newDisplayname = null
        this.showSuccess = true
        this.showError = false
      }).catch(error => {
        this.newUsername = null
        this.newDisplayName = null
        this.showSuccess = false
        this.showError = true
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
    }
  }
}
</script>
