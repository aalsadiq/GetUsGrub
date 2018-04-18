<template>
  <div>
    <v-container class="scroll-y" id="scroll-target">
      <v-container id="restaurant-selection-registered-user">
        <app-header/>
        <v-container id="select-restaurant">
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
                  <p v-for="preference in foodPreferences" :key="preference">
                    {{ preference }}
                  </p>
                </v-card>
              </v-flex>
            </v-layout>
          </div>
          <!-- Main page of the restaurant selector -->
          <select-restaurant/>
        </v-container>
        <app-footer/>
      </v-container>
    </v-container>
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
      this.$router.push({path: '/Unauthorized'})
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadRestaurantSelection === 'True') {
      } else {
        this.$router.push({path: '/Forbidden'})
      }
    } catch (ex) {
      this.$router.push({path: '/Forbidden'})
    }
  },
  created () {
    axios.get('http://localhost:8081/FoodPreferences/GetPreferences', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      params: {
        username: jwt.decode(this.$store.state.authenticationToken).Username
      }
    }).then(response => {
      this.foodPreferences = response.data
    }).catch(error => {
      Promise.reject(error)
    })
  }
}
</script>

<style>
#scroll-target {
  max-height: 56.5em;
  position: absolute;
  overflow-x: hidden;
}
#restaurant-selection-registered-user {
  max-width: 1200px;
  margin: auto;
}
#select-restaurant {
  margin-top: 2em;
}
#food-preferences {
  margin: 0em 2.4em 0em 1.2em;
}
</style>
