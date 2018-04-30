<template>
  <v-btn small dark color="red" v-on:click="DeleteItem(deleteType, itemIndex)">
    Delete
  </v-btn>
</template>

<script>
export default {
  name: 'DeleteItem',
  components: {
  },
  data () {
    return {
    }
  },
  props: ['deleteType', 'itemIndex'],
  methods: {
    DeleteItem: function (deleteType, itemIndex) {
      if (deleteType === 'Dictionary') {
        this.$store.dispatch('removeFromDictionary', itemIndex)
      } else if (deleteType === 'BillTable') {
        this.$store.dispatch('updateUserMoneyOwesFromDeleteItem', { itemIndex })
        this.$store.dispatch('removeFromBillTable', itemIndex)
      }
    }
  },
  computed: {
  }
}
</script>

<style scoped>
</style>
