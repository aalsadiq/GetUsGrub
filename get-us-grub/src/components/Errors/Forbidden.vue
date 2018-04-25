<template>
    <div id="home">
      <img src="@/assets/GetUsGrub.png">
      <p>Forbidden</p>
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
