<template>
  <div>
    <app-admin-header/>
    <v-container id="choose-your-user" fluid>
    </v-container>
    <v-layout>
      <v-flex xs10 order-lg21>
        <h1>Choose your user</h1>
        <v-btn id ="user-button" color="info">User</v-btn>
        <v-btn v-on:click="method1" id ="restaurant-button" color="info">Restaurant</v-btn>
        <v-btn id ="admin-button" color="info">Admin</v-btn>
      </v-flex>
    </v-layout>
    <v-container id="edit-field" fluid>
      <v-flex id="Test1" xs6 sm4 offset-sm3>
        <h2>Input User To Edit</h2>
          <v-form v-model="validIdentificationInput">
            <v-text-field label="username" v-model="username" :rules="usernameRules" required ></v-text-field>
          </v-form>
      </v-flex>
      <!--Add vifs here for the different user types-->
    </v-container>
    <v-flex xs5 sm5 offset-sm3>
      <h1> New User Information</h1>
       <app-user-validations/>
       <!-- <div v-if="check">  -->
         <app-restaurant-validations/>
       <!-- </div> -->
    </v-flex>
    <app-footer/>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import AppUserTextBox from '@/components/AdminUserManagement/UserTextBox'
import AppUserValidations from '@/components/AdminUserManagement/UserValidations'
import AppRestaurantValidations from '@/components/AdminUserManagement/RestaurantValidations'

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
    username: '',
    usernameRules: [
      v => !!v || 'Username is required',
      v => /^[A-Za-z\d]+$/.test(v) || 'Username must contain only letters and numbers'
    ]
  }),
  methods: {
    method1: function () {
      this.check = !this.check
    }
  }
}
</script>

<style scoped>
#edit-field{
  max-height:100px;
}
#Test1{
  max-height:100px;
}
</style>
