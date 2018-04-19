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
        <div id="select-restaurant">
          <!-- Main page of the restaurant selector -->
          <select-restaurant/>
        </div>
      </v-container>
    </v-container>
  </div>
</template>

<script>
import SelectRestaurant from './SelectRestaurant'

export default {
  name: 'RestaurantSelectionUnregisteredUserMain',
  // Adding Vue components dependencies
  components: {
    SelectRestaurant
  }
}
</script>

<style>
#login-redirect {
  margin: 4em 2.6em -0.3em 1.2em;
  font-size: small;
  padding: 0.1em 0em 0.1em 1em;
  color: rgb(82, 159, 247) !important;
  background-color: rgb(255, 255, 255) !important;
}
#scroll-target {
  max-height: 56.5em;
  position: absolute;
  overflow-x: hidden;
}
#unregistered-restaurant-selection {
  max-width: 1200px;
  margin: auto;
}
#select-restaurant {
  padding-top: 1em;
}
</style>
