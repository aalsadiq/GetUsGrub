<template>
  <v-dialog v-model="editDialog" scrollable max-width="300px">
    <v-btn small dark color="blue" slot="activator" v-on:click="UpdateTextFields">Edit</v-btn>
    <v-card>
      <v-card-title><h2> {{Item.name}} : ${{Item.price}} </h2></v-card-title>
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
                        v-model="newItemPrice"
                        v-money="money"
                        required />
          <v-btn color="blue"
                 dark
                 v-on:click.native="editDialog = false">
            Close
          </v-btn>
          <v-btn color="blue" dark v-on:click="EditFoodItem(editType, itemIndex, newItemName, newItemPrice)">Save</v-btn>
          <v-btn v-on:click="Log">Log</v-btn>
          <br /><small>*indicates required field</small>
        </v-form>
      </v-card-text>
      <v-divider />
    </v-card>
  </v-dialog>
</template>

<script>
import { VMoney } from 'v-money'

export default {
  name: 'EditItem',
  components: {
  },
  props: ['editType', 'itemIndex', 'Item'],
  data () {
    return {
      newItemName: this.Item.name,
      newItemPrice: this.Item.price,
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
    EditFoodItem: function (editType, itemIndex, newItemName, newItemPrice) {
      if (this.$refs.editForm.validate()) {
        console.log('Index: ' + itemIndex)
        console.log('New Name: ' + newItemName)
        console.log('New Price: ' + newItemPrice)
        if (editType === 'Dictionary') {
          console.log('Dictionary Item Editted')
          this.$store.dispatch('EditDictionaryItem', [itemIndex, newItemName, newItemPrice])
        } else if (editType === 'BillTable') {
          console.log('Bill Item Editted')
          this.$store.dispatch('EditBillItem', [itemIndex, newItemName, newItemPrice])
        }
      }
    },
    UpdateTextFields: function () {
      this.newItemName = this.Item.name
      this.newItemPrice = this.Item.price
    },
    Log: function () {
      console.log(this.newItemName)
    }
  },
  computed: {
    MenuItems () {
      return this.$store.state.MenuItems
    }
  }
}
</script>

<style>

</style>
