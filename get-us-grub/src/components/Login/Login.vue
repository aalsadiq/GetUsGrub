<template>
  <div>
    <app-header />
    <v-content>
      <v-alert id="login-error" :value=showError icon="warning">
          <span>
            {{ errors }}
          </span>
      </v-alert>
      <v-layout column justify-center>
        <v-container xs12 id="login">
          <v-card mx3>
            <v-toolbar dark color="blue darken-4">
              <v-toolbar-title>Login</v-toolbar-title>
            </v-toolbar>
            <div id="fields-div">
              <v-form ref="form" v-model="valid" >
                <v-text-field v-model="username"
                  prepend-icon="pets"
                  label="Enter a username"
                  name = "username"
                  :rules="$store.state.rules.usernameRules"
                  :disable=disable
                  required
                ></v-text-field>
                <v-text-field v-model="password"
                  prepend-icon="lock"
                  name="password"
                  label="Password"
                  id="password"
                  :rules="$store.state.rules.passwordRules"
                  :min="8"
                  :append-icon="visible ? 'visibility' : 'visibility_off'"
                  :append-icon-cb="() => (visible = !visible)"
                  :type=" visible ? 'text' : 'password'"
                  :disable=disable
                  required
                  ></v-text-field>
                <v-btn color="primary" @click="LoginUser" :disabled="!valid" :loading="loading">Sign In</v-btn>
                <div class="text-right">
                  <router-link class="md-accent" to="ResetPassword">Forgot password?</router-link>
                </div>
                <div class="text-right">
                  <router-link class="md-accent" to="/Registration">Don't have an account?</router-link>
                </div>
              </v-form>
            </div>
          </v-card>
        </v-container>
      </v-layout>
    </v-content>
    <app-footer/>
  </div>
</template>

<script>
import jwt from 'jsonwebtoken'
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'

export default {
  name: 'Login',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  },
  data () {
    return {
      valid: false,
      username: '',
      password: '',
      loader: null,
      loading: false,
      disable: false,
      showError: false,
      visible: false,
      errors: null
    }
  },
  beforeCreate () {
    try {
      if (this.$store.state.authenticationToken === null) {
      } else {
        this.$router.push({path: '/'})
      }
    } catch (ex) {
      this.$router.push({path: '/'})
    }
  },
  watch: {
    // Loading animation on buttons
    loader () {
      const l = this.loader
      this[l] = !this[l]

      setTimeout(() => (this[l] = false), 1000)

      this.loader = null
    }
  },
  methods: {
    LoginUser: function (username, password) {
      this.showError = false
      this.valid = false
      this.disable = true
      this.loader = 'loading'
      axios({
        method: 'POST',
        url: this.$store.state.urls.login.loginUser,
        data: {'Username': this.username, 'Password': this.password}
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
      }).catch(error => {
        this.showError = true
        this.valid = true
        this.disable = false
        this.errors = error.response.data.message
        Promise.reject(error)
      })
    }
  }
}
</script>

<style scoped>
.application .theme--light.stepper--vertical
.stepper__content:not(:last-child),
.theme--light .stepper--vertical
.stepper__content:not(:last-child) {
  border: none;
}
#login {
  max-width: 1500px;
}
#fields-div {
  padding: 2em;
  margin: 1em;
}
</style>
