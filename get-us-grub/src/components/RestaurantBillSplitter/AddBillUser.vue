<template>
  <v-form v-model="valid"
          ref="addBillUserForm"
          lazy-validation>
    <v-text-field label="Add New User"
                  v-model="newBillUser"
                  :rules="[rules.required]"
                  required />
    <v-btn small dark color="blue" v-on:click="AddBillUser(newBillUser)" v-on:keyup.enter="AddBillUser(newBillUser)">Add</v-btn>
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
        required: (value) => (!!value) || 'Required.'
      }
    }
  },
  methods: {
    AddBillUser: function (newBillUser) {
      if (this.$refs.addBillUserForm.validate()) {
        this.$store.dispatch('AddBillUser', [newBillUser, this.$store.state.uniqueUserCounter])
        this.$store.state.uniqueUserCounter++
      }
    },
    RemoveFromBill: function (index) {
      // Parameters have to be placed into an array because dispatch can only take two. The name and the payload.
      console.log(index)
      this.$store.dispatch('RemoveFromBill', index)
    }
  },
  computed: {
    BillItems () {
      return this.$store.state.BillItems
    },
    BillUsers () {
      return this.$store.state.BillUsers
    }
  }
}
</script>

<style>

</style>
