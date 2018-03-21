<template>
  <div class="dictionary">
    <h2>Dictionary</h2>
    <draggable class="menu" v-bind:list="MenuItems" v-bind:options="{group:{ name:'people',  pull:'clone', put:false }}" @start="drag=true" @end="drag=false">
      <div class="menu-item" v-for="(element, index) in MenuItems" :key="element">
        {{element.menuItemName}} : ${{element.menuItemPrice}}<br />
        <v-btn small=true v-on:click="ToggleEdit(index)">
          Edit
        </v-btn>
        <v-btn small=true v-on:click="RemoveFromDictionary(index)">
          Delete
        </v-btn>
        <v-form v-if="element.menuItemEdit">
          <label>Enter Food Item Name</label>
          <v-text-field type="text" ref="menuItemName" required />
          <br />
          <label>Enter Food Item Price</label>
          <v-text-field label="Item Price"
                        prefix="$"
                        v-model.lazy="menuItemPrice"
                        v-money="money"
                        required />
        </v-form>
      </div>
    </draggable>
  </div>
</template>

<script>
import draggable from 'vuedraggable'
import { VMoney } from 'v-money'

export default {
  name: 'Dictionary',
  components: {
    draggable
  },
  directives: { money: VMoney },
  data () {
    return {
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
    ToggleEdit: function (index) {
      this.$store.dispatch('ToggleEdit', index)
    },
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
    outline: solid;
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
