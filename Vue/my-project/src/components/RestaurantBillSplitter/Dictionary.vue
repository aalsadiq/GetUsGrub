<template>
  <div class="dictionary">
    <h2>Dictionary</h2>
    <draggable class="menu" :list="MenuItems" :options="{group:{ name:'people',  pull:'clone', put:false }}" @start="drag=true" @end="drag=false">
      <div class="menu-item" v-for="(element, index) in MenuItems" :key="element.id">
        {{element.menuItemName}} : ${{element.menuItemPrice.toFixed(2)}}
        <btn type="primary" size="xs">
          <!--v-on:click="EditDictionaryFoodItem"-->
          Edit
        </btn>
        <btn type="danger" size="xs" v-on:click="RemoveFromDictionary(index)">
          Delete
        </btn>
        .
      </div>

    </draggable>
  </div>
</template>

<script lang="ts">
  import draggable from 'vuedraggable'

  export default {
    name: "Dictionary",
    components: {
      draggable
    },
    methods: {
      RemoveFromDictionary: function (index) {
        console.log(index);
        this.$store.dispatch('RemoveFromDictionary', index);
      }
    },
    computed: {
      MenuItems() {
        return this.$store.state.MenuItems;
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
