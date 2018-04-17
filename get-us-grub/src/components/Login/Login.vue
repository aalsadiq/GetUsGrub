<template>
  <div>
    <app-header />
    <v-form ref="form" v-model="validIdentificationInput" >
      <v-text-field v-model="username"
        prepend-icon="pets"
        label="Enter a username"
        name = "username"
        :rules="$store.state.rules.usernameRules"
        required
      ></v-text-field>
      <v-text-field v-model="password"
        prepend-icon="lock"
        name="password"
        label="Password"
        id="password"
        :rules="$store.state.rules.passwordRules"
        :min="8"
        :append-icon="visibile ? 'visibility' : 'visibility_off'"
        :append-icon-cb="() => (visibile = !visibile)"
        :type=" visibile ? 'text' : 'password'"
        required
        ></v-text-field>
      <v-btn color="primary" @click="LoginUser" :disabled="validSecurityInput">Submit</v-btn>
      <div class="text-right">
        <router-link class="md-accent" tag="md-button" to="/recover">Forgot password?</router-link>
        <router-link class="md-accent" tag="md-button" to="/signup">Don't have an account?</router-link>
      </div>
    </v-form>
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
      validIdentificationInput: false,
      username: '',
      password: '',
      state: {
        loading: false
      }
    }
  },
  methods: {
    LoginUser: function (username, password) {
      axios({
        method: 'POST',
        url: 'http://localhost:8081/Login',
        data: {'Username': this.username, 'Password': this.password},
        header: {
          'Access-Control-Allow-Origin': 'http://localhost:8080/#/Login'
        }
      }).then(response => {
        console.log('you in son')
        this.$store.state.isAuthenticated = true
        this.$store.state.authenticationToken = response.data
        this.$store.state.username = jwt.decode(response.data)['Username']
      }).catch(error => {
        console.log(error.response.data)
      })
    }
  }
}
</script>

<style scoped>

</style>
