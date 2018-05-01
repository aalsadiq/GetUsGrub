<template>
  <div>
    <app-admin-header/>
    <div id="create-user-div">
      <h1>Select your user!</h1>
      <v-btn v-on:click="normalUser" id ="NormalUser-button" color="info">Regular Registration</v-btn>
      <v-btn v-on:click="adminUser" id ="AdminUser-button" color="info">Admin Registration</v-btn>
      <div v-if="check===false">
        <app-create-user/>
      </div>
      <div v-else>
        <app-user-validations-required :viewType="submitType"/>
        </div>
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
