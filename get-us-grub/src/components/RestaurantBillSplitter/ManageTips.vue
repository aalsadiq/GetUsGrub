<template>
  <v-dialog v-model="dialog" scrollable max-width="300px">
    <v-btn small dark slot="activator">Manage Tips</v-btn>
    <v-card>
      <v-card-title><h2>Adjust Tips Here</h2></v-card-title>
      <v-card-text>
        <v-form v-model="valid"
                ref="manageTipsForms"
                lazy-validation>
          <div v-for="(billUser, billUserIndex) in billUsers"
               :key="billUser">
            <h3> {{ billUserIndex }} </h3>
            <h3> {{ billUser.name }} </h3>
            <v-text-field label="Tip"
                          :rules="[rules.required, rules.max, rules.nonnegative]"
                          prefix="$"
                          v-model.number="tips[billUserIndex]"
                          v-money="money"
                          @input="updateTip"
                          required>
            </v-text-field>
          </div>
          <v-btn color="teal"
                 dark
                 v-on:click="updateBillUsersTipAssigned(tips)">
            Save
          </v-btn>
          <v-btn color="teal"
                 dark
                 v-on:click="dialog = false">
            Close
          </v-btn>
        </v-form>

      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<script>
import { VMoney } from 'v-money'

export default {
  name: 'ManageTips',
  components: {
  },
  data () {
    return {
      valid: true,
      maxValue: 1000.00,
      tips: [],
      dialog: false,
      rules: {
        required: (value) => (!!value) || 'Required.',
        // TODO: make max rule more extensible. -Ryan Luong
        max: (value) => value < this.maxValue || ('Price must be less than ' + this.maxValue + '.'),
        nonnegative: (value) => !(value < 0) || ('Price must not be less than 0.')
      },
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
  created () {
//    state.billUsers.forEach(function (element, index)this.tips)
  },
  directives: { money: VMoney },
    methods: {
      updateTip() {
        console.log('before: '+this.tips)
        this.billUsers.forEach(function (element, index) {
          if (this.tips[index] < 0) {
            this.tips[index] = -this.tips[index]
          }
        })
        console.log('after:' + this.tips)
      },
    updateBillUsersTipAssigned: function (itemName, itemPrice) {
      if (this.$refs.manageTipsForms.validate()) {
        tips.forEach(function (element, index) {
          tips[index] = this.convertFromUSDtoInt(tips[index])
        })
        console.log('Ayy ' + tips)
        this.$store.dispatch('updateBillUsersTipAssigned', tips)
      }
    },
    convertFromUSDtoInt: function (usDollars) {
      return this.$store.getters.convertFromUSDtoInt(usDollars)
    }
  },
  computed: {
    billUsers() {
      var users = this.$store.state.billUsers
      console.log('bill users:' + users)
      return this.$store.state.billUsers
    }
  }
}
</script>

<style scoped>

</style>
