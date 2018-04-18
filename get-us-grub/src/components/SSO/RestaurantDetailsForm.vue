<template>
  <div>
    <v-form v-model="validRestaurantDetailsInput">
      <v-flex xs12>
        <v-select
          :items="$store.state.constants.foodTypes"
          item-text="type"
          item-value="type"
          v-model="restaurantProfile.details.foodType"
          label="Select a food type associated with your restaurant"
          single-line
          auto
          prepend-icon="restaurant"
          hide-details
          :rules="$store.state.rules.foodTypeRules"
          required
          :disabled=disable
          ></v-select>
      </v-flex>
      <v-flex xs12>
        <v-select
          :items="$store.state.constants.avgFoodPrices"
          item-text="price"
          item-value="id"
          v-model="restaurantProfile.details.avgFoodPrice"
          label="Select average food price"
          single-line
          auto
          prepend-icon="money"
          hide-details
          :rules="$store.state.rules.avgFoodPriceRules"
          required
          :disabled=disable
          ></v-select>
      </v-flex>
      <v-flex xs12>
        <v-select
          label="Select a food preference"
          :items="$store.state.constants.foodPreferences"
          v-model="foodPreferences"
          multiple
          chips
          prepend-icon="done"
          persistent-hint
          :rules="$store.state.rules.foodPreferenceRules"
          required
          :disabled=disable
          ></v-select>
      </v-flex>
    </v-form>
  </div>
</template>

<script>
export default {
  name: 'RestaurantDetails',
  components: { },
  props: [
      'restaurantProfile',
      'foodPreferences'
  ]
}
</script>
