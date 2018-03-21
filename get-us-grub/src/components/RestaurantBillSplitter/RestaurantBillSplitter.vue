<template>
  <div id="restaurant-bill-splitter">
    <app-header />
    <div class="wrapper">
      <restaurantBillSplitter-userTable />
      <restaurantBillSplitter-billTable />
      <restaurantBillSplitter-dictionaryInput />
      <restaurantBillSplitter-dictionary />
    </div>
    <div>
      <h1> Debug </h1>
      <h2> Menu Items</h2>
      <ul>
        <li v-for="element in MenuItems" :key="element">
          {{element}}
        </li>
      </ul>
      <h2> Bill Items</h2>
      <ul>
        <li v-for="element in BillItems" :key="element">
          {{element}}
        </li>
      </ul>
    </div>
    <app-footer />
  </div>
</template>

<script>
import AppHeader from '../AppHeader.vue'
import AppFooter from '../AppFooter.vue'
import UserTable from './UserTable.vue'
import BillTable from './BillTable.vue'
import Dictionary from './Dictionary.vue'
import DictionaryInput from './DictionaryInput.vue'

export default {
  name: 'RestaurantBillSplitter',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter,
    'restaurantBillSplitter-userTable': UserTable,
    'restaurantBillSplitter-billTable': BillTable,
    'restaurantBillSplitter-dictionaryInput': DictionaryInput,
    'restaurantBillSplitter-dictionary': Dictionary
  },
  data () {
    return {
    }
  },
  methods: {

  },
  computed: {
    MenuItems () {
      return this.$store.state.MenuItems
    },
    BillItems () {
      return this.$store.state.BillItems
    },
    totalPrice () {
      return this.$store.getters.totalPrice
    }
  }
}
</script>
<style scoped>
  #restaurant-bill-splitter{
    margin: 0 0 100px 0;
  }

  .wrapper {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 10px;
    grid-auto-rows: minmax(100px, auto);
  }

  h2.total {
    padding: 10px 1.2em 0px 0px;
    text-align: right;
  }
</style>
