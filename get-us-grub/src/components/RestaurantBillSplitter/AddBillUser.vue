<template>
  <v-form v-model="valid"
          ref="addBillUserForm"
          lazy-validation
          width="300px">
    <v-text-field label="Add New Bill Friend"
                  v-model="newBillUser"
                  :rules="[rules.required]"
                  required />
    <v-btn small dark color="blue" v-on:click="addBillUser(newBillUser)" v-on:keyup.enter="addBillUser(newBillUser)">Add</v-btn>
  </v-form>
</template>

<script>
export default {
  name: 'AddBillUser',
  components: {
  },
  data () {
    return {
      newBillUser: '',
      counter: 0,
      valid: true,
      rules: {
        required: (value) => (!!value) || 'A valid name is required.'
      }
    }
  },
  methods: {
    addBillUser: function (newBillUser) {
      if (this.$refs.addBillUserForm.validate()) {
        this.$store.dispatch('addBillUser', [newBillUser, this.$store.state.uniqueCounter])
        this.$store.state.uniqueCounter++
      }
    }
  },
  computed: {
  }
}
</script>

<style scoped>

</style>
