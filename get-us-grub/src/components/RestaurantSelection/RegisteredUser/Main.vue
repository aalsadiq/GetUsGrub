<template>
  <div>
    <v-container class="scroll-y" id="scroll-target">
      <v-container id="restaurant-selection-registered-user">
        <app-header/>
        <v-container id="select-restaurant">
          <!-- Main page of the restaurant selector -->
          <select-restaurant/>
        </v-container>
        <app-footer/>
      </v-container>
    </v-container>
  </div>
</template>

<script>
import SelectRestaurant from './SelectRestaurant'
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import jwt from 'jsonwebtoken'

export default {
  name: 'RestaurantSelectionRegisteredUserMain',
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
  max-height: 56.5em;
  position: absolute;
  overflow-x: hidden;
}
#restaurant-selection-registered-user {
  max-width: 1200px;
  margin: auto;
}
</style>
