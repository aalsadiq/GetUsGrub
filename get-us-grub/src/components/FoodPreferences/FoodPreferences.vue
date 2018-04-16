<template>
  <div>
    <app-header/>
    <h1>{{ title }}</h1>
    <ul>
      <li v-for='preference in foodPreferences' :key='preference'>{{ preference }}</li>
    </ul>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'

export default {
  name: 'FoodPreferences',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  },

  data () {
    return {
      title: 'Your Food Preferences',
      foodPreferences: [],
      errors: []
    }
  },

  created () {
    axios.get('http://localhost:8081/FoodPreferences', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      params: {
        username: 'username27'
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
h1, h2 {
  font-weight: normal;
}
ul {
    display: inline-block;
}
li {
  list-style-type: circle;
  text-align: left;
}
</style>
