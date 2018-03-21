<template>
  <div>
    <app-header></app-header>
    <div class="wrapper">
      <div class="one">
        <h1>Your Bill</h1>
        <draggable class="bill" v-bind:list="BillItems" v-bind:options="{group:'people'}" @start="drag=true" @end="drag=false">
          <div class="bill-item" v-for="(element, index) in BillItems" :key="index">
            {{index}} - {{element.menuItemName}} : ${{element.menuItemPrice.toFixed(2)}}
            <div style="display: inline-block">
              <v-btn small=true><!--v-on:click="EditDictionaryFoodItem"-->Edit</v-btn>
              <v-btn small=true v-on:click="RemoveFromBill(index)">Delete</v-btn>
            </div>
          </div>
        </draggable>
        <h2 class="total">Total:</h2>
      </div>
      <restaurantBillSplitter-dictionaryInput/>
      <restaurantBillSplitter-dictionary/>
    </div>
    <div>
      <h1> Debug </h1>
      <h2> Menu Items</h2>
      <ul>
        <li v-for="element in MenuItems" :key="element">
          {{element}}
        </li>
      </ul>
      <h2> Bill Items</h2>
      <ul>
        <li v-for="element in BillItems" :key="element">
          {{element}}
        </li>
      </ul>
    </div>
  </div>

</template>

<script>
import AppHeader from '../AppHeader.vue'
import Dictionary from './Dictionary.vue'
import DictionaryInput from './DictionaryInput.vue'
import draggable from 'vuedraggable'

export default {
  name: 'RestaurantBillSplitter',
  components: {
    'app-header': AppHeader,
    'restaurantBillSplitter-dictionaryInput': DictionaryInput,
    'restaurantBillSplitter-dictionary': Dictionary,
    draggable
  },
  data () {
    return {

    }
  },
  methods: {
    RemoveFromBill: function (index) {
      // Parameters have to be placed into an array because dispatch can only take two. The name and the payload.
      console.log(index)
      this.$store.dispatch('RemoveFromBill', index)
    }
  },
  computed: {
    MenuItems () {
      return this.$store.state.MenuItems
    },
    BillItems () {
      return this.$store.state.BillItems
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
