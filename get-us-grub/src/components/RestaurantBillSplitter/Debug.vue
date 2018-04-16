<template>
  <div>
    <h1> Debug </h1>
    <h2> Menu Items</h2>
    <ul>
      <li v-for="element in menuItems" :key="element">
        {{element}}
      </li>
    </ul>
    <h2> Bill Items</h2>
    <ul>
      <li v-for="element in billItems" :key="element">
        {{element}}
      </li>
    </ul>
    <h2> Bill Users</h2>
    <ul>
      <h3> Unique ID Next: {{ this.$store.state.uniqueUserCounter }} </h3>
      <li v-for="element in billUsers" :key="element">
        {{element}}
      </li>
    </ul>
    <h2> Restaurant Menus</h2>
    <ul>
      <h3>{{restaurantMenus.length}}</h3>
      <li v-for="element in restaurantMenus" :key="element">
        {{element}}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'Debug',
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
    restaurantMenus() {
      return this.$store.state.restaurantMenus
    }
  }
}
</script>

<style scoped>
</style>
