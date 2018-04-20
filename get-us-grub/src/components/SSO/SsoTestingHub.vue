<template>
  <div id='sso-testing-hub'>
    <app-header/>
    <div id="testing-hub">
      <!-- Not sure why the spacing is off... <br> tags are temporary -->
      <br>
      <br>
      <v-flex xs4>
        <v-text-field
          label="Enter a username"
          :rules="$store.state.rules.usernameRules"
          hide-details
          required
        />
        <v-text-field
          label="Enter a password"
          :rules="$store.state.rules.passwordRules"
          :min="8"
          :counter="64"
          :append-icon="visible ? 'visibility' : 'visibility_off'"
          :append-icon-cb="() => (visible = !visible)"
          :type=" visible ? 'text' : 'password'"
          required
          v-on:input="$emit('input', value)"
        />
      </v-flex>
      <v-flex xs4>
        <div class="grid">
          <v-btn @click.prevent="register">register</v-btn>
        </div>
      </v-flex>
    </div>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'

export default {
  name: 'SsoTestingHub',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  },
  data: () => ({
  }),
  methods: {
    register () {
      console.log('registration called')
    },
    login () {
      console.log('login called')
    }
  }
}
</script>

<style scoped>
  .flex {
    margin: auto !important;
  }
  .grid {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    grid-gap: 10px;
    grid-auto-rows: minmax(100px, auto);
  }
</style>
