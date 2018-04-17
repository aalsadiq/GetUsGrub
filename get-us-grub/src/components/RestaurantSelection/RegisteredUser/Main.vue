<template>
  <div>
    <v-container class="scroll-y" id="scroll-target">
      <v-container id="main">
        <app-header/>
        <!-- Main page of the restaurant selector -->
        <select-restaurant/>
        <app-footer/>
      </v-container>
    </v-container>
  </div>
</template>

<script>
import SelectRestaurant from '@/components/RestaurantSelection/SelectRestaurant'
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import jwt from 'jsonwebtoken'

export default {
  name: 'RestaurantSelectionMain',
  // Adding Vue components dependencies
  components: {
    AppHeader,
    AppFooter,
    SelectRestaurant
  },
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push({path: '/Unauthorized'})
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantSelection === 'True') {
      } else {
        this.$router.push({path: '/Forbidden'})
      }
    } catch (ex) {
      this.$router.push({path: '/Forbidden'})
    }
  }
}
</script>

<style>
#scroll-target {
  max-height: 50.5em;
  margin: -0.8em 1em 0em 1em;
}
#main {
  max-width: 1200px;
}
</style>
