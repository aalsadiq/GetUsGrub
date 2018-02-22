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
      log: function () {
        console.log(this.$refs);
      },

      AddToDictionary: function (menuItemName, menuItemPrice) {
        this.$store.dispatch('AddToDictionary', [menuItemName, menuItemPrice]);
      },

      RemoveFromDictionary: function () {
        console.log(this.$refs);
      },

      getDictionaryItem: function () {

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
  }
</style>
