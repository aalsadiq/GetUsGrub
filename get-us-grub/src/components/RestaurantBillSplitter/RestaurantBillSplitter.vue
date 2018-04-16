<template>
  <div id="restaurant-bill-splitter">
    <app-header />
    <div class="wrapper">
      <restaurantBillSplitter-userTable />
      <restaurantBillSplitter-billTable />
      <restaurantBillSplitter-dictionary />
      <debug />
      <v-btn v-on:click="GetRestaurantMenus"> Test Get Request </v-btn>
    </div>
    <app-footer />
  </div>
</template>

<script>
import axios from 'axios'
import AppHeader from '../AppHeader.vue'
import AppFooter from '../AppFooter.vue'
import UserTable from './UserTable.vue'
import BillTable from './BillTable.vue'
import Dictionary from './Dictionary.vue'
import Debug from './Debug.vue'

export default {
  name: 'RestaurantBillSplitter',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter,
    'restaurantBillSplitter-userTable': UserTable,
    'restaurantBillSplitter-billTable': BillTable,
    'restaurantBillSplitter-dictionary': Dictionary,
    'debug': Debug
  },
  data () {
    return {
      restaurantId: null
    }
  },
  created () {
    if (this.$store.state.isAuthenticated) {
      console.log('Authenticated')
      this.restaurantId = this.$store.state.restaurantSelection.selectedRestaurant.restaurantId
      axios.get('http://localhost:8081/RestaurantBillSplitter/Restaurant', {
        headers: {
          'Access-Control-Allow-Origin': '*'
        },
        params: {
          restaurantId: this.restaurantId
        }
      }).then(response => {
        console.log(response)
        // this.responseDataStatus = 'Success! Restaurant Menus have been get: '
        // this.responseData = response.data
        // console.log(response)
      }).catch(error => {
        this.responseDataStatus = 'An error has occurred: '
        this.responseData = error.response.data
        console.log(error.response.data)
      })
    }
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
          console.log(response)
          // this.responseDataStatus = 'Success! Restaurant Menus have been get: '
          // this.responseData = response.data
          // console.log(response)
        }).catch(error => {
          this.responseDataStatus = 'An error has occurred: '
          this.responseData = error.response.data
          console.log(error.response.data)
        })
      }
    }
  },
  computed: {
  }
}
</script>

<style scoped>
  #restaurant-bill-splitter {
    margin: 0 0 100px 0;
  }

  .wrapper {
    display: grid;
    grid-template-columns: 1fr 2fr 1fr;
    grid-gap: 10px;
    grid-auto-rows: minmax(100px, auto);
  }

  h2.total {
    padding: 10px 1.2em 0px 0px;
    text-align: right;
  }
</style>
