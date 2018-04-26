<template>
  <v-toolbar id="header-toolbar" dark fixed>
    <v-toolbar-items>
      <v-btn flat id="home-btn" to="/">
        <v-avatar :size="52" :tile="true"><img src="@/assets/GetUsGrub.png"></v-avatar>
        <v-toolbar-title>
          <span id="toolbar-title">
            GetUsGrub
          </span>
        </v-toolbar-title>
      </v-btn>
    </v-toolbar-items>
    <v-spacer></v-spacer>
    <v-toolbar-items>
      <v-btn
        flat
        class="nav-btn"
        to="Registration"
        v-if="showWithoutAuthentication()"
      >
        <span class="nav-btn-text">REGISTER</span>
      </v-btn>
      <v-btn
        flat
        class="nav-btn"
        to="Login"
        v-if="showWithoutAuthentication()"
      >
        <span class="nav-btn-text">LOGIN</span>
      </v-btn>
      <v-tooltip bottom>
        <v-btn
          flat
          slot="activator"
          class="nav-btn"
          to="Profile"
          v-if="!showWithoutAuthentication()"
        >
          <v-icon id="person-icon">person</v-icon>
          <span class="nav-btn-text" id="username-text">
            {{ this.$store.state.username }}
          </span>
        </v-btn>
        <span>Profile</span>
      </v-tooltip>
      <v-btn
        flat
        class="nav-btn"
        to="RestaurantBillSplitter"
      >
        <span class="nav-btn-text">SPLIT BILL</span>
      </v-btn>
      <v-btn
        flat
        class="nav-btn"
        v-if="!showWithoutAuthentication()"
        @click="logout"
      >
        <span class="nav-btn-text">LOGOUT</span>
      </v-btn>
    </v-toolbar-items>
  </v-toolbar>
</template>

<script>
import axios from 'axios'

export default {
  data () {
    return {
      showRegisteredRestaurantSelection: false
    }
  },
  methods: {
    showWithoutAuthentication () {
      try {
        if (this.$store.state.authenticationToken === null) {
          return true
        } else {
          return false
        }
      } catch (ex) {
        return false
      }
    },
    logout () {
      axios.post('http://localhost:8081/Logout', {}, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        }
      }).then(response => {
        this.$store.commit('setAuthenticationToken', null)
        this.$store.commit('resetState', this.$store.state)
        this.$router.push({path: '/'})
      }).catch(error => {
        this.$store.commit('setAuthenticationToken', null)
        this.resetState()
        this.$store.commit('resetState', this.$store.state)
        Promise.reject(error)
      })
    }
  }
}
</script>

<style>
#nav {
  margin: auto;
}
#header-toolbar {
  background-color: #5caabc;
}
div.btn__content {
  text-transform: none;
}
.btn-text {
  font-weight: bold;
  font-size: normal;
  color: rgb(255, 255, 255);
}
#toolbar-title {
  font-weight: bold;
  font-size: 1.4em;
}
.btn__content:before {
  opacity: 0.23;
  color: rgb(255, 255, 255);
}
.nav-btn-text {
  font-weight: bold;
  font-size: 1.2em;
  color: rgb(255, 255, 255);
}
#home-btn > .btn__content:before {
  opacity: 0;
}
#username-text {
  text-transform: uppercase;
}
#person-icon {
  margin: 0 0.4em 0.1em 0;
}
</style>
