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
      console.log(deleteType)
      if (deleteType === 'Dictionary') {
        this.$store.dispatch('RemoveFromDictionary', itemIndex)
      } else if (deleteType === 'BillTable') {
        this.$store.dispatch('RemoveFromBillTable', itemIndex)
      }
    },
    Log: function () {
      console.log(this.$refs.editForm)
    }
  },
  computed: {
  }
}
</script>

<style scoped>
</style>
