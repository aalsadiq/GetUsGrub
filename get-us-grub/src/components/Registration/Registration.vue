<template>
  <div>
    <app-header/>
    <app-create-user id="create-user"/>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import AppCreateUser from './CreateUser'

export default {
  name: 'AdminHome',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter,
    'app-create-user': AppCreateUser
  },
  beforeCreate () {
    try {
      if (this.$store.state.authenticationToken === null) {
      } else {
        this.$router.push({path: '/'})
      }
    } catch (ex) {
      this.$router.push({path: '/'})
    }
  }
}
</script>
