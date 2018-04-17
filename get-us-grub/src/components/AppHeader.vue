<template>
    <v-toolbar id="header-toolbar" dark fixed>
      <div v-show="!showRestaurantSelection()">
        <router-link to="/">
          <v-btn flat class="home-btn">
            <v-icon>home</v-icon>
            <v-toolbar-title>
              <span id="toolbar-title">
                GetUsGrub
              </span>
            </v-toolbar-title>
          </v-btn>
        </router-link>
      </div>
      <div v-show="showRestaurantSelection()">
        <router-link to="/RestaurantSelection/Registered">
          <v-btn flat class="home-btn">
            <v-icon>home</v-icon>
            <v-toolbar-title>
              <span id="toolbar-title">
                GetUsGrub
              </span>
            </v-toolbar-title>
          </v-btn>
        </router-link>
      </div>
      <v-spacer></v-spacer>
      <div>
        <router-link to="/Registration">
          <v-btn
            color="red lighten-3"
            class="nav-btn"
            v-show="showWithoutAuthentication()"
          >
            <span class="btn-text">REGISTER</span>
          </v-btn>
        </router-link>
        <router-link to="/Login">
          <v-btn
            color="red lighten-3"
            class="nav-btn"
            v-show="showWithoutAuthentication()"
          >
            <span class="btn-text">LOGIN</span>
          </v-btn>
        </router-link>
        <router-link to="/RestaurantBillSplitter">
          <v-btn
            color="red lighten-3"
            class="nav-btn"
          >
            <span class="btn-text">SPLIT BILL</span>
          </v-btn>
        </router-link>
        <router-link to="/Profile">
        <!-- Highly suggest not to do a v-if like this. You should check by claims in token instead -->
          <v-btn
            color="red lighten-3"
            class="nav-btn"
            v-if="this.$store.state.isAuthenticated"
          >
            <span class="btn-text">PROFILE</span>
          </v-btn>
        </router-link>
        <router-link to="/Logout">
          <v-btn
            color="grey"
            class="nav-btn"
            v-show="!showWithoutAuthentication()"
          >
            <span class="btn-text">LOGOUT</span>
          </v-btn>
        </router-link>
      </div>
    </v-toolbar>
</template>

<script>
import jwt from 'jsonwebtoken'

export default {
  data () {
    return {
      showRegisteredRestaurantSelection: false
    }
  },
  methods: {
    showRestaurantSelection () {
      try {
        if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantSelection === 'True') {
          return true
        } else {
          return false
        }
      } catch (ex) {
        return false
      }
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
  font-size: x-large;
}
</style>
