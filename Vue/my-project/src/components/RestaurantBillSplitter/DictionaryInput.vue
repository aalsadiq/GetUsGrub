<template>
  <form class="dictionaryInput">
    <label>Enter Food Item Name</label>
    <input type="text" ref="menuItemName" required />
    <br />
    <label>Enter Food Item Price</label>
    <input type="number" min="0.00" max="1000.00" step="0.01" ref="menuItemPrice" required />
    <br />
    <button id="add_to_dictionary" v-on:click="AddToDictionary($refs.menuItemName.value, $refs.menuItemPrice.valueAsNumber)">Add To Dictionary</button>
    <button v-on:click="log">Log</button>
  </form>
</template>

<script lang="ts">
  export default {
    name: 'DictionaryInput',
    components: {
      
    },
    data() {
      return {
      }
    },
    methods: {
      AddToDictionary: function (menuItemName, menuItemPrice) {
        if (this.ValidateDictionaryForm(menuItemName, menuItemPrice)) 
          this.$store.dispatch('AddToDictionary', [menuItemName, menuItemPrice]);
      },
      ValidateDictionaryForm: function (menuItemName, menuItemPrice) {
        if (!menuItemName || !menuItemPrice)
          return false;
        else if (menuItemPrice <= this.$store.state.MINIMUM_MENU_ITEM_PRICE || menuItemPrice > this.$store.state.MAX_MENU_ITEM_PRICE)
          return false;
        else
          return true;  
      }
      log: function () {
        console.log(this.$refs);
      }
    },
    computed: {
      MenuItems() {
        return this.$store.state.MenuItems;
      }
    }
  }
</script>

<style scoped>
  .dictionaryInput {
    grid-column: 3;
    grid-row: 1;
    outline: dashed;
    padding: 20px;
  }
</style>
