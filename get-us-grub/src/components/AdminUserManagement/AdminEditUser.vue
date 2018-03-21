<template>
  <div>
    <app-admin-header/>
    <!-- <v-container id="choose-your-user" fluid>
    </v-container> -->
    <v-layout>
      <!-- <v-flex xs5 sm3 offset-sm3> -->
      <v-flex xs15>
        <h1>Choose your user</h1>
        <v-btn v-on:click="viewUserRequirements" id ="user-button" color="info">User</v-btn>
        <v-btn v-on:click="viewRestaurantRequirements" id ="restaurant-button" color="info">Restaurant</v-btn>
        <v-btn v-on:click="viewAdminRequirements" id ="admin-button" color="info">Admin</v-btn>
      </v-flex>
    </v-layout>
      <v-flex sm2 offset-sm5>
        <h2>Input User To Edit</h2>
          <v-form v-model="validIdentificationInput">
            <v-text-field label="username" v-model="username" :rules="usernameRules" required ></v-text-field>
          </v-form>
      </v-flex>
      <v-container fluid grid-list-md>
      <v-layout row wrap>
        <v-flex d-flex md4 offset-xs4>
          <v-card-text>
            <h1> New User Information</h1>
            <app-user-validations/>
            <div v-if="check">
              <h1>Additional Restaurant requireinformation</h1>
              <app-restaurant-validations/>
            </div>
          </v-card-text>
        </v-flex>
        <v-flex d-flex xs12 sm6 md4>
          <!-- <v-layout row wrap>
            <v-flex d-flex>
              <v-card-text>
                <v-checkbox :label="'Username'" v-model="checkboxUsername"></v-checkbox>
                <v-checkbox :label="'Display Name'" v-model="checkboxDisplayName"></v-checkbox>
                <v-checkbox :label="'Password'" v-model="checkboxPassword"></v-checkbox>
                <div v-if="check">
                  <v-checkbox :label="'Street1'" v-model="checkBoxStreet1"></v-checkbox>
                  <v-checkbox :label="'Street2'" v-model="checkBoxStreet2"></v-checkbox>
                  <v-checkbox :label="'City'" v-model="checkboxCity"></v-checkbox>
                  <v-checkbox :label="'State'" v-model="checkBoxState"></v-checkbox>
                  <v-checkbox :label="'Phone Number'" v-model="checkboxPhoneNumber"></v-checkbox>
                  <v-checkbox :label="'Business Hours'" v-model="checkBoxBusinessHours"></v-checkbox>
                </div>
              </v-card-text>
             </v-flex>
          </v-layout> -->
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
import AppUserValidations from '@/components/AdminUserManagement/UserValidations'
import AppRestaurantValidations from '@/components/AdminUserManagement/RestaurantValidations'
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
    // checkboxUsername: false,
    // checkboxDisplayName: false,
    // checkboxPassword: false,
    // checkBoxStreet1: false,
    // checkBoxStreet2: false,
    // checkboxCity: false,
    // checkBoxState: false,
    // checkboxPhoneNumber: false,
    // checkBoxBusinessHours: false,
    check: false,
    validIdentificationInput: false,
    // userAccount: {
    //   username: '',
    //   displayname: '',
    //   password: ''
    // },
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
    userAndAdminSubmit () {
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
    // restaurantSubmit () {
    //   axios.put('http://localhost:8081/User/Admin/EditRestaurant', {
    //     username: this.username// test
    //   }).then(response => {
    //     this.responseDataStatus = 'Success! User has been created: '
    //     this.responseData = response.data
    //     console.log(response)
    //   }).catch(error => {
    //     this.responseDataStatus = 'An error has occurred: '
    //     this.responseData = error.response.data
    //     console.log(error.response.data)
    //   })
    // }
  }
}
</script>
