<template>
  <div class="bill-table">
    <h1>Your Bill</h1>
    <div>
      <h1 v-if="!BillItems.length"> Drag Items Here!</h1>
      <draggable class="bill" v-bind:list="BillItems" v-bind:options="{group:'people'}" @start="drag=true" @end="drag=false">
        <div class="bill-item" v-for="(element, index) in BillItems" :key="index">
          {{index}} - {{element.menuItemName}} : ${{element.menuItemPrice}}<br />
          <div style="display: inline-block">
            <v-btn small><!--v-on:click="EditDictionaryFoodItem"-->Edit</v-btn>
            <v-btn small v-on:click="RemoveFromBill(index)">Delete</v-btn>
            <v-btn small>Manage Users</v-btn>
          </div>
        </div>
      </draggable>
    </div>
    <h2 class="total">Total: <span v-money="money" /></h2>
  </div>
</template>

<script>
import draggable from 'vuedraggable'
import { VMoney } from 'v-money'

export default {
  name: 'BillTable',
  components: {
    draggable
  },
  directives: { money: VMoney },
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
    TotalPrice () {
      return this.$store.getters.totalPrice
    }
  }
}
</script>

<style>
  .bill-table {
    grid-column: 1 / 3;
    grid-row: 1 / 4;
    outline: solid;
  }

    .bill-table > div {
      outline: solid;
      margin: 20px;
      background-color: grey;
    }

    .bill-table > h1 {
      text-align: center;
    }

  .bill {
    min-height: 20px;
  }

    .bill > .bill-item {
      margin: 10px;
      padding: 10px;
      background-color: aquamarine;
      border-radius: 10px;
      text-align: center;
    }
</style>
