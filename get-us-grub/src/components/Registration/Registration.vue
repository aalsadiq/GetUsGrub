<template>
  <div>
    <app-header/>
      <div id="registration">
        <app-create-user/>
      </div>
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

<style>
#registration {
  padding: 2.5em 0 0 0;
  margin: 3.5em 10em 10em 11.5em;
}
</style>
