<template>
  <v-dialog v-model="editDialog" scrollable max-width="300px">
    <v-btn small dark color="blue" slot="activator" v-on:click="UpdateTextFields">Edit</v-btn>
    <v-card>
      <v-card-title><h2> {{Item.itemName}} : ${{Item.itemPrice / 100}} </h2></v-card-title>
      <v-divider />
      <v-card-text>
        <v-form v-model="valid"
                ref="editForm"
                lazy-validation>
          <v-text-field label="Enter New Name"
                        :rules="[rules.required]"
                        v-model="newItemName"
                        required />
          <v-text-field label="Enter New Price"
                        prefix="$"
                        :rules="[rules.required, rules.nonzero, rules.max]"
                        v-model.number="newItemPrice"
                        v-money="money"
                        required />
          <v-btn color="blue"
                 dark
                 v-on:click.native="editDialog = false">
            Close
          </v-btn>
          <v-btn color="blue" dark v-on:click="EditFoodItem(editType, itemIndex, Item, newItemName, newItemPrice)">Save</v-btn>
          <br /><small>*indicates required field</small>
        </v-form>
      </v-card-text>
      <v-divider />
    </v-card>
  </v-dialog>
</template>

<script>
import { VMoney } from 'v-money'
// import { EventBus } from '@/event-bus/event-bus.js'

export default {
  name: 'EditItem',
  components: {
  },
  props: ['editType', 'itemIndex', 'Item'],
  data () {
    return {
      newItemName: this.Item.itemName,
      newItemPrice: this.Item.itemPrice,
      editDialog: false,
      valid: true,
      rules: {
        required: (value) => (!!value) || 'Required.',
        nonzero: (value) => value !== 0 || 'Price must not be 0.',
        // TODO: make max rule more extensible. -Ryan Luong
        max: (value) => value < 1000.00 || ('Price must be less than 1000.')
      },
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
  methods: {
    EditFoodItem: function (editType, itemIndex, Item, newItemName, newItemPrice) {
      if (this.$refs.editForm.validate()) {
        newItemPrice = this.convertFromUSDtoInt(newItemPrice)
        if (editType === 'Dictionary') {
          this.$store.dispatch('editDictionaryItem', [itemIndex, newItemName, newItemPrice])
        } else if (editType === 'BillTable') {
          this.$store.dispatch('updateUserMoneyOwesFromEditItem', { itemIndex, Item, newItemPrice })
          this.$store.dispatch('editBillItem', [itemIndex, newItemName, newItemPrice])
        }
      }
    },
    convertFromUSDtoInt: function (usDollars) {
      return this.$store.getters.convertFromUSDtoInt(usDollars)
    },
    UpdateTextFields: function () {
      this.newItemName = this.Item.itemName
      this.newItemPrice = this.Item.itemPrice
    }
  },
  computed: {
  }
}
</script>

<style scoped>
</style>
