<template>
  <div>
    <app-header/>
    <div id="page-divider">
      <h1>{{ title }}</h1>
      <h3>{{ message }}</h3>
      <div id="text-divider">
        <li v-for='preference in foodPreferences' :key='preference'>{{ preference }}</li>
      </div>
      <div>
        <router-link to=/FoodPreferences/Edit>
          <v-btn :dark=true>Edit Food Preferences</v-btn>
        </router-link>
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
  name: 'FoodPreferences',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  },

  data () {
    return {
      title: 'Your Food Preferences',
      message: 'Here contains your list of dietary preferences.',
      foodPreferences: [],
      errors: []
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
      this.foodPreferences = response.data
    }).catch(error => {
      Promise.reject(error)
    })
  }
}

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  @import url('https://fonts.googleapis.com/css?family=Open+Sans');

  h1, h3 {
    font-weight: normal;
    font-family: 'Open Sans', sans-serif;
  }
  #text-divider {
    display: inline-block;
    padding-top: 1vh;
    padding-bottom: 1vh;
  }
  li {
    font-family: 'Open Sans', sans-serif;
    font-size: 1.1em;
    text-align: left;
    list-style-type: circle;
  }
  #page-divider {
    margin: 5.5em 0 0 0;
  }
</style>
