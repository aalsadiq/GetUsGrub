<template>
  <div id="app">
    <v-app>
      <v-container id="router-container">
        <div>
          <router-view/>
        </div>
      </v-container>
    </v-app>
  </div>
</template>

<script>
export default {
  name: 'App'
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
html {
  overflow-y: auto;
}
</style>
