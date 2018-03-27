<template>
  <div>
    <app-header/>
    <v-form ref="form" v-model="validIdentificationInput">
      <v-text-field
        label="Enter a username"
        name = "username"
        :rules="usernameRules"
        required
      ></v-text-field>
      <v-text-field 
        prepend-icon="lock" 
        name="password" 
        label="Password" 
        id="password" 
        type="password"
        :rules="passwordRules"
        :min="8"
        :counter="64"
        :append-icon="visibile ? 'visibility' : 'visibility_off'"
        :append-icon-cb="() => (visibile = !visibile)"
        :type=" visibile ? 'text' : 'password'"
        required
        ></v-text-field>
      <v-btn color="primary" @click="loginUser" :disabled="!validSecurityInput">Submit</v-btn>
      
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
  name: 'Login'
  data () {
  return {
  credentials: {
  username: '',
  password: ''
  },
  state: {
  loading: false
  }
  }
  },
  
</script>