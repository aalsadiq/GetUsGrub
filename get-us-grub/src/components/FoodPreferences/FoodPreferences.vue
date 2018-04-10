<template>
  <div>
    <app-header/>
    <h1>{{ title }}</h1>
    <!--li v-for="preference in foodPreferences">{{ preference }}</li-->
    <app-footer/>
  </div>
</template>

<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'FoodPreferences',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter
  },
  data () {
    return {
      title: 'Your Food Preferences',
      foodPreferences: ['Pescatarian', 'Lacto-Vegetarian']
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<!--style scoped>
h1, h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
<li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style-->
