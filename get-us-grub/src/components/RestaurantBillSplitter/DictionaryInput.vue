<template>
  <v-form :rules=ValidatePrice class="dictionaryInput">
    <label>Enter Food Item Name</label>
    <v-text-field type="text" ref="menuItemName" required />
    <br />
    <label>Enter Food Item Price</label>
    <v-text-field prefix="$" type="number" min="0.00" max="1000.00" step="0.01" ref="menuItemPrice" required />
    <br />
    <v-btn v-on:click="AddToDictionary($refs.menuItemName.value, $refs.menuItemPrice.valueAsNumber)">Add To Dictionary</v-btn>
    <v-btn v-on:click="log">Log</v-btn>
  </v-form>
</template>

<script>
import axios from 'axios'
export default {
  name: 'DictionaryInput',
  components: {
    axios
  },
  data () {
    return {
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
