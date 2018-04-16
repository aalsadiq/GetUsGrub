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
            Test
          </v-toolbar-title>
        </v-toolbar>
        <v-btn v-on:click="GetRestaurantMenus"></v-btn>
        <v-btn v-for="element in restaurantMenus">Test</v-btn>
      </v-list>
    </v-navigation-drawer>
  </v-layout>
</template>

<script>
import axios from 'axios'

export default {
  name: 'GetRestaurantMenus',
  data () {
    return {
      drawer: false,
      restaurantId: null,
      restaurantMenus: [],
      menus: [
        {
          menu: [
            {
              item: {
                itemName: '',
                itemPrice: null,
                itemPicture: '',
                tag: '',
                description: ''
              }
            }
          ]
        }
      ]
    }
  },
  updated () {
    this.restaurantMenus = this.$store.state.restaurantMenus
  },
  methods: {
    GetRestaurantMenus: function () {
      if (this.$store.state.isAuthenticated) {
        console.log('Authenticated')
        this.restaurantId = this.$store.state.restaurantSelection.selectedRestaurant.restaurantId
        console.log(this.restaurantId)
        axios.get('http://localhost:8081/RestaurantBillSplitter/Restaurant', {
          headers: {
            'Access-Control-Allow-Origin': '*'
          },
          params: {
            restaurantId: this.restaurantId
          }
        }).then(response => {
          this.$store.dispatch('populateRestaurantMenus', response.data.data.menus)
          console.log(response.data.data.menus)
          console.log(this.$store.state.restaurantMenus)
          // for (int i = 0; i < response.data.data.menus.length; i++) {
          // this.menus[i] = response.data.data.menus[i]
          // }
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
          console.log(error.response.data)
        })
      }
    }
  }
}
</script>

<style scoped>

</style>
