<template>
  <div>
    <div v-show="!showSelectionPage">
      <restaurant-selection-response/>
    </div>
    <div>
      <div v-show="showAlert">
        <v-alert type="orange" icon="new_releases" class="text-xs-center">
          Unable to find a restaurant that meets your selection criteria
        </v-alert>
      </div>
      <div v-show="!showAlert">
        <v-alert type="blue-grey" :value="true">
          This is a success alert.
        </v-alert>
      </div>
      <v-container fluid v-show="showSelectionPage">
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
            <v-subheader>Select the distance</v-subheader>
            <v-flex xs6>
              <v-radio-group v-model="$store.state.restaurantSelection.request.distance" row required>
                <v-radio label="1" value="1"></v-radio>
                <v-radio label="5" value="5"></v-radio>
                <v-radio label="10" value="10"></v-radio>
                <v-radio label="15" value="15"></v-radio>
              </v-radio-group>
            </v-flex>
            <v-spacer/>
            <v-subheader>Select the average food price</v-subheader>
            <v-flex xs6>
              <v-radio-group v-model="$store.state.restaurantSelection.request.avgFoodPrice" row required>
                <v-radio label="$" value="1"></v-radio>
                <v-radio label="$$" value="2"></v-radio>
                <v-radio label="$$$" value="3"></v-radio>
              </v-radio-group>
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
      this.loader()
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
          this.showAlert = !this.showAlert
          this.showSelectionPage = !this.showSelectionPage
          this.$store.dispatch('setSelectedRestaurant', response.data)
        }
        this.showAlert = !this.showAlert
        this.valid = !this.valid
      }).catch(error => {
        this.valid = !this.valid
        // TODO: @Jenn Figure out how to handle the error [-Jenn]
        console.log(error.reponse)
        // this.$router.push('GeneralError')
      })
    }
  }
}
</script>
