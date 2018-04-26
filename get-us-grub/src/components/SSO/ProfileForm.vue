<template>
  <div>
    <v-form v-model="value.isValidRestaurantDetails" v-on:input="$emit('input', value)">
      <v-form v-model="value.isValidUserDetails">
        <v-flex xs12>
        <v-text-field
          :label="getDisplayLabel(type)"
          v-model="value.userProfile.displayName"
          :rules="$store.state.rules.displayNameRules"
          required
          :disabled="disabled"
          ></v-text-field>
        </v-flex>
      </v-form>
      <div v-if='type === "restaurant"'>
        <v-flex xs12>
          <v-select
          :items="$store.state.constants.foodTypes"
          item-text="type"
          item-value="type"
          v-model="value.restaurantProfile.details.foodType"
          label="Select a food type associated with your restaurant"
          prepend-icon="restaurant"
          :rules="$store.state.rules.foodTypeRules"
          required
          hide-details
          single-line
          auto
          :disabled="disabled"
          ></v-select>
        </v-flex>
        <v-flex xs12>
          <v-select
          :items="$store.state.constants.avgFoodPrices"
          item-text="price"
          item-value="id"
          v-model="value.restaurantProfile.details.avgFoodPrice"
          label="Select average food price"
          prepend-icon="money"
          :rules="$store.state.rules.avgFoodPriceRules"
          required
          hide-details
          single-line
          auto
          :disabled="disabled"
          ></v-select>
        </v-flex>
        <v-flex xs12>
          <v-select
          label="Select a food preference"
          :items="$store.state.constants.foodPreferences"
          v-model="value.foodPreferences"
          multiple
          chips
          prepend-icon="done"
          persistent-hint
          :rules="$store.state.rules.foodPreferenceRules"
          required
          :disabled="disabled"
          ></v-select>
        </v-flex>
      </div>
    </v-form>
  </div>
</template>

<script>
export default {
  name: 'ProfileForm',
  components: { },
  props: [
    'value',
    'type',
    'disabled'
  ],
  methods: {
    getDisplayLabel (type) {
      if (type === 'restaurant') {
        return 'Enter your restaurant\'s name'
      } else {
        return 'Enter a display name'
      }
    }
  }
}
</script>
