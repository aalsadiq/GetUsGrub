<template>
    <div>
      <app-admin-header/>
      <app-footer/>
  </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'
export default {
  name: 'AdminHome',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter
  },
  methods: {
    EditUser () {
      axios.put('/User/EditUser')
        .then(response => {
          console.log('EditUser!!!!')
        })
    }
  }
}
</script>
