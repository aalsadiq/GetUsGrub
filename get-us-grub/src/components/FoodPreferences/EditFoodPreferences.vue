<template>
  <div>
    <app-header/>
    <h1>{{ title }}</h1>
    <h2>{{ msg }}</h2>
    <li v-for="pref in prefList">{{ pref }}</li>

    <br/>
    <br/>

    <h1>Tester 2</h1>

    <li v-for="pref in allPref">
        <input type="checkbox" id="pref" :value="pref" v-model="prefList">
        <label>{{ pref }}</label>
    </li>

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
      title: 'Edit Food Preferences',
      msg: 'Your current food preferences:',
      prefList: ['Pescatarian', 'Lacto-Vegetarian'],
      allPref: ['Gluten-free', 'Halal', 'Kosher', 'Lacto-Vegetarian', 'Pescatarian', 'Vegan', 'Vegetarian'],
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
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
