<template>
  <div>
    <app-header></app-header>
    <btn type="success">Test</btn>
    <template>
      <section>
        <div>
          <btn type="primary" @click="show=!show">Click me!</btn>
        </div>
        <br />
        <collapse v-model="show">
          <div class="well" style="margin-bottom: 0">Hi there.</div>
        </collapse>
      </section>
    </template>

    <div class="wrapper">
      <div class="one">
        <!--        <draggable v-model="dictionaryFoodItems" :options="{dictionaryFoodItemName:'people'}" @start="drag=true" @end="drag=false"> -->
        <div v-for="element in dictionaryFoodItems" :key="element.id">{{element.dictionaryFoodItemName}}: {{element.dictionaryFoodItemPrice}}</div>
        <!--        </draggable> -->
      </div>
      <form class="dictionaryInput">
        <label>Enter Food Item Name</label>
        <input type="text" ref="dictionaryFoodItemName" required />
        <br />
        <label>Enter Food Item Price</label>
        <input type="number" min="0.00" max="1000.00" step="0.01" ref="dictionaryFoodItemPrice" required />
        <br />
        <button v-on:click="addToDictionary">Input</button>
      </form>
      <div class="dictionary">
        <!--<draggable v-model="dictionaryFoodItems" :options="{foodItemName:'people'}" @start="drag=true" @end="drag=false">-->
          <div v-for="element in dictionaryFoodItems" :key="element.id">{{element.dictionaryFoodItemName}}: {{element.dictionaryFoodItemPrice}}</div>
        <!--</draggable>-->
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
        dictionaryFoodItems: [
          {
            dictionaryFoodItemName: 'Test',
            dictionaryFoodItemPrice: '3.50'
          }
        ],
        billFoodItems: []
      }
    },
    methods: {
      log: function () {
        console.log(this)
      },

      addToDictionary: function () {
        console.log(this.$refs);
        this.dictionaryFoodItems.push(
          dictionaryFoodItemName: this.$refs.dictionaryFoodItemName.value,
          dictionaryFoodItemPrice: this.$refs.dictionaryFoodItemPrice.valueAsNumber
        );
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
