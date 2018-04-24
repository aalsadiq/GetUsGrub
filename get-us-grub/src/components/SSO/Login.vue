<template>
  <div id = 'sso-login'>
    <app-header />
    <div id = 'login-card' align='center'>
      <v-flex xs8>
        <!-- Title bar for the Food Preferences -->
        <v-card id = 'login-message'>
          <div v-if='isLoading'>
            <v-avatar :size=250 :tile='true'><img src="@/assets/GetUsGrub-Food.png"></v-avatar>
            <v-card-title>
              Please wait while we securely log you in...
            </v-card-title>
            <v-progress-circular :size=50 indeterminate color="primary"></v-progress-circular>
          </div>
          <div v-if='!isLoading'>
            <v-avatar :size=250><img src="@/assets/GetUsGrub-Sad.png"></v-avatar>
            <v-card-title>
              An error has occurred. Please try logging in.
            </v-card-title>
          </div>
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
      this.isLoading = false
    })
    console.log(queryJwt)
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
