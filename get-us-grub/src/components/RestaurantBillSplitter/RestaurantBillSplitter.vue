<template>
  <div id="restaurant-bill-splitter">
    <app-header />
    <div class="wrapper">
      <h1 id="restaurant-name-header" v-if="restaurantDisplayName"> {{ this.restaurantDisplayName }} </h1>
      <restaurantBillSplitter-userTable id="userTable"/>
      <restaurantBillSplitter-billTable id="billTable"/>
      <restaurantBillSplitter-dictionary id="dictionary"/>
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
      restaurantId: this.$store.state.restaurantSelection.selectedRestaurant.restaurantId,
      restaurantDisplayName: this.$store.state.restaurantSelection.selectedRestaurant.displayName
    }
  },
  created () {
    if (this.$store.state.isAuthenticated) {
      this.restaurantId = this.$store.state.restaurantSelection.selectedRestaurant.restaurantId
      axios.get(this.$store.state.urls.restaurantBillSplitter.getRestaurantMenus, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        },
        params: {
          restaurantId: this.restaurantId
        }
      }).then(response => {
        for (var i = 0; i < response.data.data.menus.length; i++) {
          for (var j = 0; j < response.data.data.menus[i].items.length; j++) {
            response.data.data.menus[i].items[j].itemPrice = this.convertFromUSDtoInt(response.data.data.menus[i].items[j].itemPrice)
          }
        }
        this.$store.dispatch('populateRestaurantMenus', response.data.data.menus)
      }).catch(error => {
        Promise.reject(error)
      })
    }
  },
  methods: {
    convertFromUSDtoInt: function (usDollars) {
      return this.$store.getters.convertFromUSDtoInt(usDollars)
    }
  },
  computed: {
    // getRestaurantURL () {
    //   return this.$store.state.urls.restaurantBillSplitter.getRestaurantMenus
    // }
  }
}
</script>

<style scoped>
  #restaurant-bill-splitter {
    margin: 0 0 100px 0;
  }

  .wrapper {
    margin: 80px 25px;
    display: grid;
    grid-template-columns: 1fr 2.5fr 1fr;
    grid-template-rows: auto auto auto;
    grid-gap: 10px;
    grid-auto-rows: minmax(100px, auto);
  }

  #restaurant-name-header {
    grid-column: 2;
    grid-row: 1;
  }

  h2.total {
    padding: 10px 1.2em 0px 0px;
    text-align: right;
  }

  #dictionary {
    grid-column: 3;
    grid-row: 2 / 5;
    outline: solid;
  }

  #billTable {
    grid-column: 2 / 3;
    grid-row: 2 / 5;
    outline: solid;
  }

  #userTable {
    grid-column: 1 / 2;
    grid-row: 2 / 5;
    outline: solid;
  }

  #debug {
    grid-column: 2;
    grid-row: 5;
  }
</style>
