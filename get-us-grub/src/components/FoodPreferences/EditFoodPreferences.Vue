<template>
  <div>
    <app-header/>
      <div id="page-divider">
        <h1>{{ title }}</h1>
        <h3>{{ message }}</h3>
        <div id="form-divider">
          <v-form>
            <v-checkbox
              v-for="preference in $store.state.constants.foodPreferences"
              :key='preference'
              :label='preference'
              :value='preference'
              v-model="foodPreferences"
              style="display: inline-block"
              hide-details
            >
            </v-checkbox>
          </v-form>
        </div>
        <div>
          <router-link to="/FoodPreferences">
            <v-btn :dark=true>Cancel</v-btn>
          </router-link>
          <v-btn :dark=true @click="update">Save</v-btn>
        </div>
      </div>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'
import jwt from 'jsonwebtoken'

export default {
  name: 'EditPreferences',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  },
  data: () => ({
    title: 'Edit Your Food Preferences',
    message: 'Checked elements will be saved as your updated food preferences.',
    foodPreferences: [],
    checklist: ['Gluten-free', 'Halal', 'Kosher', 'Lacto-Vegetarian', 'Pescatarian', 'Vegan', 'Vegetarian'],
    foodPreferencesDto: {
      foodPreferences: []
    },
    response: false
  }),

  methods: {
    update () {
      axios.post(this.$store.state.urls.foodPreferences.editPreferences, {
        username: jwt.decode(this.$store.state.authenticationToken).Username,
        foodPreferencesDto: this.foodPreferencesDto
      }).then(response => {
        console.log(response.data)
        this.response = response.data
      }).catch(error => {
        Promise.reject(error)
      })
    }
  },

  // beforeCreate () {
  //   if (this.$store.state.authenticationToken === null) {
  //     this.$router.push({path: '/Unauthorized'})
  //   }
  //   try {
  //     if (jwt.decode(this.$store.state.authenticationToken).UpdatePreferences === 'True') {
  //     } else {
  //       this.$router.push({path: '/Forbidden'})
  //     }
  //   } catch (ex) {
  //     this.$router.push({path: '/Forbidden'})
  //   }
  // },

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
      this.foodPreferences = response.data
    }).catch(error => {
      Promise.reject(error)
    })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1, h3 {
  font-weight: normal;
  font-family: 'Open Sans', sans-serif;
}
#form-divider {
  display: inline-block;
  margin: 1em;
}
.text {
  font-family: 'Open Sans', sans-serif;
}
#page-divider {
  margin: 5.5em 0 0 0;
}
</style>
