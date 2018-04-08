<template>
  <div id="restaurant-selection-response">
    <h2>Restaurant Name:</h2>
    <p id="display-name">
      {{ restaurant.displayName }}
    </p>
    <h3>Phone Number:</h3>
    <p>
      {{ restaurant.phoneNumber }}
    </p>
    <h3>Address:</h3>
    <p>
      {{ restaurant.address.street1 }}
      {{ restaurant.address.street2 }}
      {{ restaurant.address.city }}, {{ restaurant.address.state }} {{ restaurant.address.zip }}
    </p>
    <h3>BusinessHours:</h3>
    <p v-for="businessHour in restaurant.businessHours" :key="businessHour.day">
      {{ businessHour.day }}:
      {{ businessHour.openTime }}
      {{ businessHour.closeTime }}
    </p>
    <v-flex xs12>
      <v-btn @click="submit" color="primary">
        rerun
      </v-btn>
      <v-btn @click="confirmRestaurant" color="primary">
        confirm restaurant
      </v-btn>
    </v-flex>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import axios from 'axios'

export default {
  computed: mapState({
    restaurant: state => state.restaurantSelection.selectedRestaurant
  }),
  methods: {
    // Submits a GET request to the backend Web API end point
    submit () {
      axios.get('http://localhost:8081/RestaurantSelection/Unregistered/', {
        params: {
          foodType: this.$store.state.restaurantSelection.request.foodType.type,
          city: this.$store.state.restaurantSelection.request.city,
          state: this.$store.state.restaurantSelection.request.state.abbreviation,
          distanceInMiles: this.$store.state.restaurantSelection.request.distance,
          avgFoodPrice: this.$store.state.restaurantSelection.request.avgFoodPrice
        }
      }).then(response => {
        this.$store.dispatch('setSelectedRestaurant', response.data)
      }).catch(error => {
        console.log(error)
        this.$router.push('GeneralError')
      })
    },
    confirmRestaurant () {
      this.$store.state.restaurantSelection.selectedRestaurant.isConfirmed = true
      // TODO: @Brian Will need to redirect to your directions page [-Jenn]
      // this.$router.push('DirectionsPage')
    }
  }
}
</script>

<style>
#restaurant-selection-response {
  margin: 0 0 7em 0;
}

#display-name {
  font-size: 1.3em;
}
</style>
