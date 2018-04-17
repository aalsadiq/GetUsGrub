<template>
    <div id="home">
      <app-header/>
      <img src="@/assets/GetUsGrub.png">
      <p>General Error</p>
      <v-btn @click="goBack" color="cyan darken-2">
      <span class="btn-text">
        Go Back
      </span>
      </v-btn>
      <app-footer/>
    </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'

export default {
  name: 'Home',
  components: {
    AppHeader,
    AppFooter
  },
  methods: {
    goBack () {
      this.$router.go(-1)
    }
  }
}
</script>
