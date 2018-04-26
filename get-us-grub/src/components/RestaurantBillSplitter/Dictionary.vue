<template>
  <div id="dictionary">
    <div class="dictionary-header">
      <h1 v-if="showRestaurantMenuItems">{{ restaurantDisplayName }}</h1>
      <h1 id="customDictionaryHeader" v-if="!showRestaurantMenuItems"> Dictionary</h1>
      <v-divider />
    </div>
    <ul class="dictionary-controls">
      <li>
        <dictionary-input v-if="!showRestaurantMenuItems" />
      </li>
      <li>
        <get-restaurant-menus />
      </li>
    </ul>
    <draggable v-if="!showRestaurantMenuItems" class="menu" v-bind:list="menuItems" v-bind:options="{group:{ name:'items', pull:'clone', put:false }}" :clone="Clone" @start="drag=true" @end="drag=false">
      <div class="menu-item"  v-for="(menuItem, menuItemIndex) in menuItems" :key="menuItemIndex">
        {{menuItem.itemName}} : ${{(menuItem.itemPrice / 100).toFixed(2)}}<br />
        <edit-item :editType="$options.name" :itemIndex="menuItemIndex" :Item="menuItem" />
        <delete-item :deleteType="$options.name" :itemIndex="menuItemIndex" />
      </div>
    </draggable>
    <draggable v-if="showRestaurantMenuItems" class="menu" v-bind:list="restaurantMenuItems" v-bind:options="{group:{ name:'items', pull:'clone', put:false }}" :clone="Clone" @start="drag=true" @end="drag=false">
      <div class="menu-item" v-for="(restaurantMenuItem, restaurantMenuItemIndex) in restaurantMenuItems" :key="restaurantMenuItemIndex">
        {{restaurantMenuItem.itemName}} : ${{(restaurantMenuItem.itemPrice / 100).toFixed(2)}}<br />
      </div>
    </draggable>
  </div>
</template>

<script>
import draggable from 'vuedraggable'
import { VMoney } from 'v-money'
import DictionaryInput from './DictionaryInput'
import GetRestaurantMenus from './GetRestaurantMenus.vue'
import EditItem from './EditItem.vue'
import DeleteItem from './DeleteItem.vue'

export default {
  name: 'Dictionary',
  components: {
    'dictionary-input': DictionaryInput,
    'get-restaurant-menus': GetRestaurantMenus,
    'edit-item': EditItem,
    'delete-item': DeleteItem,
    draggable
  },
  data () {
    return {
      restaurantId: null,
      restaurantDisplayName: this.$store.state.restaurantSelection.selectedRestaurant.displayName,
      money: {
        decimal: '.',
        thousands: '',
        prefix: '',
        suffix: '',
        precision: 2,
        masked: false
      }
    }
  },
  created () {
  },
  directives: { money: VMoney },
  methods: {
    Clone: function (el) {
      return {
        uID: this.getAndIncrementUniqueCounter(),
        itemName: el.itemName,
        itemPrice: el.itemPrice,
        selected: el.selected
      }
    },
    getAndIncrementUniqueCounter: function () {
      var temp = this.getUniqueCounter
      this.$store.dispatch('incrementUniqueCounter')
      return temp
    }
  },
  computed: {
    menuItems () {
      return this.$store.state.menuItems
    },
    restaurantMenuItems () {
      return this.$store.state.restaurantMenuItems
    },
    showRestaurantMenuItems () {
      return this.$store.state.showRestaurantMenuItems
    },
    convertFromInttoUSD () {
      return this.$store.getters.convertFromInttoUSD
    },
    getUniqueCounter () {
      return this.$store.getters.getUniqueCounter
    }
  }
}
</script>

<style>
  #dictionary {
    display: grid;
    grid-template-rows: min-content min-content auto;
    grid-gap: 0px;
  }

    #dictionary > .dictionary-header {
      text-align: center;
      grid-row: 1;
      grid-row-end: 1;
    }

    #dictionary > .dictionary-controls {
      grid-row: 2;
      list-style: none;
      display: flex;
      justify-content: center;
    }

  .menu {
    grid-row: 3;
  }

  div.menu-item {
    margin: 10px;
    padding: 10px;
    background-color: aquamarine;
    border-radius: 10px;
    text-align: center;
  }

  #customDictionaryHeader {

  }
</style>
