<template>
  <v-content>
      <div id="registered-restaurant-selection">
        <div id="food-preferences">
          <!-- Title bar for the Food Preferences -->
          <v-alert id="food-preferences-alert" :value=true>
            <span id="preferences-title">
              Your Food Preferences
            </span>
          </v-alert>
            <v-card>
              <p v-if="foodPreferences || foodPreferences.length > 0" v-for="preference in foodPreferences" :key="preference">
                {{ preference }}
              </p>
              <p v-if="!foodPreferences || foodPreferences.length === 0">
                You currently have no food preferences.
              </p>
            </v-card>
        </div>
        <!-- Main page of the restaurant selector -->
        <select-restaurant/>
      </div>
  </v-content>
</template>

<script>
import SelectRestaurant from './SelectRestaurant'
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import jwt from 'jsonwebtoken'
import axios from 'axios'

export default {
  name: 'RestaurantSelectionRegisteredUserMain',
  // Adding Vue components dependencies
  components: {
    AppHeader,
    AppFooter,
    SelectRestaurant
  },
  data () {
    return {
      foodPreferences: null
    }
  },
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push('Unauthorized')
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantSelection === 'True') {
      } else {
        this.$router.push('Forbidden')
      }
    } catch (ex) {
      this.$router.push('Forbidden')
    }
  },
  created () {
    axios.get(this.$store.state.urls.foodPreferences.getPreferences, {
      headers: {
        'Authorization': `Bearer ${this.$store.state.authenticationToken}`
      },
      params: {
        username: jwt.decode(this.$store.state.authenticationToken).Username
      }
    }).then(response => {
      this.foodPreferences = response.data.sort()
    }).catch(error => {
      Promise.reject(error)
    })
  }
}
</script>

<style scoped>
#registered-restaurant-selection {
  max-width: 1200px;
  margin: auto;
}
#preferences-title {
  font-weight: bold;
  font-size: 1em;
}
#food-preferences-alert {
  background-color: rgb(2, 53, 112) !important;
}
</style>
