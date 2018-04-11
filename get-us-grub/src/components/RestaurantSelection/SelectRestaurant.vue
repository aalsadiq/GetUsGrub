<template>
  <div>
    <div>
      <v-container fluid>
        <div v-show="showSection">
          <!-- Alert for when there is no restaurant avaialble within user's selection criteria -->
          <v-alert id="unableToFindRestaurantAlert" icon="new_releases" class="text-xs-center" :value=showAlert>
            Unable to find a restaurant that meets your selection criteria
          </v-alert>
          <!-- Title bar for the restaurant selection -->
          <v-alert id="selectRestaurantTitleBar" :value=showRestaurantTitleBar>
            <span id="quote">
            "With great power comes great responsibility" - Uncle Ben
            </span>
          </v-alert>
        </div>
        <div v-show="!showSection">
          <!-- Restaurant selection results Vue component -->
          <result/>
        </div>
        <div v-show="showSection">
          <v-card id="card">
          <v-form v-model="valid" ref="form">
            <v-layout row justify-space-between>
              <!-- Food types drop down menu -->
              <v-flex xs6 sm6>
                <v-select
                  :items="$store.state.constants.foodTypes"
                  v-model="$store.state.restaurantSelection.request.foodType"
                  item-text="type"
                  label="Select a food type"
                  :rules="$store.state.rules.foodTypeRules"
                  autocomplete
                  required
                ></v-select>
              </v-flex>
              <!-- City text field -->
              <v-flex xs4>
              <v-text-field
                label="Enter a city"
                v-model="$store.state.restaurantSelection.request.city"
                hint="City"
                persistent-hint
                :rules="$store.state.rules.addressCityRules"
                required
              ></v-text-field>
              </v-flex>
              <!-- States drop down menu -->
              <v-flex xs1>
                <v-select
                  :items="$store.state.constants.states"
                  v-model="$store.state.restaurantSelection.request.state"
                  item-text="abbreviation"
                  label="State"
                  hint="State"
                  persistent-hint
                  :rules="$store.state.rules.addressStateRules"
                  autocomplete
                  required
                ></v-select>
              </v-flex>
            </v-layout>
            <v-layout row justify-space-between>
              <!-- Prices radio buttons -->
              <v-subheader>PRICE*</v-subheader>
              <v-flex xs6>
                <v-radio-group :value="$store.state.restaurantSelection.request.avgFoodPrice" v-model.number="$store.state.restaurantSelection.request.avgFoodPrice" row>
                  <v-radio label="$0 - $10" :value="1"></v-radio>
                  <v-radio label="$10 - $50" :value="2"></v-radio>
                  <v-radio label="$50+" :value="3"></v-radio>
                </v-radio-group>
              </v-flex>
              <v-spacer/>
              <!-- Distance radio buttons -->
              <v-subheader>DISTANCE* (miles)</v-subheader>
              <v-flex xs5>
                <v-radio-group :value="$store.state.restaurantSelection.request.avgFoodPrice" v-model.number="$store.state.restaurantSelection.request.distance" row>
                  <v-radio label="1" :value="1"></v-radio>
                  <v-radio label="5" :value="5"></v-radio>
                  <v-radio label="10" :value="10"></v-radio>
                  <v-radio label="15" :value="15"></v-radio>
                </v-radio-group>
              </v-flex>
            </v-layout>
            </v-form>
          </v-card>
          <!-- Submit button -->
          <v-tooltip bottom>
            <v-btn
              id="search-btn"
              @click="submit"
              :disabled="!valid"
              :loading="loading"
              slot="activator"
              >
              <v-icon>search</v-icon>
            </v-btn>
            <span>Search</span>
          </v-tooltip>
        </div>
      </v-container>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'
import axios from 'axios'
import Result from './Result'

export default {
  // Vue component dependencies
  components: {
    Result
  },
  // Local variable data
  data () {
    return {
      valid: false,
      showAlert: false,
      showRestaurantTitleBar: true,
      loader: null,
      loading: false
    }
  },
  watch: {
    // Loading animation on buttons
    loader () {
      const l = this.loader
      this[l] = !this[l]

      setTimeout(() => (this[l] = false), 2000)

      this.loader = null
    }
  },
  // Mapping states to local variables from the Vuex store
  computed: mapState({
    showSection: state => state.restaurantSelection.showRestaurantSelectionSection
  }),
  methods: {
    // Submitting information to the backend
    submit () {
      this.valid = false
      this.loader = 'loading'
      // Sending GET Request
      axios.get('http://localhost:8081/RestaurantSelection/Unregistered/', {
        // Paramaters for URL queries
        params: {
          foodType: this.$store.state.restaurantSelection.request.foodType.type,
          city: this.$store.state.restaurantSelection.request.city,
          state: this.$store.state.restaurantSelection.request.state.abbreviation,
          distanceInMiles: this.$store.state.restaurantSelection.request.distance,
          avgFoodPrice: this.$store.state.restaurantSelection.request.avgFoodPrice
        }
      // Receiving successful response
      }).then(response => {
        if (response.data != null) {
          this.showAlert = false
          this.valid = true
          this.showRestaurantTitleBar = true
          this.$store.dispatch('updateShowSelectedRestaurant', false)
          this.$store.dispatch('setSelectedRestaurant', response.data)
        } else {
          this.showAlert = true
          this.showRestaurantTitleBar = false
          this.valid = true
        }
      // Receiving an unsuccessful response
      }).catch(error => {
        this.valid = true
        // Route to the General Error page
        this.$router.push('GeneralError')
        Promise.reject(error)
      })
    }
  }
}
</script>

<style>
#selectRestaurantTitleBar {
  background-color: #6F81AD !important
}
#unableToFindRestaurantAlert {
  background-color: #e26161 !important
}
#search-btn {
  background-color: rgb(255, 255, 255);
}
#card {
  padding: 0 0.7em 0 0.7em;
  margin: 0 0 1em 0;
}
#quote {
  color: rgb(255, 255, 255);
  font-size: normal;
}
</style>
