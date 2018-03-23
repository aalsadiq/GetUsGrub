<template>
  <div>
    <h1> Debug </h1>
    <h2> Menu Items</h2>
    <ul>
      <li v-for="element in MenuItems" :key="element">
        {{element}}
      </li>
    </ul>
    <h2> Bill Items</h2>
    <ul>
      <li v-for="element in BillItems" :key="element">
        {{element}}
      </li>
    </ul>
    <h2> Bill Users</h2>
    <ul>
      <h3> Unique ID Next: {{ this.$store.state.uniqueUserCounter }} </h3>
      <li v-for="element in BillUsers" :key="element">
        {{element}}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'Debug',
  computed: {
    MenuItems () {
      return this.$store.state.MenuItems
    },
    BillItems () {
      return this.$store.state.BillItems
    },
    BillUsers () {
      return this.$store.state.BillUsers
    }
  }
}
</script>

<style scoped>

</style>
