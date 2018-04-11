<template>
  <div>
      {{ responseData }}
    <app-admin-header/>
    <v-flex xs15order-lg2>
      <h1>Select your user!</h1>
      <v-btn v-on:click="editUserAccountButton" id ="editUserAccount-button" color="info">Edit User Account</v-btn>
      <v-btn v-on:click="editUserProfileButton" id ="editUserProfile-button" color="info">Edit User Profile Profile</v-btn>
          <div v-if="check===false">
            <app-user-validations-not-required :viewType="submitType"/>
            </div>
          <div v-else>
            {{ 'Call Profile vue... '}}
          </div>
    </v-flex>
    <app-footer/>
  </div>
</template>

<script>
import AppCreateUser from '@/components/Registration/CreateUser'
import AdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import UserValidationsNoRequiredFields from '@/components/AdminUserManagement/UserValidationNoRequiredFields'
export default {
  name: 'CreateUser',
  components: {
    'app-create-user': AppCreateUser,
    'app-admin-header': AdminHeader,
    'app-footer': AppFooter,
    'app-user-validations-not-required': UserValidationsNoRequiredFields
  },
  data: () => ({
    check: false,
    submitType: 'EditUser'
  }),
  methods: {
    editUserAccountButton: function () {
      this.check = false
    },
    editUserProfileButton: function () {
      this.check = true
    }
  }
}
</script>
