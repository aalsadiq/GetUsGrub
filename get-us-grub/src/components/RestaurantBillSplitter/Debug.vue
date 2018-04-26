<template>
  <div>
    <h1> Debug </h1>
    <h2> Menu Items</h2>
    <ul>
      <li v-for="(element, index) in menuItems" :key="index">
        {{element}}
      </li>
    </ul>
    <h2> Bill Items</h2>
    <ul>
      <li v-for="(element, index) in billItems" :key="index">
        {{element}}
      </li>
    </ul>
    <h2> Bill Users</h2>
    <ul>
      <h3> Unique ID Next: {{ this.$store.state.uniqueCounter }} </h3>
      <li v-for="(element, index) in billUsers" :key="index">
        {{element}}
      </li>
    </ul>
    <h2> Restaurant Menus</h2>
    <ul>
      <h3>{{restaurantMenus.length}}</h3>
      <li v-for="(element, index) in restaurantMenus" :key="index">
        {{element}}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'Debug',
  updated () {
  },
  computed: {
    menuItems () {
      return this.$store.state.menuItems
    },
    billItems () {
      return this.$store.state.billItems
    },
    billUsers () {
      return this.$store.state.billUsers
    },
    restaurantMenus () {
      return this.$store.state.restaurantMenus
    }
  }
}
</script>

<style scoped>
</style>
