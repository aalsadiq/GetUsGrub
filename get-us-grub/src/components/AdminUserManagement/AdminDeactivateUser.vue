<template>
    <div>
      <app-admin-header/>
      <h1> Deactivate User </h1>
      <v-layout justify-center>
        <app-user-text-box :viewType="submitType"/>
      </v-layout>
    <app-footer/>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import AppUserTextBox from '@/components/AdminUserManagement/UserTextBox'
export default {
  name: 'DeactivateUserVue',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter,
    'app-user-text-box': AppUserTextBox
  },
  data: () => ({
    submitType: 'DeactivateUser'
  })
}
</script>
