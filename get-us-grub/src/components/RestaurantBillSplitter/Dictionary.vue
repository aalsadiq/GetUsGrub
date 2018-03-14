<template>
  <div class="dictionary">
    <h2>Dictionary</h2>
    <draggable class="menu" v-bind:list="MenuItems" v-bind:options="{group:{ name:'people',  pull:'clone', put:false }}" @start="drag=true" @end="drag=false">
      <div class="menu-item" v-for="(element, index) in MenuItems" :key="element">
        {{element.menuItemName}} : ${{element.menuItemPrice.toFixed(2)}}<br />
        <v-btn small=true v-on:click="edit">
          Edit
        </v-btn>
        <v-btn small=true v-on:click="RemoveFromDictionary(index)">
          Delete
        </v-btn>
        <form v-if="edit">
          <label>Enter Food Item Name</label>
          <input type="text" ref="menuItemName" required />
          <br />
          <label>Enter Food Item Price</label>
          <input type="number" min="0.00" max="1000.00" step="0.01" ref="menuItemPrice" required />
        </form>
      </div>

    </draggable>
  </div>
</template>

<script>
import draggable from 'vuedraggable'

export default {
  name: 'Dictionary',
  components: {
    draggable
  },
  data () {
    return {

    }
  },
  methods: {
    RemoveFromDictionary: function (index) {
      console.log(index)
      this.$store.dispatch('RemoveFromDictionary', index)
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
  .dictionary {
    grid-column: 3;
    grid-row: 2 / 4;
    outline: dashed;
  }

  .dictionary > h2{
    text-align:center
  }

  div.menu-item {
    margin: 10px;
    padding: 10px;
    background-color: aquamarine;
    border-radius: 10px;
    text-align: center;
  }

</style>
