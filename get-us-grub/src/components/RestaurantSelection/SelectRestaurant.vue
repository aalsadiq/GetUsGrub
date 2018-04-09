<template>
  <div>
    <div>
      <v-container fluid>
        <div>
          <div v-show="showSection">
            <v-alert id="unableToFindRestaurantAlert" icon="new_releases" class="text-xs-center" :value=showAlert>
              Unable to find a restaurant that meets your selection criteria
            </v-alert>
            <v-alert id="selectRestaurantTitleBar" :value=showRestaurantTitleBar>
              "With great power comes great responsibility" - Uncle Ben
            </v-alert>
          </div>
        </div>
        <div v-show="!showSection">
          <restaurant-selection-result/>
        </div>
        <div v-show="showSection">
          <v-card id="card">
          <v-form v-model="valid" ref="form">
            <v-layout row justify-space-between>
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
              <v-subheader>PRICE*</v-subheader>
              <v-flex xs6>
                <v-radio-group :value="$store.state.restaurantSelection.request.avgFoodPrice" v-model.number="$store.state.restaurantSelection.request.avgFoodPrice" row>
                  <v-radio label="$0 - $10" :value="1"></v-radio>
                  <v-radio label="$10 - $50" :value="2"></v-radio>
                  <v-radio label="$50+" :value="3"></v-radio>
                </v-radio-group>
              </v-flex>
              <v-spacer/>
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
import RestaurantSelectionResult from './RestaurantSelectionResult'

export default {
  components: {
    RestaurantSelectionResult
  },
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
    loader () {
      const l = this.loader
      this[l] = !this[l]

      setTimeout(() => (this[l] = false), 2000)

      this.loader = null
    }
  },
  computed: mapState({
    showSection: state => state.restaurantSelection.showRestaurantSelectionSection
  }),
  methods: {
    submit () {
      this.valid = false
      this.loader = 'loading'
      // this.loader()
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
          this.$store.dispatch('updateShowSelectedRestaurant', false)
          this.showAlert = false
          this.valid = true
          this.$store.dispatch('setSelectedRestaurant', response.data)
          this.showRestaurantTitleBar = true
        } else {
          this.showAlert = true
          this.showRestaurantTitleBar = false
          this.valid = !this.valid
          console.log(response.data)
        }
      }).catch(error => {
        // TODO: @Jenn Figure out how to handle the error [-Jenn]
        console.log(error.reponse)
        this.$router.push('GeneralError')
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
</style>
