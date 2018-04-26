<template>
<div>
  <div id="restaurant-selection-response">
    <v-card id="card-result">
      <v-layout row justify-space-between>
        <!-- Selected restaurant information displayed here -->
        <v-flex xs12>
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
          <p class="paragraph" v-for="(businessHour, index) in restaurant.businessHours" :key="index">
            {{ businessHour.day }}:
            {{ businessHour.twelveHourFormatOpenTime }} -
            {{ businessHour.twelveHourFormatCloseTime }}
          </p>
          <div v-if="restaurant.foodPreferences !== null">
            <h3>Accommodations:</h3>
              <p class="paragraph" v-for="foodPreference in restaurant.foodPreferences" :key="foodPreference">
                {{ foodPreference }}
              </p>
          </div>
        </v-flex>
        <!-- Google embedded map displayed here -->
        <v-flex xs6>
          <google-embed-map/>
        </v-flex>
      </v-layout>
    </v-card>
    <v-flex xs12>
      <!-- Confirm that user will be going to this restaurant -->
      <v-btn id="confirm-btn" @click="confirmRestaurant" :disabled="!this.responseValid">
        <span class="btn-text">
          CONFIRM
        </span>
      </v-btn>
    </v-flex>
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
#card-result {
  padding: 1em 1em 0.5em 1em;
  margin: 0 0 1em 0;
}
#confirm-btn {
  margin: 0 0 3em 0;
  background-color: #20b39a !important;
}
.paragraph {
  margin: 0em 0em 0em 0em;
  padding: 0em 0em 0.2em 0em;
}
h3 {
  padding: 0.2em 0 0.3em 0;
}
</style>
