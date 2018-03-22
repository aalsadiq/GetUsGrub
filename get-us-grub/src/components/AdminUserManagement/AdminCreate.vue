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
              <app-user-validations/>
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
import UserValidations from '@/components/AdminUserManagement/UserValidations'
import AppFooter from '@/components/AppFooter'
// import axios from 'axios'
export default {
  name: 'CreateUser',
  components: {
    'app-create-user': AppCreateUser,
    'app-admin-header': AdminHeader,
    'app-footer': AppFooter,
    'app-user-validations': UserValidations
  },
  data: () => ({
    check: false
  }),
  props: {
    userAccount: {
      sendUsername: this.username,
      sendDisplayname: this.displayName,
      sendPassword: this.passowrd
    }
  },
  methods: {
    normalUser: function () {
      this.check = false
    },
    adminUser: function () {
      this.check = true
    },
    testingUserSubmit: function () {
      // console.log(this.$emit('grabUserAccount'),userName)
    }
    // userSubmit () {
    //   axios.post('http://localhost:8081/User/Admin/Create', {
    //   }).then(response => {
    //     this.responseDataStatus = 'Success! User has been created: '
    //     this.responseData = response.data
    //   }).catch(error => {
    //     this.responseDataStatus = 'An error has occurred: '
    //     this.responseData = error.response.data
    //   })
    // }
  }
}
</script>
