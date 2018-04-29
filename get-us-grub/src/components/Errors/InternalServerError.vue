<template>
  <div id="home">
    <div class="content">
      <img class="img-logo" src="@/assets/GetUsGrub-Sad.png">
      <h1>500 Internal Server Error</h1>
      <h3>It's not you, it's us. Our team is working hard to fix the issue.</h3>
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
  name: 'InternalServerError',
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
