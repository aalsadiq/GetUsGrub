<template>
  <div>
    <app-header/>
    <div class="wrapper">
      <div class="one">
        <h1>Your Bill</h1>
        <draggable class="bill" :list="BillItems" :options="{group:'people'}" @start="drag=true" @end="drag=false">
          <div class="bill-item" v-for=" (element, index) in BillItems" :key="index">
            {{index}} - {{element.menuItemName}} : ${{element.menuItemPrice.toFixed(2)}}
            <div style="display: inline-block">
              <btn type="primary" size="xs"><!--v-on:click="EditDictionaryFoodItem"-->Edit</btn>
              <btn type="danger" size="xs" v-on:click="RemoveFromBill(index)">Delete</btn>
            </div>
          </div>
        </draggable>
        <h2 class="total">Total:</h2>
      </div>

      <restaurantBillSplitter-dictionaryInput/>
      <restaurantBillSplitter-dictionary/>
      
    </div>
    <app-footer />
    <div>
      <h1> Debug </h1>
      <ul>
        <li v-for="element in MenuItems">
          {{element}}
        </li>
      </ul>
    </div>
  </div>

</template>

<script lang="ts">
  import Header from '../Header.vue'
  import Footer from '../Footer.vue'
  import Dictionary from './Dictionary.vue'
  import DictionaryInput from './DictionaryInput.vue'
  import draggable from 'vuedraggable'

  export default {
    name: 'RestaurantBillSplitter',
    components: {
      'app-header': Header,
      'app-footer': Footer,
      'restaurantBillSplitter-dictionaryInput': DictionaryInput,
      'restaurantBillSplitter-dictionary': Dictionary,
      draggable
    },
    data() {
      return {
        show: false
      }
    },
    methods: {
      RemoveFromBill: function (index) {
        // Parameters have to be placed into an array because dispatch can only take two. The name and the payload.
        console.log(index);
        this.$store.dispatch('RemoveFromBill', index);
      }
    },
    computed: {
      MenuItems() {
        return this.$store.state.MenuItems;
      },       
      BillItems() {
        return this.$store.state.BillItems;        
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
