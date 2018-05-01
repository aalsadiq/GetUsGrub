<template>
<div>
  <div id="restaurant-selection-response">
    <v-card id="card-result">
      <!-- Selected restaurant information displayed here -->
      <h2>Restaurant Name:</h2>
      <p id="display-name" class="paragraph">
        {{ restaurant.displayName }}
      </p>
      <h3>Phone Number:</h3>
      <p class="paragrah">
        {{ restaurant.phoneNumber }}
      </p>
      <h3>Address:</h3>
      <p class="paragraph" v-if='restaurant.address.street2 === ""'>
        {{ restaurant.address.street1 }},
        {{ restaurant.address.city }}, {{ restaurant.address.state }} {{ restaurant.address.zip }}
      </p>
      <p class="paragraph" v-if='restaurant.address.street2 !== ""'>
        {{ restaurant.address.street1 }},
        {{ restaurant.address.street2 }},
        {{ restaurant.address.city }}, {{ restaurant.address.state }} {{ restaurant.address.zip }}
      </p>
      <h3>BusinessHours:</h3>
      <p class="paragraph">
        <span v-for="(businessHour, index) in restaurant.businessHours" :key="index">
        {{ businessHour.day }}:
        {{ businessHour.twelveHourFormatOpenTime }} -
        {{ businessHour.twelveHourFormatCloseTime }}
        <br>
        </span>
      </p>
      <div v-if="restaurant.foodPreferences !== null">
        <h3>Accommodations:</h3>
          <p class="paragraph" v-for="foodPreference in restaurant.foodPreferences" :key="foodPreference">
            {{ foodPreference }}
          </p>
      </div>
      <!-- Google embedded map displayed here -->
      <google-embed-map id="map"/>
        <!-- Confirm that user will be going to this restaurant -->
      <v-container>
      <v-btn id="confirm-btn" @click="confirmRestaurant" :disabled="!this.responseValid">
        <div class="btn-text">
          CONFIRM
        </div>
      </v-btn>
      </v-container>
    </v-card>
  </div>
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
      showAlert: false
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
      this.$router.push('RestaurantBillSplitter')
    }
  }
}
</script>

<style scoped>
#display-name {
  font-size: 1.3em;
}
#confirm-btn {
  background-color: #20b39a !important;
}
#map {
  height: 500px;
  width: 100%;
  max-height: 500px;
  max-width: 500px;
  display: inline-block;
}
</style>
