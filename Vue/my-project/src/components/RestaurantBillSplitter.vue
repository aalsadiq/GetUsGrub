<template>
  <div id="top">
    <a href="/#/">
      <img src="../assets/logo.png" />
    </a>
    <nav>
      <ul>
        <li><a href="#/">Home</a></li>
        <li><a>Registration</a></li>
        <li><a href="#/Login">Login</a></li>
        <li><a>Logout</a></li>
        <li><a href="#/RestaurantBillSplitter">Restaurant Bill Splitter</a></li>
      </ul>
    </nav>
    <div>
      <h1>RESTAURANT BILL SPLITTER</h1>
    </div>

    <div class="wrapper">
      <div class="one">
        <draggable v-model="foodItems" :options="{foodItemName:'people'}" @start="drag=true" @end="drag=false">
          <div v-for="element in foodItems" :key="element.id">{{element.foodItemName}}</div>
        </draggable>
      </div>
      <div class="dictionaryInput">
        <p>Enter Food Item Name</p>
        <input type="text" />
        <br />
        <p>Enter Food Item Price</p>
        <input type="number" />
        <br />
        <br />
        <button v-on:click="addToDictionary(foodItemName, foodItemPrice)">Input</button>
      </div>
      <div class="dictionary">
        <draggable v-model="foodItems" :options="{foodItemName:'people'}" @start="drag=true" @end="drag=false">
          <div v-for="element in foodItems" :key="element.id">{{element.foodItemName}}</div>
        </draggable>
      </div>
    </div>
  </div>



</template>

<script>
  import draggable from 'vuedraggable'
  export default {
    name: 'RestaurantBillSplitter',
    components: {
      draggable
    },
    data() {
      return {
        testItem: 'hello',
        foodItems: [
          {
            foodItemName: 'Burger',
            foodItemPrice: '$1.00'
          },
          {
            foodItemName: 'Milk Shake',
            foodItemPrice: '$0.50'
          },
          {
            foodItemName: 'Test',
            foodItemPrice: '$3.00'
          },
        ]
      }
    },
    methods: {
      log: function () {
        console.log(this)
      },

      addToDictionary: function (name, price) {
        this.foodItems.foodItemName = name
        this.foodItems.foodItemPrice = price
      },

      getDictionaryItem: function () {

      }
    },
    computed: {

    }
  }
</script>
<style>
  #top {
    font-family: 'Avenir', Helvetica, Arial, sans-serif;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
    margin-top: 60px;
  }

  .wrapper {
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
</style>
