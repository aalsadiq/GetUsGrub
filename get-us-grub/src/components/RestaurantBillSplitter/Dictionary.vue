<template>
  <div class="dictionary">
    <h2>Dictionary</h2>
    <draggable class="menu" v-bind:list="MenuItems" v-bind:options="{group:{ name:'people', pull:'clone', put:false }}" @start="drag=true" @end="drag=false">
      <div class="menu-item" v-for="(menuItem, menuItemIndex) in MenuItems" :key="menuItemIndex">
        {{menuItem.name}} : ${{menuItem.price}}<br />
        <edit-item :editType="editType" :itemIndex="menuItemIndex" :Item="menuItem"></edit-item>
        <v-btn small dark color="red" v-on:click="RemoveFromDictionary(menuItemIndex)">
          Delete
        </v-btn>
      </div>
    </draggable>
  </div>
</template>

<script>
import draggable from 'vuedraggable'
import { VMoney } from 'v-money'
import EditItem from './EditItem.vue'

export default {
  name: 'Dictionary',
  components: {
    'edit-item': EditItem,
    draggable
  },
  data () {
    return {
      editType: 'dictionary',
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
  directives: { money: VMoney },
  methods: {
    clone: function (el) {
      return {
        name: el.name,
        price: el.price
      }
    },
    RemoveFromDictionary: function (menuItemIndex) {
      console.log(menuItemIndex)
      this.$store.dispatch('RemoveFromDictionary', menuItemIndex)
    },
    Log: function () {
      console.log(this.$refs.editForm)
    }
  },
  computed: {
    MenuItems () {
      return this.$store.state.MenuItems
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

  .dictionary > h2{
    text-align:center
  }

  div.menu-item {
    margin: 10px;
    padding: 10px;
    background-color: aquamarine;
    border-radius: 10px;
    text-align: center;
  }

</style>
