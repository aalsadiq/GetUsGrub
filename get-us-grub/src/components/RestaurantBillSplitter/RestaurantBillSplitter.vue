<template>
  <div>
    <app-header></app-header>
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
      restaurantDisplayName: '',
      restaurantLatitude: null,
      restaurantLongitude: null
    }
  },
  created () {
    this.restaurantDisplayName = this.$store.state.restaurantDisplayName
    this.restaurantLatitude = this.$store.state.restaurantLatitude
    this.restaurantLongitude = this.$store.state.restaurantLongitude
    if (this.$store.state.isAuthenticated) {
      console.log('Authenticated')
      axios.get('http://localhost:8081/RestaurantBillSplitter/Restaurant', {
        headers: {
          'Access-Control-Allow-Origin': '*'
        },
        params: {
          DisplayName: this.restaurantDisplayName,
          Latitude: this.restaurantLatitude,
          Longitude: this.restaurantLongitude
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
      console.log(this.$store.state.restaurantID)
      if (this.$store.state.isAuthenticated) {
        console.log('Authenticated')
        axios.get('http://localhost:8081/RestaurantBillSplitter/Restaurant', {
          headers: {
            'Access-Control-Allow-Origin': '*'
          },
          params: {
            DisplayName: this.$store.state.restaurantID,
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
  .wrapper {
    margin: 20px;
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 10px;
    grid-auto-rows: minmax(100px, auto);
  }

  .one {
    grid-column: 1 / 3;
    grid-row: 1 / 4;
    outline: dashed;
  }

  .one > h1 {
    text-align: center;
  }

  div.bill {
    margin: 20px;
    min-height: 20px;
    outline: dashed;
    background-color: grey;
  }

    div.bill > div.bill-item {
      margin: 10px;
      padding: 10px;
      background-color: aquamarine;
      border-radius: 10px;
      text-align: center;
    }

  h2.total {
    padding: 10px 1.2em 0px 0px;
    text-align: right;
  }
</style>
