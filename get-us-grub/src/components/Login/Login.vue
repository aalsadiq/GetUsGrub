<template>
  <div>
    <app-header/>
    <v-form ref="form" v-model="validIdentificationInput" >
      <v-text-field v-model="username"
        prepend-icon="pets"
        label="Enter a username"
        name = "username"
        :rules="usernameRules"
        required
      ></v-text-field>
      <v-text-field v-model="password"
        prepend-icon="lock"
        name="password"
        label="Password"
        id="password"
        :rules="rules.passwordRules"
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
      rules: {
        usernameRules: [
          username => !!username || 'Username is required',
          username => /^[A-Za-z\d]+$/.test(username) || 'Username must contain only letters and numbers'
        ],
        passwordRules: [
          password => !!password || 'Password is required',
          password => password.length >= 8 || 'Password must be at least 8 characters',
          password => password.length < 64 || 'Password must be at most 64 characters'
        ]
      },
      username: '',
      password: '',
      state: {
        loading: false
      }
    }
  },
  methods: {
    LoginUser: function (username, password) {
      console.log('you in son')
      axios({
        method: 'POST',
        url: 'http://',
        data:{'': ''}
      })
    }
  }
}
</script>

<style scoped>

</style>
