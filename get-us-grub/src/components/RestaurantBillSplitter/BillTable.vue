<template>
  <div class="bill-table">
    <h1>Your Bill</h1>
    <v-divider />
    <h2 class="total">Total: {{ this.money.prefix }}{{ (totalPrice / 100).toFixed(2) }} </h2> <!-- TODO: I've yet to figure out how to display the html element using functions to convert -->
    <div>
      <manage-tips/>
    </div>
    <div class="bill-items">
      <h1 v-if="!billItems.length"> Drag Items Here!</h1>
      <draggable class="bill" v-bind:list="billItems" v-bind:options="{group:{ name:'items', pull: false }}" @start="drag=true" @end="drag=false">
        <div class="bill-item" v-for="(billItem, billItemIndex) in billItems" :key="billItemIndex">
          <!-- <bill-table-pie-chart :billItem="billItem" :billItemIndex="billItemIndex" :width="50" :height="50" /> -->
          <div class="bill-item-controls">
            <h2> {{ billItem.itemName }} : ${{ (billItem.itemPrice / 100).toFixed(2) }} </h2>
            <br />
            <ul style="list-style-type: none">
              <li>
                <edit-item :editType="editType" :itemIndex="billItemIndex" :Item="billItem" />
              </li>
              <li>
                <delete-item :deleteType="deleteType" :itemIndex="billItemIndex" />
              </li>
              <li>
                <manage-users :billItem="billItem" :billItemIndex="billItemIndex"/>
              </li>
            </ul>
          </div>
          <div v-for="billUser in billUsers" :key="billUser">
            <div v-for="uniqueID in billItem.selected" :key="uniqueID">
              <div v-if="billUser.uID === uniqueID">
                <p> {{ billUser.name }} </p>
              </div>
            </div>
          </div>
        </div>
      </draggable>
    </div>
  </div>
</template>

<script>
import ManageTips from './ManageTips.vue'
import EditItem from './EditItem.vue'
import DeleteItem from './DeleteItem.vue'
import AddBillUser from './AddBillUser.vue'
import ManageUsers from './ManageUsers.vue'
import draggable from 'vuedraggable'
import { VMoney } from 'v-money'
import BillTablePieChart from './BillTablePieChart'

export default {
  name: 'BillTable',
  components: {
    'manage-tips': ManageTips,
    'edit-item': EditItem,
    'delete-item': DeleteItem,
    'add-bill-user': AddBillUser,
    'manage-users': ManageUsers,
    'bill-table-pie-chart': BillTablePieChart,
    draggable
  },
  directives: { money: VMoney },
  data () {
    return {
      editType: 'BillTable',
      deleteType: 'BillTable',
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
  },
  computed: {
    billItems () {
      return this.$store.state.billItems
    },
    billUsers () {
      return this.$store.state.billUsers
    },
    totalPrice () {
      return this.$store.getters.totalPrice
    }
  }
}
</script>

<style>
  bill-table-pie-chart {
    max-height: 100px;
    grid-column: 0;
  }

  bill-item-controls {
    grid-column: 1;
  }

    .bill-table > .bill-items {
      outline: solid;
      margin: 20px;
      background-color: grey;
    }

    .bill-table > h1 {
      text-align: center;
    }

  .bill {
    display: grid;
    grid-template-columns: auto auto;
    min-height: 10px;
    min-width: 0;
  }

    .bill > .bill-item {
      display: grid;
      grid-template-columns: 50% 50%;
      margin: 10px;
      padding: 10px;
      background-color: aquamarine;
      border-radius: 10px;
      min-width: 0;
      min-height: 0;
      overflow: hidden;
    }

  .total {
    margin-bottom: 10px;
  }
</style>
