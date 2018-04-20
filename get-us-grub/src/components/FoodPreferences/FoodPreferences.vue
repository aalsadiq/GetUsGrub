<template>
  <div>
    <div>
      <h1>{{ title }}</h1>
    </div>
    <div class="list">
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
    axios.get('http://localhost:8081/FoodPreferences/GetPreferences', {
      headers: {
        'Access-Control-Allow-Origin': '*'
      },
      params: {
        username: 'username17'
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
.list {
    display: inline-block;
    background-color: transparent;
}
li {
  list-style-type: circle;
  text-align: left;
}
</style>
