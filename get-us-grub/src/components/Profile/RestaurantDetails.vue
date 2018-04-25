<template>
<div id="restaurant-details-div">
  <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-toolbar color="blue" dark>
            <v-spacer/>
            <v-toolbar-title id="details-toolbar-title">Details</v-toolbar-title>
            <v-spacer/>
          </v-toolbar>
          <v-list two-line>
            <div v-for="(storeDetail, storeIndex) in storeDetails" :key="storeIndex">
            <div v-for="(detailProperty, index) in Object.keys(details)" :key="index" v-if="storeDetail.property === detailProperty">
              <v-list-tile
                avatar
                ripple
                @click="toggle()"
              >
                <v-list-tile-content>
                  <v-list-tile-title>{{ storeDetail.displayString }}: {{ details[detailProperty] }}</v-list-tile-title>
                </v-list-tile-content>
              <div v-if="isEdit">
                <v-list-tile-action v-if="detailProperty === 'avgFoodPrice'">
                  <v-select
                    :items="$store.state.constants.avgFoodPrices"
                    item-text="price"
                    item-value="id"
                    v-model="details[detailProperty]"
                    label="Select average food price"
                    single-line
                    auto
                    prepend-icon="money"
                    hide-details
                    :rules="$store.state.rules.avgFoodPriceRules"
                  ></v-select>
                </v-list-tile-action>
                <v-list-tile-action v-else-if="detailProperty === 'foodType'">
                <v-select
                  :items="$store.state.constants.foodTypes"
                  item-text="type"
                  item-value="type"
                  v-model="details[detailProperty]"
                  label="Select a food type associated with your restaurant"
                  single-line
                  auto
                  prepend-icon="restaurant"
                  hide-details
                  :rules="$store.state.rules.foodTypeRules"
                ></v-select>
                </v-list-tile-action>
                <v-list-tile-action v-else>
                  <v-switch
                    v-model="details[detailProperty]">
                  </v-switch>
                </v-list-tile-action>
              </div>
              </v-list-tile>
              <v-divider v-if="index !== details.length" :key="detailProperty"></v-divider>
            </div>
            </div>
          </v-list>
        </v-card>
      </v-flex>
    </v-layout>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  props: [
    'details',
    'isEdit'
  ],
  data () {
    return {
    }
  },
  computed: mapState({
    // Mapping states to local variables from the Vuex store
    storeDetails: state => state.constants.restaurantDetails
  }),
  methods: {
    toggle () {}
  }
}
</script>

<style>
#details-toolbar-title {
  margin: auto;
}
</style>
