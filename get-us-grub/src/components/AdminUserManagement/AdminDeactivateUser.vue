<template>
    <div>
      <app-admin-header/>
        <div id = "user-text-box">
          <h1> Deactivate User Page </h1>
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
    DeactivateUser () {
      axios.put('/User/DeactivateUser')
        .then(response => {
          console.log('DONE!!!!')
        })
    },
    userSubmit () {
      if (this.$refs.form.validate()) {
        // Native form submission is not yet supported
        axios.put('/User/DeactivateUser', {
          username: this.username,
          headers: {
            'Content-type': 'application/json'
          }
        })
      }
    }
  }
}
</script>
