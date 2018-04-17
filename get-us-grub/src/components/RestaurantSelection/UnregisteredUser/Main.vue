<template>
  <div>
    <v-container class="scroll-y" id="scroll-target">
      <v-container id="unregistered-restaurant-selection">
        <v-alert id='login-redirect' outline icon="info" :value="true">
          Attention individual users:
          <router-link to="/Login">
            <v-btn small id='login-redirect-btn' color="blue lighten-1"><span class="btn-text">Login</span></v-btn>
          </router-link> to include food preferences in your search
        </v-alert>
        <!-- Main page of the restaurant selector -->
        <select-restaurant/>
      </v-container>
    </v-container>
  </div>
</template>

<script>
import SelectRestaurant from '@/components/RestaurantSelection/SelectRestaurant'
import jwt from 'jsonwebtoken'

export default {
  name: 'RestaurantSelectionUnregisteredUserMain',
  // Adding Vue components dependencies
  components: {
    SelectRestaurant
  },
  beforeCreate () {
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantSelection === 'True') {
        this.$router.push({path: '/RestaurantSelection/Registered'})
      }
    } catch (ex) {}
  }
}
</script>

<style>
#login-redirect {
  margin: -1em 1.1em -0.3em 1.1em;
  font-size: small;
  padding: 0.1em 1em 0.1em 1em;
  color: rgb(82, 159, 247) !important;
}
#scroll-target {
  max-height: 50.5em;
  margin: -0.8em 1em 0em 1em;
  position: absolute;
  overflow-x: hidden;
}
#unregistered-restaurant-selection {
  max-width: 1200px;
  margin: 0em 0em 0em 7%;
}
</style>
