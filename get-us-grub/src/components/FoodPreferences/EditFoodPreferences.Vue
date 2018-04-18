<template>
  <div>
    <app-header/>
    <div>
      <h1>{{ title }}</h1>
      <h2>{{ message }}</h2>
    </div>
    <br/>
    <div class="checklist">
      <ul v-for='preference in checklist' :key='preference'>
        <input type="checkbox" id="pref" :value="preference" v-model="foodPreferences">
        <label>{{ preference }}</label>
        <br/>
      </ul>
    </div>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
import axios from 'axios'
export default {
  name: 'EditPreferences',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  },
  data () {
    return {
      title: 'Edit Your Food Preferences',
      message: 'Checked elements will be saved as your updated food preferences.',
      foodPreferences: [],
      checklist: ['Gluten-free', 'Halal', 'Kosher', 'Lacto-Vegetarian', 'Pescatarian', 'Vegan', 'Vegetarian']
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
ul {
  text-align: left;
  align-content: center;
}
li {
  list-style-type: circle;
  text-align: left;
}
.checklist {
  display: inline-block;
}
</style>
