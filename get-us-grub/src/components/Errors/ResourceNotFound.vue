<template>
  <div id="home">
    <div class="content">
      <div>
        <img class="img-logo" src="@/assets/GetUsGrub-Food.png">
        <h1>404 Resource Not Found</h1>
        <h3>You lost? The page you're looking for doesn't exist.</h3>
      </div>
    </div>
    <v-btn @click="goBack" color="cyan darken-2">
      <span class="btn-text">
        Go Back
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
    goBack () {
      this.$router.go(-1)
    }
  }
}
</script>

<style src="@/components/Errors/ErrorsStyleSheet.css">
</style>
