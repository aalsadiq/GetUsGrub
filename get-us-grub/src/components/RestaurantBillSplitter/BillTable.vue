<template>
  <div class="bill-table">
    <h1>Your Bill</h1>
    <div>
      <h1 v-if="!BillItems.length"> Drag Items Here!</h1>
      <draggable class="bill" v-bind:list="BillItems" v-bind:options="{group:{ name:'items', pull: false }}" @start="drag=true" @end="drag=false">
        <div class="bill-item" v-for="(billItem, billItemIndex) in BillItems" :key="billItemIndex">
          {{billItem.name}} : ${{billItem.price}}<br />
          <edit-item :editType="editType" :itemIndex="billItemIndex" :Item="billItem" />
          <v-btn small dark color="red" v-on:click="RemoveFromBill(billItemIndex)">Delete</v-btn>
          <manage-users :billItem="billItem"/>
          <v-divider/>
          {{billItem.selected}}
        </div>
      </draggable>
    </div>
    <h2 class="total">Total: {{ TotalPrice }} </h2>
  </div>
</template>

<script>
import EditItem from './EditItem.vue'
import AddBillUser from './AddBillUser.vue'
import ManageUsers from './ManageUsers.vue'
import draggable from 'vuedraggable'
import { VMoney } from 'v-money'

export default {
  name: 'BillTable',
  components: {
    'edit-item': EditItem,
    'add-bill-user': AddBillUser,
    'manage-users': ManageUsers,
    draggable
  },
  directives: { money: VMoney },
  data () {
    return {
      editType: 'bill',
      money: {
        decimal: '.',
        thousands: '',
        prefix: '$',
        suffix: '',
        precision: 2,
        masked: false
      }
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
    BillUsers () {
      return this.$store.state.BillUsers
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

  .total {
    margin-bottom: 10px;
  }
</style>
