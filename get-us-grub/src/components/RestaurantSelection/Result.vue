<template>
  <div id="restaurant-selection-response">
    <!-- Title bar for the restaurant selection -->
    <v-alert id="result-bar" :value=showResultBar>
      <span id="quote">
        Result
      </span>
    </v-alert>
    <v-card id="card-result">
      <v-layout row justify-space-between>
        <!-- Selected restaurant information displayed here -->
        <v-flex xs12>
          <h2>Restaurant Name:</h2>
          <p id="display-name">
            {{ restaurant.displayName }}
          </p>
          <h3>Phone Number:</h3>
          <p>
            {{ restaurant.phoneNumber }}
          </p>
          <h3>Address:</h3>
          <p v-if='restaurant.address.street2 === ""'>
            {{ restaurant.address.street1 }},
            {{ restaurant.address.city }}, {{ restaurant.address.state }} {{ restaurant.address.zip }}
          </p>
          <p v-if='restaurant.address.street2 !== ""'>
            {{ restaurant.address.street1 }},
            {{ restaurant.address.street2 }},
            {{ restaurant.address.city }}, {{ restaurant.address.state }} {{ restaurant.address.zip }}
          </p>
          <h3>BusinessHours:</h3>
          <p v-for="businessHour in restaurant.businessHours" :key="businessHour.day">
            {{ businessHour.day }}:
            {{ businessHour.twelveHourFormatOpenTime }} -
            {{ businessHour.twelveHourFormatCloseTime }}
          </p>
        </v-flex>
        <!-- Google embedded map displayed here -->
        <v-flex xs6>
          <google-embed-map/>
        </v-flex>
      </v-layout>
    </v-card>
    <v-flex xs12>
      <!-- Confirm that user will be going to this restaurant -->
      <v-btn @click="confirmRestaurant" color="teal lighten-2" :disabled="!this.responseValid">
        <span class="btn-text">
          CONFIRM
        </span>
      </v-btn>
    </v-flex>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import GoogleEmbedMap from '@/components/EmbedMap/GoogleEmbedMap'

export default {
  // Vue component dependencies
  components: {
    GoogleEmbedMap
  },
  // Local variable data
  data () {
    return {
      responseValid: true,
      loader: null,
      loading: false,
      showAlert: false,
      showResultBar: true
    }
  },
  computed: mapState({
    // Mapping states to local variables from the Vuex store
    restaurant: state => state.restaurantSelection.selectedRestaurant
  }),
  methods: {
    // Route to bill splitter and set isConfirmed variable in store to true
    confirmRestaurant () {
      this.$store.state.restaurantSelection.selectedRestaurant.isConfirmed = true
      this.$router.push({path: '/RestaurantBillSplitter'})
    }
  }
}
</script>

<style>
#restaurant-selection-response {
  margin: 1em 0.7em 7em 0em;
}
#result-bar {
  background-color: #e49f9f !important
}
#display-name {
  font-size: 1.3em;
}
#card-result {
  padding: 1em 1em 0.5em 1em;
  margin: 0 0 1em 0;
}
</style>
