<template>
  <v-form v-model="valid"
          ref="dictionaryInputForm"
          class="dictionaryInput"
          lazy-validation>
    <v-text-field label="Item Name"
                  :rules="[rules.required]"
                  v-model="name"
                  required />
    <v-text-field label="Item Price"
                  :rules="[rules.required, rules.nonzero, rules.max]"
                  prefix="$"
                  v-model.lazy="price"
                  v-money="money"
                  required />
    <v-btn color="teal"
           dark
           v-on:click="AddToDictionary(name, price)" >
      Add To Dictionary
    </v-btn>
    <v-btn color="teal"
           dark
           v-on:click="log">
      Log
    </v-btn>
    <br /><small>*indicates required field</small>
  </v-form>
</template>

<script>
import axios from 'axios'
import { VMoney } from 'v-money'

export default {
  name: 'DictionaryInput',
  components: {
    axios
  },
  data () {
    return {
      valid: true,
      maxValue: 1000,
      name: '',
      price: null,
      rules: {
        required: (value) => (!!value) || 'Required.',
        nonzero: (value) => value != 0 || 'Price must not be 0.',
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
    AddToDictionary: function (name, price) {
      if (this.$refs.dictionaryInputForm.validate()) {
        console.log('Add Form Validated')
        this.$store.dispatch('AddToDictionary', [name, price])
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
    MenuItems () {
      return this.$store.state.MenuItems
    }
  }
}
</script>

<style scoped>
  .dictionaryInput {
    grid-column: 3;
    grid-row: 1;
    outline: solid;
    padding: 0 20px 0 20px;
  }
</style>
