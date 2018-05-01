<!--<template>
  <div>
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
                  class="user-control3">
    </v-text-field>
  </div>
</template>

<script>
import { EventBus } from '@/event-bus/event-bus.js'
import { VMoney } from 'v-money'

export default {
  name: 'BillUser',
  props: ['dialog', 'billItem', 'billItemIndex', 'billUser', 'billUserIndex'],
  data() {
    return {
      newDialog: dialog,
      oldPercentage: 0,
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
  directives: {
    money: VMoney
  },
  watch: {
    selected(newSelected, oldSelected) {
      if (this.newDialog === true) {
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
      if (this.newDialog === true) {
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
    },
    manual: {
      handler: function (newPercentage, oldPercentage) {
        console.log(oldPercentage[0].percentage)
        console.log(newPercentage[0].percentage)
      },
      deep: true
    },
  },
  methods: {
    updateUserMoneyOwesFromSelected: function (billItemIndex, billItem, newSelected, oldSelected) {
      this.$store.dispatch('updateUserMoneyOwesFromSelected', { billItemIndex, billItem, newSelected, oldSelected })
    },
    addManual: function (billItemIndex, billItem, newSelectedManual, oldSelectedManual) {
      var newUID = Number(newSelectedManual.filter(x => !oldSelectedManual.includes(x)))
      this.$store.commit('addManual', { billItemIndex, billItem, newUID })
    },
    removeManual: function (billItemIndex, billItem, newSelectedManual, oldSelectedManual) {
      var removedUID = Number(oldSelectedManual.filter(x => !newSelectedManual.includes(x)))
      this.$store.commit('removeManual', { billItemIndex, billItem, removedUID })
    },
    removeSelectedManual: function (billItemIndex, billItem, newSelectedManual, oldSelectedManual) {
      var removedUID = Number(oldSelectedManual.filter(x => !newSelectedManual.includes(x)))
      this.$store.commit('removeSelectedManual', { billItemIndex, billItem, removedUID })
    },
    updateSelectedManual: function (billItemIndex, billItem, newSelectedManual, oldSelectedManual) {
      this.$store.commit('updateSelectedManual', { billItemIndex, billItem, newSelectedManual, oldSelectedManual })
    },
    updatePercentage: function (billItemIndex, billUserIndex) {
      var newPercentage = this.billItems[billItemIndex].manual[this.billItems[billItemIndex].manual.findIndex(x => x.manualUID === this.billUsers[billUserIndex].uID)].percentage
      var totalPercentage = this.getTotalPercentage(billItemIndex)
      console.log('Bill User Index ' + billUserIndex)
      console.log('Bill Item Index ' + billItemIndex)
      console.log('New Percentage ' + newPercentage)
      console.log('Total Percentage ' + totalPercentage)
    },
    emitUsersInBillItemEvent: function (billItem) {
      EventBus.$emit('users-in-bill-item', billItem)
    },
    getTotalPercentage: function (billItemIndex) {
      return this.$store.getters.getTotalPercentage(billItemIndex)
    }
  },
  computed: {
    billItems() {
      return this.$store.state.billItems
    },
    billUsers() {
      return this.$store.state.billUsers
    },
    selected() {
      return this.billItem.selected
    },
    selectedManual() {
      return this.billItem.selectedManual
    },
    manual() {
      return this.billItem.manual
    }
  }
}
</script>

<style scoped>
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
</style>-->
