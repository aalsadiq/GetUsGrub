<template>
  <div>
    <app-admin-header/>
    <div id="create-user-div">
      <v-layout>
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
            <app-user-validations-required :viewType="submitType"/>
            </div>
          </v-flex>
        </v-layout>
      </div>
    <app-footer/>
  </div>
</template>

<script>
import AppCreateUser from '@/components/Registration/CreateUser'
import AdminHeader from '@/components/AdminUserManagement/AdminHeader'
import UserValidations from '@/components/AdminUserManagement/UserValidations'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'CreateUser',
  components: {
    'app-create-user': AppCreateUser,
    'app-admin-header': AdminHeader,
    'app-footer': AppFooter,
    'app-user-validations-required': UserValidations
  },
  data: () => ({
    check: false,
    submitType: 'CreateAdmin'
  }),
  methods: {
    normalUser: function () {
      this.check = false
    },
    adminUser: function () {
      this.check = true
    }
  }
}
</script>

<style scoped>
#create-user-div {
  padding: 2em 6em 0em 10em;
}
</style>
