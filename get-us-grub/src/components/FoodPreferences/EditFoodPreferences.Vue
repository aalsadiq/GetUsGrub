<template>
  <div>
    <app-header/>
    <h1>{{ title }}</h1>
    <h2>{{ test }}</h2>
    <ul>
      <li v-for='preference in foodPreferences' :key='preference'>{{ preference }}</li>
    </ul>
    <br/>
    <br/>
    <h1>Food Preference Checklist</h1>
    <h2>{{ message }}</h2>
    <br/>
    <ul>
      <li v-for='preference in checklist' :key='preference'>
        <input type="checkbox" id="pref" :value="preference" v-model="foodPreferences">
        <label>{{ preference }}</label>
      </li>
    </ul>
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'EditPreferences',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  },
  data () {
    return {
      title: 'Edit Your Food Preferences',
      test: 'Checked elements for testing',
      message: 'Checked elements will be saved as your food preferences.',
      foodPreferences: ['Lacto-Vegetarian', 'Pescatarian'],
      checklist: ['Gluten-free', 'Halal', 'Kosher', 'Lacto-Vegetarian', 'Pescatarian', 'Vegan', 'Vegetarian']
    }
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
  text-align: left;
}
li {
  list-style-type: circle;
  text-align: left;
}
</style>
