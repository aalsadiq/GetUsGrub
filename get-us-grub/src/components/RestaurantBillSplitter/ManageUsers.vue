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
                    v-model="billItem.selected"></v-checkbox>
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
  methods: {
    updateUserMoneyOwes: function (billItemIndex) {
      this.$store.dispatch('updateUserMoneyOwes', billItemIndex)
    },
    emitUsersInBillItemEvent: function (billItem) {
      EventBus.$emit('users-in-bill-item', billItem)
    }
  },
  updated() {
    this.updateUserMoneyOwes(this.billItemIndex)
    this.emitUsersInBillItemEvent(this.billItem)
  },
  computed: {
    billUsers () {
      return this.$store.state.billUsers
    }
  }
}
</script>

<style>
</style>
