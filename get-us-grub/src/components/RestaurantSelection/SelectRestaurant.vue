<template>
  <div>
    <div v-show="!showSelectionPage">
      <restaurant-selection-response/>
    </div>
    <div>
      <v-container fluid v-show="showSelectionPage">
        <div>
          <v-alert id="unableToFindRestaurantAlert" icon="new_releases" class="text-xs-center" :value=showAlert>
            Unable to find a restaurant that meets your selection criteria
          </v-alert>
          <v-alert id="selectRestaurantTitleBar" :value=!showAlert>
            "With great power comes great responsibility" - Uncle Ben
          </v-alert>
        </div>
        <v-form v-model="valid" ref="form">
          <v-layout row justify-space-between>
            <v-flex xs6>
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
              <!-- <v-btn-toggle mandatory v-model="$store.state.restaurantSelection.request.distance">
                  <font color="black" flat><b>Distance</b></font>
                <v-btn flat>
                  1
                </v-btn>
                <v-btn flat>
                  5
                </v-btn>
                <v-btn flat>
                  10
                </v-btn>
                <v-btn flat>
                  15
                </v-btn>
                <font color="black" flat><b>mi</b></font>
              </v-btn-toggle> -->
            </v-flex>
          </v-layout>
          <v-btn
            @click="submit"
            :disabled="!valid"
            color="primary"
            :loading="loading"
            >
            <v-icon dark>search</v-icon>
          </v-btn>
        </v-form>
      </v-container>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import RestaurantSelectionResponse from './RestaurantSelectionResponse'

export default {
  components: {
    RestaurantSelectionResponse
  },
  data () {
    return {
      showSelectionPage: true,
      showSelectionResponsePage: false,
      valid: false,
      showAlert: false,
      loader: null,
      loading: false
    }
  },
  watch: {
    loader () {
      const l = this.loader
      this[l] = !this[l]

      setTimeout(() => (this[l] = false), 3000)

      this.loader = null
    }
  },
  methods: {
    submit () {
      this.valid = !this.valid
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
          this.showAlert = true
          this.showSelectionPage = false
          this.$store.dispatch('setSelectedRestaurant', response.data)
        }
        this.valid = !this.valid
        console.log(response.data)
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
  background-color: #b75959 !important
}
</style>
