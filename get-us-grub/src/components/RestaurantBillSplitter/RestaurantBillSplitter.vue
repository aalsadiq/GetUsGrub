<template>
  <div id="restaurant-bill-splitter">
    <app-header />
    <div class="wrapper">
      <restaurantBillSplitter-userTable />
      <restaurantBillSplitter-billTable />
      <restaurantBillSplitter-dictionaryInput />
      <restaurantBillSplitter-dictionary />
      <debug/>
      <v-btn v-on:click="GetRestaurantMenus(restaurantDisplayName)"> Test Get Request </v-btn>
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
import DictionaryInput from './DictionaryInput.vue'
import Debug from './Debug.vue'

export default {
  name: 'RestaurantBillSplitter',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter,
    'restaurantBillSplitter-userTable': UserTable,
    'restaurantBillSplitter-billTable': BillTable,
    'restaurantBillSplitter-dictionaryInput': DictionaryInput,
    'restaurantBillSplitter-dictionary': Dictionary,
    'debug': Debug
  },
  data () {
    return {
      restaurantDisplayName: this.$store.state.restaurantDisplayName
    }
  },
  created () {
    //console.log(this.$store.state.restaurantDisplayName)
    //console.log(this.$store.state.restaurantLatitude)
    //console.log(this.$store.state.restaurantLongitude)
    //if (this.$store.state.isAuthenticated) {
    //console.log('Authenticated')
    //axios.get('http://localhost:8081/RestaurantBillSplitter/Restaurant', {
    //  headers: {
    //    'Access-Control-Allow-Origin': '*'
    //  },
    //  DisplayName: this.$store.state.restaurantDisplayName,
    //  Latitude: this.$store.state.restaurantLatitude,
    //  Longitude: this.$store.state.restaurantLongitude
    //}).then(response => {
    //  console.log('GET Success!')
    //  // this.responseDataStatus = 'Success! Restaurant Menus have been get: '
    //  // this.responseData = response.data
    //  // console.log(response)
    //}).catch(error => {
    //  this.responseDataStatus = 'An error has occurred: '
    //  this.responseData = error.response.data
    //  console.log(error.response.data)
    //})
    //}
  },
  methods: {
    GetRestaurantMenus: function () {
      console.log(this.$store.state.restaurantDisplayName)
      console.log(this.$store.state.restaurantLatitude)
      console.log(this.$store.state.restaurantLongitude)
      if (this.$store.state.isAuthenticated) {
        console.log('Authenticated')
        axios.get('http://localhost:8081/RestaurantBillSplitter/Restaurant', {
          headers: {
            'Access-Control-Allow-Origin': '*'
          },
          params: {
            DisplayName: this.$store.state.restaurantDisplayName,
            Latitude: this.$store.state.restaurantLatitude,
            Longitude: this.$store.state.restaurantLongitude
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
    MenuItems () {
      return this.$store.state.MenuItems
    },
    BillItems () {
      return this.$store.state.BillItems
    },
    BillUsers () {
      return this.$store.state.BillUsers
    },
    totalPrice () {
      return this.$store.getters.totalPrice
    }
  }
}
</script>
<style scoped>
  #restaurant-bill-splitter{
    margin: 0 0 100px 0;
  }

  .wrapper {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 10px;
    grid-auto-rows: minmax(100px, auto);
  }

  h2.total {
    padding: 10px 1.2em 0px 0px;
    text-align: right;
  }
</style>
