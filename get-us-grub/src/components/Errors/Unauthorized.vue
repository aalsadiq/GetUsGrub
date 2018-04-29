<template>
  <div id="home">
    <div class="content">
      <img class="img-logo" src="@/assets/GetUsGrub-Sad.png">
      <h1>401 Unauthorized</h1>
      <h3>Your access to this page has been denied.</h3>
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
  name: 'Unauthorized',
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
