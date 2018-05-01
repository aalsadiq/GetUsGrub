<template>
  <v-content>
      <div id="unregistered-restaurant-selection">
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
      </div>
  </v-content>
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

<style scoped>
#login-redirect {
  font-weight: bold;
  font-size: small;
  color: rgb(82, 159, 247) !important;
}
#unregistered-restaurant-selection {
  max-width: 1200px;
  margin: auto;
}
</style>
