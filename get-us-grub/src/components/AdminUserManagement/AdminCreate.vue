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
