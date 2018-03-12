<template>
    <div>
      <app-admin-header/>
      <button v-on:click="DeactivateUser">Deactivate User </button>
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
    DeactivateUser () {
      axios.put('/User/DeactivateUser')
        .then(response => {
          console.log('DONE!!!!')
        })
    }
  }
}
</script>
