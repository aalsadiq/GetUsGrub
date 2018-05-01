<template>
  <div>
    <v-card id="card-component">
      <v-toolbar color="teal" dark>
        <v-spacer>
          <v-card-title class="form-component">
            <h3 v-if="!isEdit">{{ message }}</h3>
            <h3 v-else>{{ editMessage }}</h3>
          </v-card-title>
        </v-spacer>
      </v-toolbar>
      <v-expand-transition>
        <div v-show="!isEdit">
          <v-card-text>
            <div class="form-component" style="padding-bottom: 1em">
              <h4 v-if="this.currentFoodPreferences.length == 0">{{ emptyPreferencesMessage }}</h4>
              <li v-else v-for="preference in currentFoodPreferences" :key='preference'>{{ preference }}</li>
            </div>
          </v-card-text>
        </div>
      </v-expand-transition>
      <v-expand-transition>
        <div v-show="isEdit">
          <v-card-text>
            <div class="form-component">
              <v-checkbox
                v-for="preference in $store.state.constants.foodPreferences"
                :key='preference'
                :label='preference'
                :value='preference'
                v-model="updatedFoodPreferences"
                style="display: inline-block"
                hide-details
              >
              </v-checkbox>
            </div>
          </v-card-text>
        </div>
      </v-expand-transition>
    </v-card>
  </div>
</template>

<script>
import axios from 'axios'
import jwt from 'jsonwebtoken'

export default {
  name: 'FoodPreferences',
  props: [
    'isEdit'
  ],
  data: () => ({
    message: 'Here contains your list of dietary preferences.',
    editMessage: 'Update your dietary preferences by hitting save.',
    emptyPreferencesMessage: 'Your list is currently empty. Be sure to add in preferences by hitting edit!',
    updatedFoodPreferences: [],
    currentFoodPreferences: [],
    errors: []
  }),
  methods: {
    getFoodPreferences: function () {
      axios.get(this.$store.state.urls.foodPreferences.getPreferences, {
        headers: {
          'Authorization': `Bearer ${this.$store.state.authenticationToken}`
        },
        params: {
          username: jwt.decode(this.$store.state.authenticationToken).Username
        }
      }).then(response => {
        this.currentFoodPreferences = response.data.sort()
        this.updatedFoodPreferences = response.data.sort()
      }).catch(error => {
        Promise.reject(error)
      })
    },
    update: function () {
      axios.post(this.$store.state.urls.foodPreferences.editPreferences,
        {
          'foodPreferences': this.updatedFoodPreferences
        },
        {
          headers: {
            'Authorization': `Bearer ${this.$store.state.authenticationToken}`
          }
        }).then(response => {
        this.response = response.data
      }).catch(error => {
        Promise.reject(error)
      }).then(this.getFoodPreferences())
    }
  },
  beforeCreate () {
    if (this.$store.state.authenticationToken === null) {
      this.$router.push({path: '/Unauthorized'})
    }
    try {
      if (jwt.decode(this.$store.state.authenticationToken).ReadPreferences === 'True') {
      } else {
        this.$router.push({path: '/Forbidden'})
      }
    } catch (ex) {
      this.$router.push({path: '/Forbidden'})
    }
  },
  created () {
    this.getFoodPreferences()
  }
}
</script>

<style scoped>
  @import url('https://fonts.googleapis.com/css?family=Open+Sans');

  h3 {
    font-weight: bold;
    font-family: 'Open Sans', sans-serif;
  }
  #card-component {
    display: inline-block;
  }
  .form-component {
    display: inline-block;
  }
  .btn-divider {
    padding-top: 1em;
  }
  li {
    font-family: 'Open Sans', sans-serif;
    font-size: 1.1em;
    text-align: left;
    list-style-type: circle;
  }
</style>
