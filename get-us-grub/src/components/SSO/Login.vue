<template>
  <div id = 'sso-login'>
    <app-header />
    <div id = 'login-card' align='center' v-if='isLoading'>
      <v-flex xs8>
        <!-- Title bar for the Food Preferences -->
        <v-card id = 'login-message'>
          <v-card-title>
            Please wait while we securely log you in...
          </v-card-title>
          <v-progress-circular :size=50 indeterminate color="primary"></v-progress-circular>
        </v-card>
      </v-flex>
    </div>
    <app-footer />
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'
import jwt from 'jsonwebtoken'

export default {
  name: 'SsoLogin',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  },
  data: () => ({
    isLoading: true
  }),
  created () {
    var queryJwt = this.$route.query.jwt
    axios({
      method: 'GET',
      url: this.$store.state.urls.sso.login,
      headers: {
        Authorization: `Bearer ${queryJwt}`
      }
    }).then(response => {
      this.valid = true
      this.disable = false
      this.$store.state.isAuthenticated = true
      this.$store.state.authenticationToken = response.data
      this.$store.state.username = jwt.decode(response.data)['Username']
      this.$router.push({path: '/'})
    }).catch(ex => {
    })
    console.log(jwt)
  }
}
</script>

<style scoped>
#login-card {
  margin-top: 10em
}
#login-message {
  font-size: 150%;
  display: inline-block;
}
</style>
