<template>
  <div id="app">
    <v-app>
      <v-container id="router-container">
        <router-view/>
        <v-snackbar
          :timeout="timeout"
          :top="verticalPosition === 'top'"
          :bottom="verticalPosition === 'bottom'"
          :right="horizontalPosition === 'right'"
          :left="horizontalPosition === 'left'"
          :multi-line="mode === 'multi-line'"
          :vertical="mode === 'vertical'"
          v-model="snackbar"
          :color="'error'"
        >
          Your session will expire in {{ expMinutes }}
          <v-btn id='refresh-btn' light color="white" @click.native="refresh">Refresh</v-btn>
        </v-snackbar>
      </v-container>
    </v-app>
  </div>
</template>

<script>
import jwt from 'jsonwebtoken'
import moment from 'moment'

export default {
  name: 'App',
  data () {
    return {
      datenow: null,
      exp: null,
      expMinutes: '5 minutes',
      snackbar: false,
      verticalPosition: 'bottom',
      horizontalPosition: 'left',
      mode: 'mult-line',
      timeout: 10000000
    }
  },
  methods: {
    time () {
      this.datenow = moment.unix(moment().unix())
    },
    logoutUser () {
      this.$store.dispatch('setAuthenticationToken', null)
      location.reload()
      this.$router.push({path: '/'})
    },
    compareTime () {
      try {
        if (this.$store.state.authenticationToken == null) {
          this.snackbar = false
        } else if (!this.snackbar && this.exp.diff(this.datenow, 'minutes') < 5 && this.exp.diff(this.datenow, 'minutes') > 0) {
          this.expMinutes = '5 minutes'
          this.snackbar = true
        } else if (!this.snackbar && this.exp.diff(this.datenow, 'minutes') < 1 && this.exp.diff(this.datenow, 'minutes') > 0) {
          this.expMinutes = '1 minute'
          this.snackbar = true
        } else if (this.exp.diff(this.datenow, 'seconds') <= 10) {
          this.snackbar = false
          this.logoutUser()
        }
      } catch (ex) {}
    },
    setExpiration () {
      try {
        if (this.$store.state.authenticationToken != null) {
          this.exp = moment.unix(jwt.decode(this.$store.state.authenticationToken).exp)
        }
      } catch (ex) {}
    },
    refresh () {
      console.log('refresh')
      // this.snackbar = false
    }
  },
  mounted () {
    this.timeInterval = setInterval(this.time, 1000)
    this.setExpirationInterval = setInterval(this.setExpiration, 1000)
    this.compareInterval = setInterval(this.compareTime, 1000)
  },
  beforeDestroy () {
    clearInterval(this.timeInterval)
    clearInterval(this.setExpirationInterval)
    clearInterval(this.compareInterval)
  }
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
#router-container {
  margin-top: 3.3em;
}
/* Removing scrollbar on page */
html {
  overflow-y: hidden;
  overflow-x: hidden;
}
/* Omit text underlines to router-links */
a {
  text-decoration: none;
}
.container {
  background-color: rgb(255, 255, 255);
  max-width: 100%;
  padding: 0px 35px 0px 16px;
}
#refresh-btn {
  color: rgb(177, 13, 13);
  font-weight: bold;
  padding: 0.3em;
}
</style>
