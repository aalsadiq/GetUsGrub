<template>
  <div>
    <div fluid>
      <div>
        <!-- Alert for when there is no restaurant avaialble within user's selection criteria -->
        <v-alert id="unableToFindRestaurantAlert" icon="new_releases" class="text-xs-center" :value=showAlert>
          Unable to find a restaurant that meets your selection criteria
        </v-alert>
        <!-- Title bar for the restaurant selection -->
        <v-alert id="selectRestaurantTitleBar" :value=showRestaurantTitleBar>
          <span class="quote">
          "With great power comes great responsibility" - Uncle Ben
          </span>
        </v-alert>
      </div>
      <div>
        <v-card id="card" v-resize="onResize">
          <v-form v-model="valid" ref="form">
            <v-layout row justify-space-around>
              <!-- Food types drop down menu -->
              <v-flex xs5>
                <v-select
                  :items="$store.state.constants.foodTypes"
                  v-model="$store.state.restaurantSelection.request.foodType"
                  item-text="type"
                  label="Food type"
                  hint="Food type"
                  persistent-hint
                  :rules="$store.state.rules.foodTypeRules"
                  autocomplete
                  required
                  :disabled=disable
                ></v-select>
              </v-flex>
              <!-- City text field -->
              <v-flex xs4>
              <v-text-field
                v-model="$store.state.restaurantSelection.request.city"
                label="City"
                hint="City"
                persistent-hint
                :rules="$store.state.rules.addressCityRules"
                required
                :disabled=disable
              ></v-text-field>
              </v-flex>
              <!-- States drop down menu -->
              <v-flex xs2>
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
                  :disabled=disable
                ></v-select>
              </v-flex>
            </v-layout>
            <v-layout row justify-space-around>
              <!-- Prices radio buttons -->
              <v-flex xs3>
              <v-subheader>PRICE*</v-subheader>
                <v-radio-group
                  :value="$store.state.restaurantSelection.request.avgFoodPrice"
                  v-model.number="$store.state.restaurantSelection.request.avgFoodPrice"
                  :row="windowSize.width > mobileScreenSize"
                  >
                  <v-radio label="$0 - $10" :value="1" :disabled=disable></v-radio>
                  <v-radio label="$10 - $50" :value="2" :disabled=disable></v-radio>
                  <v-radio label="$50+" :value="3" :disabled=disable></v-radio>
                </v-radio-group>
              </v-flex>
              <!-- Distance radio buttons -->
              <v-flex xs3>
              <v-subheader>MAX DISTANCE* (miles)</v-subheader>
                <v-radio-group
                  :value="$store.state.restaurantSelection.request.avgFoodPrice"
                  v-model.number="$store.state.restaurantSelection.request.distance"
                  :disabled=disable
                  :row="windowSize.width > mobileScreenSize"
                  >
                  <v-radio label="1" :value="1" :disabled=disable></v-radio>
                  <v-radio label="5" :value="5" :disabled=disable></v-radio>
                  <v-radio label="10" :value="10" :disabled=disable></v-radio>
                  <v-radio label="15" :value="15" :disabled=disable></v-radio>
                </v-radio-group>
              </v-flex>
            </v-layout>
          </v-form>
        </v-card>
        <!-- Submit button -->
        <div class="search-btn-div">
          <v-tooltip bottom>
            <v-btn
              class="search-btn"
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
        <result-bar/>
      </div>
    </div>
    <div v-show="showSection">
      <!-- Restaurant selection results Vue component -->
      <result/>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import Result from '@/components/RestaurantSelection/Result'
import ResultBar from '@/components/RestaurantSelection/ResultBar'

export default {
  // Vue component dependencies
  components: {
    Result,
    ResultBar
  },
  // Local variable data
  data () {
    return {
      valid: false,
      showAlert: false,
      showRestaurantTitleBar: true,
      loader: null,
      loading: false,
      showSection: false,
      disable: false,
      windowSize: {
        width: 0,
        height: 0
      },
      mobileScreenSize: 1000
    }
  },
  watch: {
    // Loading animation on buttons
    loader () {
      const l = this.loader
      this[l] = !this[l]

      setTimeout(() => (this[l] = false), 1000)

      this.loader = null
    }
  },
  mounted () {
    this.mobileScreenWidth = this.$store.state.constants.mobileScreenWidth
    this.onResize()
  },
  methods: {
    onResize () {
      this.windowSize = { width: window.innerWidth, height: window.innerHeight }
    },
    // Submitting information to the backend
    submit () {
      this.showError = false
      this.valid = false
      this.disable = true
      this.loader = 'loading'
      this.showSection = false
      // Sending GET Request
      axios.get(this.$store.state.urls.restaurantSelection.unregisteredUser, {
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
          this.$store.dispatch('setSelectedRestaurant', response.data)
          this.showAlert = false
          this.valid = true
          this.showRestaurantTitleBar = true
          this.showSection = true
          this.disable = false
        } else {
          this.showAlert = true
          this.disable = false
          this.showRestaurantTitleBar = false
          this.valid = true
        }
      // Receiving an unsuccessful response
      }).catch(error => {
        this.valid = true
        this.disable = false
        try {
          if (error.response.status === 401) {
            // Route to Unauthorized page
            this.$router.push('Unauthorized')
          }
          if (error.response.status === 403) {
            // Route to Forbidden page
            this.$router.push('Forbidden')
          }
          if (error.response.status === 404) {
            // Route to ResourceNotFound page
            this.$router.push('ResourceNotFound')
          }
          if (error.response.status === 500) {
            // Route to InternalServerError page
            this.$router.push('InternalServerError')
          } else {
            this.errors = JSON.parse(JSON.parse(error.response.data.message))
            this.showError = true
          }
          Promise.reject(error)
        } catch (ex) {
          this.errors = error.response.data
          this.showError = true
          Promise.reject(error)
        }
      })
    }
  }
}
</script>

<style scoped>
#selectRestaurantTitleBar {
  background-color: #6F81AD !important
}
#unableToFindRestaurantAlert {
  background-color: #e26161 !important
}
#search-btn {
  background-color: rgb(255, 255, 255);
}
.quote {
  color: rgb(255, 255, 255);
  font-size: 1.1em;
  font-weight: bold;
}
#required {
  font-size: x-small;
  text-align: left;
  color: rgb(139, 139, 139);
}
</style>
