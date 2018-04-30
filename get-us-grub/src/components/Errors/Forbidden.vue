<template>
  <div id="home">
    <div class="content">
      <img class="img-logo" src="@/assets/GetUsGrub-Sad.png">
      <h1>403 Forbidden</h1>
      <h3>You do not have permission to access this page.</h3>
    </div>
    <v-btn @click="goHome" color="cyan darken-2">
      <span class="btn-text">
        Home
      </span>
    </v-btn>
    <app-footer/>
  </div>
</template>

<script>
import AppFooter from '@/components/AppFooter'

export default {
  name: 'Forbidden',
  components: {
    AppFooter
  },
  methods: {
    goHome () {
      this.$router.push({ path: '/' })
    }
  }
}
</script>

<style src="@/components/Errors/ErrorsStyleSheet.css">
</style>
