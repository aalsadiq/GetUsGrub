<template>
  <div id="app">
    <v-app>
      <app-header/>
      <v-container id="router-container">
        <div>
            <!-- <img src="./assets/logo.png"> -->
            <router-view/>
        </div>
      </v-container>
      <app-footer/>
    </v-app>
  </div>
</template>

<script>
import AppHeader from './components/AppHeader'
import AppFooter from './components/AppFooter'

export default {
  name: 'App',
  components: {AppHeader, AppFooter}
}
</script>

<style>
#app {
  font-family: 'Avenir', Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  /* margin-top: 60px; */
}
#router-container {
  margin-top: 4.5em;
}
</style>
