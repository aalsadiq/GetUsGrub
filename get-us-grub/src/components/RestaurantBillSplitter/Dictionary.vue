<template>
  <div class="dictionary">
    <h2 v-if="restaurantDisplayName">{{ restaurantDisplayName }}</h2>
    <h2>Dictionary</h2>
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
import EditItem from './EditItem.vue'
import DeleteItem from './DeleteItem.vue'
export default {
  name: 'Dictionary',
  components: {
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
    grid-row: 2 / 4;
    outline: solid;
  }

    .dictionary > h2 {
      text-align: center
    }

  div.menu-item {
    margin: 10px;
    padding: 10px;
    background-color: aquamarine;
    border-radius: 10px;
    text-align: center;
  }
</style>
