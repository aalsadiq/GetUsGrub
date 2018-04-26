<template>
  <v-dialog v-model="dialog" scrollable max-width="300px">
    <v-btn small dark color="green" slot="activator">Manage Users</v-btn>
    <v-card>
      <v-card-title>Selects Users to Split With</v-card-title>
      <v-card-text>{{billItem.name}} {{billItem.price}}</v-card-text>
      <v-divider />
      <v-card-text>
        <v-checkbox v-for="(billUser, billUserIndex) in billUsers"
                    :key="billUserIndex"
                    :label="billUser.name"
                    :value="billUser.uID"
                    v-model="billItem.selected">
        </v-checkbox>
      </v-card-text>
      <v-divider />
      <v-card-text style="display: inline-block">
        <add-bill-user />
      </v-card-text>
      <v-divider />
      <v-card-actions>
        <v-btn color="blue" dark v-on:click.native="dialog = false">Close</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import AddBillUser from './AddBillUser.vue'
import { EventBus } from '@/event-bus/event-bus.js'

export default {
  name: 'ManageUsers',
  components: {
    'add-bill-user': AddBillUser
  },
  data () {
    return {
      dialog: false
    }
  },
  props: ['billItem', 'billItemIndex'],
  watch: {
    selected(newSelected, oldSelected) {
      if (this.dialog === true) {
        console.log('Old: ' + oldSelected)
        console.log('New: ' + newSelected)
        console.log('Bill Item Index: ' + this.billItemIndex)
        console.log('Temp Index: ' + this.tempBillItemIndex + ' of ' + this.billItem.itemName)
        //this.updateBillItemsInUser()
        this.updateUserMoneyOwesFromSelected(this.billItemIndex, this.billItem, newSelected, oldSelected)
        console.log('New Temp Index: ' + this.tempBillItemIndex + ' of ' + this.billItem.itemName)}      
    }
  },
  methods: {
    updateUserMoneyOwesFromSelected: function (billItemIndex, billItem, newSelected, oldSelected) {
      this.$store.dispatch('updateUserMoneyOwesFromSelected', { billItemIndex, billItem, newSelected, oldSelected })
    },
    // updateBillItemsInUser: function () {
       
    // },
    emitUsersInBillItemEvent: function (billItem) {
      EventBus.$emit('users-in-bill-item', billItem)
    }
  },
  created () {
    this.billItemSelected = this.billItem.selected // Used to keep track of the this.billItem.selected array BEFORE the changes
  },
  updated () {
    this.emitUsersInBillItemEvent(this.billItem)
    this.billItemSelected = this.billItem.selected
  },
  computed: {
    billUsers () {
      return this.$store.state.billUsers
    },
    selected () {
      return this.billItem.selected
    }
  }
}
</script>

<style>
</style>
