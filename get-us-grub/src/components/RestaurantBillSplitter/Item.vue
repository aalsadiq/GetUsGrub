<template>

</template>

<script>
export default {
  name: 'Item',
  components: {
  },
  data () {
    return {
    }
  },
  methods: {
    log: function () {
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
