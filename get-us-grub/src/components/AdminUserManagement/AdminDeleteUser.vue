<template>
    <div>
      <app-admin-header/>
        <div id = 'user-text-box'>
            <h1> Delete User </h1>
          <app-user-text-box :viewType="submitType"/>
        </div>
    <app-footer/>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import AppUserTextBox from '@/components/AdminUserManagement/UserTextBox'
export default {
  name: 'DeleteUserVue',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter,
    'app-user-text-box': AppUserTextBox
  },
  data: () => ({
    submitType: 'DeleteUser'
  })
}
</script>
