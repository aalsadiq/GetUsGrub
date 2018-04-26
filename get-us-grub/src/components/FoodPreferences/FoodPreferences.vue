<template>
  <div>
    <v-card>
      <v-card-title class="form-component">
        <h3 v-if="!isChecklist">{{ message }}</h3>
        <h3 v-else>{{ editMessage }}</h3>
      </v-card-title>
      <v-expand-transition>
        <v-card-text v-show="!isChecklist">
          <div class="form-component" style="padding-bottom: 1em">
            <h4 v-if="this.currentFoodPreferences.length == 0">{{ emptyPreferencesMessage }}</h4>
            <li v-else v-for="preference in currentFoodPreferences" :key='preference'>{{ preference }}</li>
          </div>
          <div class="btn-divider" v-show="isEdit">
            <v-btn :dark="true" @click.native="toggleEdit">Edit Food Preferences</v-btn>
          </div>
        </v-card-text>
      </v-expand-transition>
      <v-expand-transition>
        <v-card-text v-show="isChecklist">
          <div class="form-component">
              <v-checkbox
                v-for="preference in $store.state.constants.foodPreferences"
                :key='preference'
                :label='preference'
                :value='preference'
                :change=checkChanges
                v-model="updatedFoodPreferences"
                style="display: inline-block"
                hide-details
              >
              </v-checkbox>
          </div>
          <div class="btn-divider">
            <v-btn :dark=true @click.native="cancel">Cancel</v-btn>
            <v-btn :dark=!isDisabled :disabled=isDisabled @click.native="update">Save</v-btn>
          </div>
        </v-card-text>
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
    errors: [],
    isChecklist: false,
    isDisabled: true
  }),
  watch: {
    updatedFoodPreferences () {
      this.isDisabled = this.checkChanges()
    }
  },
  methods: {
    toggleEdit: function () {
      this.isChecklist = !this.isChecklist
    },
    checkChanges: function () {
      var current = this.currentFoodPreferences
      var updated = this.updatedFoodPreferences
      if (current.length !== updated.length) {
        return false
      } else {
        for (var item in current) {
          if (!updated.includes(current[item])) {
            return false
          }
        }
        return true
      }
    },
    cancel: function () {
      this.toggleEdit()
      this.updatedFoodPreferences = this.currentFoodPreferences
    },
    update: function () {
      axios.post(this.$store.state.urls.foodPreferences.editPreferences,
        {
          'foodPreferences': this.updatedFoodPreferences
        },
        {
          headers: {
            'Access-Control-Allow-Origin': this.$store.state.headers.accessControlAllowOrigin,
            'Authorization': `Bearer ${this.$store.state.authenticationToken}`
          }
        }).then(response => {
        console.log(response.data)
        this.response = response.data
      }).catch(error => {
        Promise.reject(error)
      }).then(this.currentFoodPreferences = this.updatedFoodPreferences.sort())
        .then(this.toggleEdit)
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
    axios.get(this.$store.state.urls.foodPreferences.getPreferences, {
      headers: {
        'Access-Control-Allow-Origin': this.$store.state.headers.accessControlAllowOrigin,
        'Authorization': `Bearer ${this.$store.state.authenticationToken}`
      },
      params: {
        username: jwt.decode(this.$store.state.authenticationToken).Username
      }
    }).then(response => {
      console.log(response.data)
      this.currentFoodPreferences = response.data.sort()
      this.updatedFoodPreferences = response.data.sort()
    }).catch(error => {
      Promise.reject(error)
    })
  }
}
</script>

<style scoped>
  @import url('https://fonts.googleapis.com/css?family=Open+Sans');

  h3 {
    font-weight: bold;
    font-family: 'Open Sans', sans-serif;
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
