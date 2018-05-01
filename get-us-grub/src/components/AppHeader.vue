<template>
  <v-toolbar id="header-toolbar" app dark fixed>
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
    <v-spacer />
    <v-toolbar-items v-resize="onResize" v-if="windowSize.width <= mobileScreenSize">
      <v-menu offset-y>
        <v-btn flat dark slot="activator">
          <v-icon dark>list</v-icon>
        </v-btn>
        <v-list>
          <v-list-tile id="RegistrationTile" v-if="showWithoutAuthentication()">
            <v-btn
              flat
              class="nav-btn"
              to="Registration"
            >
            Register
            </v-btn>
          </v-list-tile>
          <v-list-tile id="LoginTile" v-if="showWithoutAuthentication()">
            <v-btn
              flat
              class="nav-btn"
              to="Login"
            >
            Login
            </v-btn>
          </v-list-tile>
          <v-list-tile id="ProfileTile" v-if="!showWithoutAuthentication()">
            <v-btn
              flat
              class="nav-btn"
              to="Profile"
            >
            {{ this.$store.state.username}}
            </v-btn>
          </v-list-tile>
          <v-list-tile id="BillSplitterTile">
            <v-btn
              flat
              class="nav-btn"
              to="RestaurantBillSplitter"
            >
            Split Bill
            </v-btn>
          </v-list-tile>
          <v-list-tile v-if="!showWithoutAuthentication()">
            <v-btn
              flat
              class="nav-btn"
              @click="logout"
            >
            Logout
            </v-btn>
          </v-list-tile>
        </v-list>
      </v-menu>
    </v-toolbar-items>
    <v-toolbar-items v-resize="onResize" v-if="windowSize.width > mobileScreenSize">
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
  data: () => ({
    windowSize: {
      width: 0,
      height: 0
    },
    mobileScreenSize: 1000,
    showRegisteredRestaurantSelection: false
  }),
  mounted () {
    this.onResize()
  },
  methods: {
    onResize () {
      this.windowSize = { width: window.innerWidth, height: window.innerHeight }
    },
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
      axios.post(this.$store.state.urls.logout.logoutUser, {}, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        }
      }).then(response => {
        this.$store.commit('setAuthenticationToken', null)
        this.$store.commit('setIsAuthenticated', false)
        this.$store.commit('setUsername', '')
        // Force refresh of page
        location.reload()
        this.$router.push({path: '/'})
      }).catch(error => {
        this.$store.commit('setAuthenticationToken', null)
        this.$store.commit('setIsAuthenticated', false)
        this.$store.commit('setUsername', '')
        // Force refresh of page
        location.reload()
        this.$router.push({path: '/'})
        Promise.reject(error)
      })
    }
  }
}
</script>

<style scoped>
#nav {
  margin: auto;
}
#home-btn {
  text-transform: none;
}
#header-toolbar {
  background-color: #5caabc;
}
.btn-text {
  font-weight: bold;
  font-size: normal;
  color: rgb(255, 255, 255);
}
#toolbar-title {
  font-weight: bold;
  font-size: 1.3em
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
</style>
