<template>
  <form class="dictionaryInput">
    <label>Enter Food Item Name</label>
    <input type="text" ref="menuItemName" required />
    <br />
    <label>Enter Food Item Price</label>
    <input type="number" min="0.00" max="1000.00" step="0.01" ref="menuItemPrice" required />
    <br />
    <button id="add_to_dictionary" v-on:click="AddToDictionary($refs.menuItemName.value, $refs.menuItemPrice.valueAsNumber)">Add To Dictionary</button>
  </form>
</template>

<script lang="ts">
  export default {
    name: 'DictionaryInput',
    components: {
      
    },
    data() {
      return {
        show: false
      }
    },
    methods: {
      AddToDictionary: function (menuItemName, menuItemPrice) {
        // Parameters have to be placed into an array because dispatch can only take two. The name and the payload.
        this.$store.dispatch('AddToDictionary', [menuItemName, menuItemPrice]);
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
