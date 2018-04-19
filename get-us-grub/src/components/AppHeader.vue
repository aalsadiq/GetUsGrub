<template>
  <v-toolbar id="header-toolbar" dark fixed>
    <v-toolbar-items>
      <v-btn flat id="home-btn" to="/">
        <v-avatar :size="52" tile="true"><img src="@/assets/GetUsGrub.png"></v-avatar>
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
        to="/Registration"
        v-if="showWithoutAuthentication()"
      >
        <span class="nav-btn-text">REGISTER</span>
      </v-btn>
      <v-btn
        flat
        class="nav-btn"
        to="/Login"
        v-if="showWithoutAuthentication()"
      >
        <span class="nav-btn-text">LOGIN</span>
      </v-btn>
      <v-btn
        flat
        class="nav-btn"
        to="/RestaurantBillSplitter"
      >
        <span class="nav-btn-text">SPLIT BILL</span>
      </v-btn>
       <v-btn
        flat
        class="nav-btn"
        to="/Profile"
        v-if="this.$store.state.isAuthenticated"
      >
        <span class="nav-btn-text">PROFILE</span>
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
      this.$store.dispatch('setAuthenticationToken', null)
      location.reload()
      this.$router.push({path: '/'})
    }
  }
}
</script>

<style>
#nav {
  margin: auto;
}
#header-toolbar {
  background-color: #329fa3;
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
.btn__content:before {
  opacity: 0.20;
  color: rgb(255, 255, 255);
}
.nav-btn-text {
  font-weight: bold;
  font-size: normal;
  color: rgb(255, 255, 255);
}
#home-btn > .btn__content:before {
  opacity: 0;
}
</style>
