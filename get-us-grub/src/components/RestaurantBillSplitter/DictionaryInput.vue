<template>
  <v-dialog v-model="dialog" scrollable max-width="300px">
    <v-btn small dark slot="activator">Add Food Item</v-btn>
    <v-card>
      <v-card-title> <h2>Enter Your Food Item</h2></v-card-title>
      <v-card-text>
        <v-form v-model="valid"
                ref="dictionaryInputForm"
                class="dictionaryInput"
                lazy-validation>
          <v-text-field label="Item Name"
                        :rules="[rules.required]"
                        ref="nameField"
                        v-model="name"
                        required />
          <v-text-field label="Item Price"
                        :rules="[rules.required, rules.nonzero, rules.max, rules.nonnegative]"
                        prefix="$"
                        ref="priceField"
                        v-model.number="price"
                        v-money="money"
                        required />
          <v-btn color="teal"
                 dark
                 v-on:click="AddToDictionary(name, price)">
            Add To Dictionary
          </v-btn>
          <v-btn color="teal"
                 dark
                 v-on:click="dialog = false">
            Close
          </v-btn>
          <br /><small>*indicates required field</small>
        </v-form>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import axios from 'axios'
import { VMoney } from 'v-money'
export default {
  name: 'DictionaryInput',
  components: {
    axios
  },
  // directives: { money: VMoney },
  data () {
    return {
      valid: true,
      maxValue: 1000.00,
      name: '',
      price: null,
      dialog: false,
      rules: {
        required: (value) => (!!value) || 'Required.',
        nonzero: (value) => value !== 0 || 'Price must not be 0.',
        // TODO: make max rule more extensible. -Ryan Luong
        max: (value) => value < this.maxValue || ('Price must be less than ' + this.maxValue + '.'),
        nonnegative: (value) => !(value < 0) || ('Price must not be less than 0.')
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
    AddToDictionary: function (name, price) {
      if (this.$refs.dictionaryInputForm.validate()) {
        console.log('Add Form Validated')
        this.$store.dispatch('addToDictionary', [name, price])
      }
    },
    ValidatePrice: function () {
      return true
    },
    log: function () {
      console.log(this.$refs.dictionaryInputForm)
    }
  },
  computed: {
  }
}
</script>

<style scoped>
  .dictionaryInput {
  }
</style>
