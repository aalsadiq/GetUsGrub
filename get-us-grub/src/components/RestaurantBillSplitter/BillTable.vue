<template>
  <div class="bill-table">
    <h1>Your Bill</h1>
    <div>
      <h1 v-if="!billItems.length"> Drag Items Here!</h1>
      <draggable class="bill" v-bind:list="billItems" v-bind:options="{group:{ name:'items', pull: false }}" @start="drag=true" @end="drag=false">
        <div class="bill-item" v-for="(billItem, billItemIndex) in billItems" :key="billItemIndex">
          {{billItem.name}} : ${{billItem.price}}<br />
          <edit-item :editType="editType" :itemIndex="billItemIndex" :Item="billItem" />
          <delete-item :deleteType="deleteType" :itemIndex="billItemIndex" />
          <manage-users :billItem="billItem" />
          <v-divider />
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
    <h2 class="total">Total: {{ this.money.prefix }}{{ totalPrice }} </h2>
  </div>
</template>

<script>
import EditItem from './EditItem.vue'
import DeleteItem from './DeleteItem.vue'
import AddBillUser from './AddBillUser.vue'
import ManageUsers from './ManageUsers.vue'
import draggable from 'vuedraggable'
import { VMoney } from 'v-money'

export default {
  name: 'BillTable',
  components: {
    'edit-item': EditItem,
    'delete-item': DeleteItem,
    'add-bill-user': AddBillUser,
    'manage-users': ManageUsers,
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
  .bill-table {
    grid-column: 2 / 3;
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
