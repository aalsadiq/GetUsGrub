<template>
  <div>
    <app-header></app-header>
    <btn type="success">Test</btn>

    <div class="wrapper">
      <div class="one">
        <draggable class="bill" v-model="BillItems" :options="{group:'people'}" @start="drag=true" @end="drag=false">
          <div class="bill-item" v-for=" (element, index) in BillItems" :key="index">
            {{index}} - {{element.menuItemName}} : ${{element.menuItemPrice}}
            <btn type="primary" size="xs" v-on:click="EditDictionaryFoodItem">Edit</btn>
            <btn type="danger" size="xs" v-on:click="removeFromBill">Delete</btn>
          </div>
        </draggable>
      </div>
      <form class="dictionaryInput">
        <label>Enter Food Item Name</label>
        <input type="text" ref="menuItemName" required />
        <br />
        <label>Enter Food Item Price</label>
        <input type="number" min="0.00" max="1000.00" step="0.01" ref="menuItemPrice" required />
        <br />
        <button id="add_to_dictionary" v-on:click="addToDictionary">Add To Dictionary</button>
      </form>
      <div class="dictionary">
        <h2>Dictionary</h2>
        <draggable class="menu" :clone="clone" v-model="MenuItems" :options="{group:{ name:'people',  pull:'clone', put:false }}" @start="drag=true" @end="drag=false">

          <div class="menu-item" v-for="(element, index) in MenuItems" :key="element.id">
            {{element.menuItemName}} : ${{element.menuItemPrice}}
            <btn type="primary" size="xs" v-on:click="EditDictionaryFoodItem">Edit</btn>
            <btn type="danger" size="xs" v-on:click="removeFromDictionary">Delete</btn>
          </div>

        </draggable>
      </div>
    </div>
    <app-footer></app-footer>
  </div>

</template>

<script lang="ts">
  import Header from '../Header.vue'
  import Footer from '../Footer.vue'
  import draggable from 'vuedraggable'

  export default {
    name: 'RestaurantBillSplitter',
    components: {
      'app-header': Header,
      'app-footer': Footer,
      draggable
    },
    data() {
      return {
        show: false,
        MenuItems: [
          {
            menuItemName: 'McDouble',
            menuItemPrice: '1.50'
          },
          {
            menuItemName: 'Big Mac',
            menuItemPrice: '3.50'
          }
        ],
        BillItems: [
          {
            menuItemName: '',
            menuItemPrice: ''
            //billItemUsers:
            //[
            //  {
            //    billItemUsername: ''
            //  }
            //]
          }
        ]
      }
    },
    methods: {
      log: function () {
        console.log(this.$refs);
      },

      addToDictionary: function () {
        if (this.$refs.MenuItemName.value.length == 0 || this.$refs.MenuItemPrice.valueAsNumber.length == 0)
          document.getElementById('add_to_dictionary').disabled = true;
        this.MenuItems.push(
          {
            MenuItemName: this.$refs.MenuItemName.value,
            MenuItemPrice: this.$refs.MenuItemPrice.valueAsNumber
          }
        );
      },

      removeFromDictionary: function () {
        this.MenuItems.pop();
      },

      getDictionaryItem: function () {

      }
    },
    computed: {

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

  .dictionaryInput {
    grid-column: 3;
    grid-row: 1;
    outline: dashed;
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
