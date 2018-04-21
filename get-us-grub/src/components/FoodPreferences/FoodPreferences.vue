<template>
  <div>
    <app-header/>
    <h1>{{ title }}</h1>
    <h3>{{ message }}</h3>
    <div class="text-container">
      <li v-for='preference in foodPreferences' :key='preference'>{{ preference }}</li>
    </div>
    <div>
      <router-link to="/Profile">
        <v-btn dark="true">Back</v-btn>
      </router-link>
      <router-link to=/FoodPreferences/Edit>
        <v-btn dark="true">Edit</v-btn>
      </router-link>
    </div>
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
      message: 'Here contains your list of dietary needs.',
      foodPreferences: [],
      errors: []
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
      console.log(response.data)
      this.foodPreferences = response.data
    }).catch(e => {
      this.errors.push(e)
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
  .text-container {
    display: inline-block;
    padding-top: 1vh;
    padding-bottom: 1vh;
  }
  li {
    font-family: 'Open Sans', sans-serif;
    text-align: left;
    list-style-type: circle;
  }
</style>
