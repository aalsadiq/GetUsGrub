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
import CreateUser from './CreateUser'
import jwt from 'jsonwebtoken'

export default {
  name: 'FirstTimeRegistration',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter,
    'app-create-user': CreateUser
  },
  beforeCreate () {
    if (this.$store.state.firstTimeUserToken === null) {
      this.$router.push('Unauthorized')
    }
    try {
      if (jwt.decode(this.$store.state.firstTimeUserToken).ReadIsFirstTimeUser === 'True') {
      } else {
        this.$router.push('Forbidden')
      }
    } catch (ex) {
      this.$router.push('Forbidden')
    }
  }
}
</script>

<style scoped>
#registration {
  padding: 2.5em 0 0 0;
  margin: 3.5em 10em 10em 11.5em;
}
</style>
