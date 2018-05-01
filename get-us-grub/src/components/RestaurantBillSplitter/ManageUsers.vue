<template>
  <v-dialog v-model="dialog" scrollable max-width="500px">
    <v-btn small dark color="green" slot="activator">Manage Users</v-btn>
    <v-card>
      <v-card-title>Selects Users to Split With</v-card-title>
      <h2>{{ billItem.itemName }} : ${{ (billItem.itemPrice / 100).toFixed(2) }}</h2>
      <v-divider />
      <v-card-text>
        <div v-for="(billUser, billUserIndex) in billUsers"
             :key="billUserIndex"
             class="user-controls">
          <v-checkbox :label="billUser.name"
                      :value="billUser.uID"
                      v-model="billItem.selected"
                      class="user-control1">
          </v-checkbox>
          <div v-if="billItem.selected.includes(billUser.uID)"
               class="user-control2">
            <v-switch :value="billUser.uID"
                      :label="`Split Manually?`"
                      v-model="billItem.selectedManual" />
          </div>
          <v-text-field v-if="billItem.selected.includes(billUser.uID) && billItem.selectedManual.includes(billUser.uID)"
                        mask="##%"
                        v-model="billItem.manual[billItem.manual.findIndex(x => x.manualUID === billUser.uID)].percentage"
                        @input="updatePercentage(billItem, billItemIndex, billUserIndex)"
                        class="user-control3">
          </v-text-field>
          <v-divider />
        </div>
      </v-card-text>
      <v-card-text style="display: inline-block">
        <add-bill-user class="add-bill-user" />
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
import { VMoney } from 'v-money'

export default {
  name: 'ManageUsers',
  components: {
    'add-bill-user': AddBillUser
  },
  data () {
    return {
      oldPercentages: [{}],
      dialog: false,
      money: {
        decimal: '.',
        thousands: '',
        prefix: '',
        suffix: '',
        precision: 2,
        masked: false
      }
    }
  },
  directives: { money: VMoney },
  props: ['billItem', 'billItemIndex'],
  watch: {
    selected (newSelected, oldSelected) {
      if (this.dialog === true) {
        // if (oldSelected.length === 0 && newSelected.length === 1) {
        // } else if (newSelected.length > oldSelected.length) {
        //   var addedUserUID = Number(newSelected.filter(x => !oldSelected.includes(x)))
        //   console.log(addedUserUID)
        // } else if (oldSelected.length > newSelected.length) {
        //   var removedUserUID = Number(oldSelected.filter(x => !newSelected.includes(x)))
        //   console.log(removedUserUID)
        // } else if ()

        // TODO: @RYAN UPDATE YOUR BUSINESS LOGIC IN HERE, NOT THE STORE, SO YOU DON'T HAVE SO MANY IF STATEMENTS -RYAN
        if (oldSelected.length > newSelected.length) {
          var removedUID = Number(oldSelected.filter(x => !newSelected.includes(x)))
          if (this.billItem.selectedManual.includes(removedUID)) {
            this.removeSelectedManual(this.billItemIndex, this.billItem, newSelected, oldSelected)
            this.removeManual(this.billItemIndex, this.billItem, newSelected, oldSelected)
          }
        }
        this.updateUserMoneyOwesFromSelected(this.billItemIndex, this.billItem, newSelected, oldSelected)
      }
    },
    selectedManual (newSelectedManual, oldSelectedManual) {
      if (this.dialog === true) {
        if (oldSelectedManual.length === 0 && newSelectedManual.length === 1) {
          this.addManual(this.billItemIndex, this.billItem, newSelectedManual, oldSelectedManual)
        } else if (newSelectedManual.length === 0 && oldSelectedManual.length === 1) {
          this.removeManual(this.billItemIndex, this.billItem, newSelectedManual, oldSelectedManual)
        } else if (newSelectedManual.length > oldSelectedManual.length) {
          this.addManual(this.billItemIndex, this.billItem, newSelectedManual, oldSelectedManual)
          // this.updateSelectedManual(this.billItemIndex, this.billItem, newSelectedManual, oldSelectedManual)
        } else if (newSelectedManual.length < oldSelectedManual.length) {
          this.removeManual(this.billItemIndex, this.billItem, newSelectedManual, oldSelectedManual)
        }
      }
    }
  },
  methods: {
    updateUserMoneyOwesFromSelected: function (billItemIndex, billItem, newSelected, oldSelected) {
      var totalPercentage = this.getTotalPercentage(billItemIndex)
      this.$store.dispatch('updateUserMoneyOwesFromSelected', { billItemIndex, billItem, newSelected, oldSelected, totalPercentage })
    },
    addManual: function (billItemIndex, billItem, newSelectedManual, oldSelectedManual) {
      var newUID = Number(newSelectedManual.filter(x => !oldSelectedManual.includes(x)))
      var billUserIndex = Number(this.billUsers.findIndex(x => x.uID === newUID))
      var totalPercentage = this.getTotalPercentage(billItemIndex)
      this.oldPercentages.push({ uID: newUID, oldPercentage: 0 })
      this.$store.commit('addManual', { billItemIndex, billItem, billUserIndex, newUID, totalPercentage })
    },
    removeManual: function (billItemIndex, billItem, newSelectedManual, oldSelectedManual) {
      var removedUID = Number(oldSelectedManual.filter(x => !newSelectedManual.includes(x)))
      var billUserIndex = Number(this.billUsers.findIndex(x => x.uID === removedUID))
      var index = Number(this.oldPercentages.findIndex(x => x.uID === removedUID))
      var oldPercentage = Number(this.oldPercentages[index].oldPercentage)
      var totalPercentage = this.getTotalPercentage(billItemIndex) // This should be replaced
      this.oldPercentages.splice(index, 1)
      this.$store.commit('removeManual', { billItemIndex, billItem, billUserIndex, removedUID, oldPercentage, totalPercentage })
    },
    removeSelectedManual: function (billItemIndex, billItem, newSelectedManual, oldSelectedManual) {
      var removedUID = Number(oldSelectedManual.filter(x => !newSelectedManual.includes(x)))
      this.$store.commit('removeSelectedManual', { billItemIndex, billItem, removedUID })
    },
    updateSelectedManual: function (billItemIndex, billItem, newSelectedManual, oldSelectedManual) {
      this.$store.commit('updateSelectedManual', { billItemIndex, billItem, newSelectedManual, oldSelectedManual })
    },
    updatePercentage: function (billItem, billItemIndex, billUserIndex) {
      var oldPercentage = this.oldPercentages[this.oldPercentages.findIndex(x => x.uID === this.billUsers[billUserIndex].uID)].oldPercentage
      var newPercentage = this.billItems[billItemIndex].manual[this.billItems[billItemIndex].manual.findIndex(x => x.manualUID === this.billUsers[billUserIndex].uID)].percentage
      if (!newPercentage) {
        newPercentage = 0
      }
      var totalPercentage = this.getTotalPercentage(billItemIndex)
      this.oldPercentages[this.oldPercentages.findIndex(x => x.uID === this.billUsers[billUserIndex].uID)].oldPercentage = newPercentage
      this.$store.commit('updatePercentage', { billItem, billItemIndex, billUserIndex, newPercentage, oldPercentage, totalPercentage })
    },
    emitUsersInBillItemEvent: function (billItem) {
      EventBus.$emit('users-in-bill-item', billItem)
    },
    getTotalPercentage: function (billItemIndex) {
      return this.$store.getters.getTotalPercentage(billItemIndex)
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
    billItems () {
      return this.$store.state.billItems
    },
    billUsers () {
      return this.$store.state.billUsers
    },
    selected () {
      return this.billItem.selected
    },
    selectedManual () {
      return this.billItem.selectedManual
    },
    manual () {
      return this.billItem.manual
    }
  }
}
</script>

<style scoped>
  .user-controls {
    display: grid;
    grid-template-columns: auto auto;
    grid-template-rows: auto;
  }

  .user-control1 {
    grid-column: 1;
    grid-row: 1;
  }

  .user-control2 {
    grid-column: 2;
    grid-row: 1;
  }

  .user-control3 {
    grid-row: 2;
  }

  .add-bill-user {
    position: absolute;
    bottom: 0;
    width: auto;
    max-width: 200px;
    left: 50%;
    margin-left: -100px;
  }

</style>
