<template>
  <div>
    <app-header/>
    <btn type="success">Test</btn>

    <div class="wrapper">
      <div class="one">
        <h1>Your Bill</h1>
        <draggable class="bill" v-model="BillItems" :list="BillItems" :options="{group:'people'}" @start="drag=true" @end="drag=false">
          <div class="bill-item" v-for=" (element, index) in BillItems" :key="index">
            {{index}} - {{element.menuItemName}} : ${{element.menuItemPrice}}
            <div style="display: inline-block">
              <btn type="primary" size="xs" ><!--v-on:click="EditDictionaryFoodItem"-->Edit</btn>
              <btn type="danger" size="xs" ><!--v-on:click="RemoveFromBill"-->Delete</btn>
            </div>
          </div>
        </draggable>
      </div>

      <restaurantBillSplitter-dictionaryInput/>

      <div class="dictionary">
        <h2>Dictionary</h2>
        <draggable class="menu" v-model="MenuItems" :list="MenuItems" :clone="clone" :options="{group:{ name:'people',  pull:'clone', put:false }}" @start="drag=true" @end="drag=false">
          <div class="menu-item" v-for="(element, index) in MenuItems" :key="element.id">
            {{element.menuItemName}} : ${{element.menuItemPrice}}
            <btn type="primary" size="xs" ><!--v-on:click="EditDictionaryFoodItem"-->Edit</btn>
            <btn type="danger" id="index" size="xs" ><!--v-on:click="RemoveFromDictionary"-->Delete</btn>
         . </div>

        </draggable>
      </div>
    </div>
    <app-footer/>
  </div>

</template>

<script lang="ts">
  import Header from '../Header.vue'
  import Footer from '../Footer.vue'
  import DictionaryInput from './DictionaryInput.vue'
  import draggable from 'vuedraggable'

  export default {
    name: 'RestaurantBillSplitter',
    components: {
      'app-header': Header,
      'app-footer': Footer,
      'restaurantBillSplitter-dictionaryInput': DictionaryInput,    
      draggable
    },
    data() {
      return {
        show: false,
        //MenuItems: [
        //  {
        //    menuItemName: 'Big Mac',
        //    menuItemPrice: '4.00'
        //  },
        //  {
        //    menuItemName: 'Large Fries',
        //    menuItemPrice: '2.50'
        //  }
        //],
        //BillItems: [
        //  {
        //    menuItemName: '',
        //    menuItemPrice: '',
        //    billitemusers:
        //    [
        //      {
        //        billitemusername: ''
        //      }
        //    ]
        //  }
        //]
      }
    },
    methods: {
      log: function () {
        console.log(this.$refs);
      },

      getDictionaryItem: function () {

      }
    },
    computed: {
      MenuItems() {
        return this.$store.state.MenuItems;
      }
       
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
    text-align: center;
  }

  .one {
    grid-column: 1 / 3;
    grid-row: 1 / 4;
    outline: dashed;
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
    }

  .dictionary {
    grid-column: 3;
    grid-row: 2 / 4;
    outline: dashed;
  }

  div.menu-item {
    margin: 10px;
    padding: 10px;
    background-color: aquamarine;
    border-radius: 10px;
  }

    div.menu-item > btn {
      text-align: right;
    }
</style>
