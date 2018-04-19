<template>
  <div class="dictionary">
    <h1 v-if="restaurantDisplayName">{{ restaurantDisplayName }}</h1>
    <h1 id="customDictionaryHeader" v-if="!restaurantDisplayName"> Dictionary</h1>
    <v-divider/>
    <ul style="list-style: none; display: inline-flex;">
      <li>
        <dictionary-input />
      </li>
      <li>
        <get-restaurant-menus />
      </li>
    </ul>
    <draggable class="menu" v-bind:list="menuItems" v-bind:options="{group:{ name:'items', pull:'clone', put:false }}" :clone="Clone" @start="drag=true" @end="drag=false">
      <div class="menu-item" v-for="(menuItem, menuItemIndex) in menuItems" :key="menuItemIndex">
        {{menuItem.name}} : ${{menuItem.price}}<br />
        <edit-item :editType="$options.name" :itemIndex="menuItemIndex" :Item="menuItem" />
        <delete-item :deleteType="$options.name" :itemIndex="menuItemIndex" />
      </div>
    </draggable>
  </div>
</template>

<script>
import axios from 'axios'
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
      restaurantDisplayName: '',
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
        name: el.name,
        price: el.price,
        selected: el.selected
      }
    },
    Log: function () {
      console.log(this.$refs.editForm)
    },
    GetRestaurantMenus: function () {
      axios.get('http://localhost:8081/RestaurantBillSplitter', {
        displayName: ''
      })
    }
  },
  computed: {
    menuItems () {
      return this.$store.state.menuItems
    }
  }
}
</script>

<style>
  .dictionary {
    grid-column: 3;
    grid-row: 1 / 4;
    outline: solid;
  }

    .dictionary > h1 {
      text-align: center
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
