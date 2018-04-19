<template>
    <div id="home">
      <img src="@/assets/GetUsGrub.png">
      <p>Internal Server Error</p>
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
