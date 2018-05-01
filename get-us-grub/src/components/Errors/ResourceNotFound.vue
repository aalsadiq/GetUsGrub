<template>
  <div id="home">
    <div class="content">
      <div>
        <img class="img-logo" src="@/assets/GetUsGrub-Food.png">
        <h1>404 Resource Not Found</h1>
        <h3>You lost? The page you're looking for doesn't exist.</h3>
      </div>
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
  name: 'ResourceNotFound',
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
