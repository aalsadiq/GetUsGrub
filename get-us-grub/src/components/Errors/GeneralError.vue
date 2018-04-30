<template>
  <div>
    <div class="content">
      <img class="img-logo" src="@/assets/GetUsGrub-Sad.png">
      <h1>General Error</h1>
      <h3>Sorry! Something must have went wrong. Please try again later.</h3>
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
  name: 'GeneralError',
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
