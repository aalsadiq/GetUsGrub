<template>
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
                  :rules="[rules.required, rules.nonzero, rules.max]"
                  prefix="$"
                  ref="priceField"
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
export default {
  name: 'DictionaryInput',
  components: {
    axios
  },
  // directives: { money: VMoney },
  data () {
    return {
      valid: true,
      maxValue: 1000,
      name: '',
      price: null,
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
  methods: {
    AddToDictionary: function (menuItemName, menuItemPrice) {
      if (this.ValidateDictionaryForm(menuItemName, menuItemPrice)) {
        this.$store.dispatch('AddToDictionary', [menuItemName, menuItemPrice])
      }
    },
    ValidateDictionaryForm: function (menuItemName, menuItemPrice) {
      if (!menuItemName || !menuItemPrice) {
        return false
      } else if (menuItemPrice <= this.$store.state.MINIMUM_MENU_ITEM_PRICE || menuItemPrice > this.$store.state.MAX_MENU_ITEM_PRICE) {
        return false
      } else {
        return true
      }
    },
    ValidatePrice: function () {
      return true
    },
    log: function () {
      axios.get('https://jsonplaceholder.typicode.com/posts/1')
        .then(function (response) {
          console.log(response)
        })
        .catch(function (error) {
          console.log(error)
        })
      console.log(this.$refs)
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
    outline: dashed;
    padding: 5px;
  }
</style>
