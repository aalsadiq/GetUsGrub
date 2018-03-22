<template>
  <div>
    <app-admin-header/>
    <!-- <v-container id="choose-your-user" fluid>
    </v-container> -->
    <v-layout>
      <!-- <v-flex xs5 sm3 offset-sm3> -->
      <v-flex xs15>
        <h1>Choose your user</h1>
        <v-btn v-on:click="viewUserRequirements" id ="user-button" color="info">User Account</v-btn>
        <v-btn v-on:click="viewRestaurantRequirements" id ="restaurant-button" color="info">Restaurant Profile</v-btn>
        <v-btn v-on:click="viewAdminRequirements" id ="admin-button" color="info">User Profile</v-btn>
      </v-flex>
    </v-layout>
      <v-flex sm2 offset-sm5>
        <h2>Input User To Edit</h2>
          <v-form v-model="validIdentificationInput">
            <v-text-field label="username" v-model="username" :rules="usernameRules" required ></v-text-field>
            <v-btn v-on:click="imageuploadvue" id ="imageupload-button" color="warning">Image Upload</v-btn>
          </v-form>
      </v-flex>
      <v-container fluid grid-list-md>
      <v-layout row wrap>
        <v-flex sm2 offset-sm5>
          <v-card-text>
            <h1> New User Information</h1>
            <app-user-validations/>
            <div v-if="check">
              <h1>Additional Restaurant requireinformation</h1>
              <app-restaurant-validations/>
              <v-spacer/>
            </div>
            <v-btn id ="submit-account-button" color="warning" v-on:click="submitUserAccount">Submit UserAccount</v-btn>
            <v-btn id ="submit-restaurant-button" color="warning" v-on:click="submitRestaurantProfile">Submit RestaurantProfile</v-btn>
            <v-btn id ="submit-profile-button" color="warning" v-on:click="submitUserProfile">Submit UserProfile</v-btn>
          </v-card-text>
        </v-flex>
      </v-layout>
    </v-container>
    <app-footer/>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import AppUserTextBox from '@/components/AdminUserManagement/UserTextBox'
import AppUserValidations from '@/components/AdminUserManagement/UserValidationNoRequiredFields'
import AppRestaurantValidations from '@/components/AdminUserManagement/RestaurantValidationsNoRequiredFields'
import axios from 'axios'
export default {
  name: 'AdminHome',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter,
    'app-user-text-box': AppUserTextBox,
    'app-user-validations': AppUserValidations,
    'app-restaurant-validations': AppRestaurantValidations
  },
  data: () => ({
    check: false,
    validIdentificationInput: false,
    userAccount: {
      username: '',
      displayname: '',
      password: '',
      street1: '',
      street2: '',
      city: '',
      zip: '',
      phone: '',
      businesshours: ''
    },
    usernameRules: [
      username => !!username || 'Username is required',
      username => /^[A-Za-z\d]+$/.test(username) || 'Username must contain only letters and numbers'
    ]
  }),
  methods: {
    viewRestaurantRequirements: function () {
      this.check = true
    },
    viewUserRequirements: function () {
      this.check = false
    },
    viewAdminRequirements: function () {
      this.check = false
    },
    checkbox: function () {
      this.checkbox = !this.checkbox
    },
    submitUserAccount () {
      axios.put('http://localhost:8081/User/Admin/EditUser', {
        editUser: this.userAccount
      }).then(response => {
        this.responseDataStatus = 'Success! User has been created: '
        this.responseData = response.data
        console.log(response)
      }).catch(error => {
        this.responseDataStatus = 'An error has occurred: '
        this.responseData = error.response.data
        console.log(error.response.data)
      })
    }
  }
}
</script>
