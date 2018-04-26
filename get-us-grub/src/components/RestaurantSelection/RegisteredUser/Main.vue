<template>
  <div>
      <div id="restaurant-selection-registered-user">
        <app-header/>
        <div id="select-restaurant">
          <div id="food-preferences">
            <v-layout>
            <v-flex xs12>
              <!-- Title bar for the Food Preferences -->
              <v-alert id="food-preferences-alert" :value=true>
                <span id="preferences-title">
                  Your Food Preferences
                </span>
              </v-alert>
            </v-flex>
            </v-layout>
            <v-layout>
              <v-flex xs12>
                <v-card>
                  <p v-if="foodPreferences || foodPreferences.length > 0" v-for="preference in foodPreferences" :key="preference">
                    {{ preference }}
                  </p>
                  <p v-if="!foodPreferences || foodPreferences.length === 0">
                    You currently have no food preferences.
                  </p>
                </v-card>
              </v-flex>
            </v-layout>
          </div>
          <!-- Main page of the restaurant selector -->
          <select-restaurant/>
        </div>
        <app-footer/>
      </div>
  </div>
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
        'Access-Control-Allow-Origin': this.$store.state.headers.accessControlAllowOrigin,
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
#restaurant-selection-registered-user {
  padding: 2.5em 0 0 0;
  max-width: 1200px;
  margin: auto;
}
#select-restaurant {
  margin-top: 2em;
}
#food-preferences {
  margin: 3em 0 0.5em 0;
  padding: 0.1em 0 0 0;
}
#preferences-title {
  font-weight: bold;
  font-size: 1em;
}
#food-preferences-alert {
  height: 2em;
  background-color: rgb(2, 53, 112) !important;
}
p {
  margin: 0 0 0.1em 0;
}
</style>
