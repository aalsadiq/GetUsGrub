<template>
    <div>
      <app-admin-header/>
        <div id = 'user-text-box'>
            <h1> Delete User Page </h1>
          <app-user-text-box/>
        </div>
    <app-footer/>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import AppUserTextBox from '@/components/AdminUserManagement/UserTextBox'
import axios from 'axios'
export default {
  name: 'AdminHome',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter,
    'app-user-text-box': AppUserTextBox
  },
  methods: {
    DeleteUser () {
      axios.put('/User/DeleteUser')
        .then(response => {
          console.log('Deleted!!!!')
        })
    }
  }
}
</script>
