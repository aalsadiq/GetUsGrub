<template>
  <v-layout>
    <v-btn small
           dark
           v-on:click="drawer = !drawer">
      Change Menu
    </v-btn>
    <v-navigation-drawer temporary
                         v-model="drawer"
                         right
                         absolute>
      <v-list>
        <v-toolbar>
          <v-toolbar-title>
            Select Your Menu!
          </v-toolbar-title>
        </v-toolbar>
        <div>
          <v-btn dark color="green" v-on:click="useCustomMenu"> Your Custom Menu</v-btn>
        </div>
        <v-divider />
        <div v-if="this.restaurantDisplayName">
          <h2> {{ this.restaurantDisplayName }}'s Menus </h2>
          <v-btn dark color="blue" v-for="(menu, index) in restaurantMenus" :key="index" v-on:click="populateDictionary(menu.items)">
            {{ menu.menuName }}
          </v-btn>
        </div>
      </v-list>
    </v-navigation-drawer>
  </v-layout>
</template>

<script>
export default {
  name: 'GetRestaurantMenus',
  data () {
    return {
      drawer: false,
      restaurantId: this.$store.state.restaurantSelection.selectedRestaurant.restaurantId,
      restaurantDisplayName: this.$store.state.restaurantSelection.selectedRestaurant.displayName,
      restaurantMenuItems: []
    }
  },
  // updated () {
  //   this.restaurantMenus = this.$store.state.restaurantMenus
  // },
  methods: {
    useCustomMenu: function () {
      this.$store.dispatch('useCustomMenu')
    },
    populateDictionary: function (menuItems) {
      this.restaurantMenuItems = menuItems
      console.log(this.restaurantMenuItems)
      this.$store.dispatch('populateDictionary', this.restaurantMenuItems)
    }
  },
  computed: {
    restaurantMenus () {
      return this.$store.state.restaurantMenus
    }
  }
}
</script>

<style scoped>
</style>
