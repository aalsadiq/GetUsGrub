<template>
  <div id="edit-user-div">
    <app-admin-header/>
    <v-flex xs15order-lg2>
      <h1>Edit User</h1>
        <app-user-validations-not-required/>
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
    check: false
  })
}
</script>

<style scoped>

</style>
