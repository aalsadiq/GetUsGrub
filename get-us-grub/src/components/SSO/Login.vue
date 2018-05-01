<template>
  <div>
    <app-header />
    <v-content>
      <v-container>
        <div align='center'>
          <v-card id = 'login-card'>
            <div v-if='isLoading'>
              <v-avatar :size=250 :tile='true'><img src="@/assets/GetUsGrub-Food.png"></v-avatar>
              <v-card-title class="justify-center">
                Please wait while we securely log you in...
              </v-card-title>
              <v-progress-circular :size=50 indeterminate color="primary" />
            </div>
            <div v-if='!isLoading'>
              <v-avatar :size=250><img src="@/assets/GetUsGrub-Sad.png"></v-avatar>
              <v-card-title class="justify-center">
                An error has occurred. Please try logging in manually.
              </v-card-title>
            </div>
          </v-card>
        </div>
      </v-container>
    </v-content>
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
  // Log user in based on query when the page loads
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
      var decodedJwt = jwt.decode(response.data)
      this.$store.commit('setUsername', decodedJwt['Username'])
      if (decodedJwt.ReadIsFirstTimeUser === 'True') {
        this.$store.state.firstTimeUserToken = response.data
        this.$router.push('FirstTimeRegistration')
      } else {
        this.$store.commit('setIsAuthenticated', true)
        this.$store.commit('setAuthenticationToken', response.data)
        this.$router.push({path: '/'})
      }
    }).catch(ex => {
      this.isLoading = false
    })
  }
}
</script>

<style>
#login-card {
  max-width: 450px;
}
</style>
