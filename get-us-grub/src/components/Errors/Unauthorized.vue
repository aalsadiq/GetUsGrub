<template>
  <div id="home">
    <div class="content">
      <img class="img-logo" src="@/assets/GetUsGrub-Sad.png">
      <h1>401 Unauthorized</h1>
      <h3>Your access to this page has been denied.</h3>
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
  name: 'Unauthorized',
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
