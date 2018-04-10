<template>
  <div id="restaurant-selection-response">
    <v-alert id="unableToFindRestaurantAlert" icon="new_releases" class="text-xs-center" :value=showAlert>
      Unable to find a restaurant that meets your selection criteria
    </v-alert>
    <v-card id="card-result">
      <v-layout row justify-space-between>
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
          <p>
            {{ restaurant.address.street1 }},
            {{ restaurant.address.street2 }}
            {{ restaurant.address.city }}, {{ restaurant.address.state }} {{ restaurant.address.zip }}
          </p>
          <h3>BusinessHours:</h3>
          <p v-for="businessHour in restaurant.businessHours" :key="businessHour.day">
            {{ businessHour.day }}:
            {{ businessHour.openTime }} -
            {{ businessHour.closeTime }}
          </p>
        </v-flex>
        <v-flex xs6>
          <google-embed-map/>
        </v-flex>
      </v-layout>
    </v-card>
    <v-flex xs12>
      <v-btn @click="showRestaurantSelection" color="yellow darken-3" :disabled="!this.responseValid">
        <span class="btn-text">
          NEW
        </span>
      </v-btn>
      <v-btn @click="submit" color="deep-orange darken-3" :loading="loading" :disabled="!this.responseValid">
        <span class="btn-text">
          RERUN
        </span>
      </v-btn>
      <v-btn @click="confirmRestaurant" color="cyan darken-2" :disabled="!this.responseValid">
        <span class="btn-text">
          CONFIRM
        </span>
      </v-btn>
    </v-flex>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import axios from 'axios'
import GoogleEmbedMap from '@/components/EmbedMap/GoogleEmbedMap'

export default {
  components: {
    GoogleEmbedMap
  },
  data () {
    return {
      responseValid: true,
      loader: null,
      loading: false,
      showAlert: false
    }
  },
  watch: {
    loader () {
      const l = this.loader
      this[l] = !this[l]

      setTimeout(() => (this[l] = false), 1500)

      this.loader = null
    }
  },
  computed: mapState({
    restaurant: state => state.restaurantSelection.selectedRestaurant
  }),
  methods: {
    showRestaurantSelection () {
      this.$store.dispatch('updateShowSelectedRestaurant', true)
    },
    // Submits a GET request to the backend Web API end point
    submit () {
      this.responseValid = false
      this.loader = 'loading'
      axios.get('http://localhost:8081/RestaurantSelection/Unregistered/', {
        params: {
          foodType: this.$store.state.restaurantSelection.request.foodType.type,
          city: this.$store.state.restaurantSelection.request.city,
          state: this.$store.state.restaurantSelection.request.state.abbreviation,
          distanceInMiles: this.$store.state.restaurantSelection.request.distance,
          avgFoodPrice: this.$store.state.restaurantSelection.request.avgFoodPrice
        }
      }).then(response => {
        if (response.data != null) {
          this.showAlert = false
          this.responseValid = true
          this.$store.dispatch('setSelectedRestaurant', response.data)
          this.showRestaurantTitleBar = true
        } else {
          this.showAlert = true
          this.responseValid = true
        }
      }).catch(error => {
        this.responseValid = true
        Promise.reject(error)
        this.$router.push('GeneralError')
      })
    },
    confirmRestaurant () {
      this.$store.state.restaurantSelection.selectedRestaurant.isConfirmed = true
      this.$router.push('RestaurantBillSplitter')
    }
  }
}
</script>

<style>
#restaurant-selection-response {
  margin: 0 0 7em 0;
}
#unableToFindRestaurantAlert {
  background-color: #e26161 !important
}
#display-name {
  font-size: 1.3em;
}
#card-result {
  padding: 1em 1em 0.5em 1em;
  margin: 0 0 1em 0;
}
</style>
