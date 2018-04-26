<template>
  <div id="app">
    <v-app>
      <div id="router-div">
        <router-view id="router"/>
        <!-- Popup for refreshing session -->
        <v-snackbar
          :timeout="timeout"
          :top="verticalPosition === 'top'"
          :bottom="verticalPosition === 'bottom'"
          :right="horizontalPosition === 'right'"
          :left="horizontalPosition === 'left'"
          :multi-line="mode === 'multi-line'"
          :vertical="mode === 'vertical'"
          v-model="popUp"
          :color="popUpColor"
        >
          <v-icon>warning</v-icon>
          <span id="session-expire-text"> Your session will expire in {{ expMinutes }} </span>
          <v-btn id='refresh-btn' light color="white" @click.native="refresh" :disable="disable">Refresh</v-btn>
        </v-snackbar>
      </div>
    </v-app>
  </div>
</template>

<script>
import jwt from 'jsonwebtoken'
import moment from 'moment'
import axios from 'axios'
export default {
  name: 'App',
  data () {
    return {
      datenow: null,
      exp: null,
      expMinutes: '5 minutes',
      popUp: false,
      verticalPosition: 'bottom',
      horizontalPosition: 'left',
      mode: 'mult-line',
      popUpColor: 'warning',
      timeout: 10000000,
      disable: false
    }
  },
  methods: {
  // Compare expiration time with the current date time and triggers the popup
    compareTime () {
      try {
        if (this.$store.state.authenticationToken == null) {
          this.popUp = false
        } else if (this.exp.diff(this.dateTimeNow, 'seconds') <= 10) {
          this.popUp = false
          this.logoutUser()
        } else if (!this.popUp && this.exp.diff(this.dateTimeNow, 'minutes') <= 5 && this.exp.diff(this.dateTimeNow, 'minutes') > 0) {
          this.expMinutes = '5 minutes'
          this.popUpColor = 'error'
          this.popUp = true
        }
      } catch (ex) {}
    },
    time () {
      // Set dateTimeNow as a moment object in unix format
      this.dateTimeNow = moment.unix(moment().unix())
    },
    logoutUser () {
      axios.post('http://localhost:8081/Logout', {}, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        }
      }).then(response => {
        this.$store.commit('setAuthenticationToken', null)
        this.$router.push({path: '/'})
      }).catch(error => {
        this.$store.commit('setAuthenticationToken', null)
        this.$router.push({path: '/'})
        Promise.reject(error)
      })
    },
    // Set the exp variable if there is a token in the Vuex store
    setExpiration () {
      try {
        if (this.$store.state.authenticationToken != null) {
          // Set exp as a moment object in unix format
          this.exp = moment.unix(jwt.decode(this.$store.state.authenticationToken).exp)
        }
      } catch (ex) {}
    },
    // Refresh the user's session by calling creation of new token
    refresh () {
      this.disable = true
      axios.post('http://localhost:8081/RenewSession', {}, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        }
      }).then(response => {
        this.popUp = false
        this.$store.commit('setAuthenticationToken', response.data)
        this.exp = moment.unix(jwt.decode(this.$store.state.authenticationToken).exp)
        this.disable = false
      }).catch(error => {
        Promise.reject(error)
        this.popUp = false
        this.disable = false
      })
    }
  },
  // Set time intervals throughout user's session
  mounted () {
    // Call function every 5000 milliseconds (5 seconds)
    this.timeInterval = setInterval(this.time, 5000)
    this.setExpirationInterval = setInterval(this.setExpiration, 50000)
    this.compareInterval = setInterval(this.compareTime, 5000)
  },
  // Clear all intervals set
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
  background-color: rgb(243, 243, 243);
}
/* Removing scrollbar on page */
html {
  background-color: rgb(243, 243, 243);
  overflow-x: hidden;
}
/* Make scrollbar transparent */
::-webkit-scrollbar {
    width: 0px;
    background: transparent;
}
/* Omit text underlines to router-links */
a {
  text-decoration: none;
}
.container {
  max-width: 100%;
}
#refresh-btn {
  color: rgb(177, 13, 13);
  font-weight: bold;
  padding: 0.3em;
}
#router {
  width: 100%;
  padding: 0 0 3em 0;
}
#session-expire-text {
  margin: 0em -1.5em 0em 0.7em;
  font-weight: bold;
}
</style>
