<template>
  <div>
    <app-header/>
      <div id="page-container">
        <h1>{{ title }}</h1>
        <h3>{{ message }}</h3>
        <br/>
        <div id="form-container">
          <v-form v-model=isValid>
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
            <v-btn dark="true">Back</v-btn>
          </router-link>
          <v-btn dark="true">Save</v-btn>
        </div>
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
  data: () => ({
    title: 'Edit Your Food Preferences',
    message: 'Checked elements will be saved as your updated food preferences.',
    foodPreferences: ['Halal', 'Vegan'],
    checklist: ['Gluten-free', 'Halal', 'Kosher', 'Lacto-Vegetarian', 'Pescatarian', 'Vegan', 'Vegetarian'],
    isValid: false
  }),
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
h1, h3 {
  font-weight: normal;
  font-family: 'Open Sans', sans-serif;
}
#form-container {
  display: inline-block;
}
.text {
  font-family: 'Open Sans', sans-serif;
}
#page-container {
  margin: 5.5em 0 0 0;
}
</style>
