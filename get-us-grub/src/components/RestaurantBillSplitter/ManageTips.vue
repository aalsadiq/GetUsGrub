<template>
  <v-dialog v-model="dialog" scrollable max-width="300px">
    <v-btn small dark slot="activator">Manage Tips</v-btn>
    <v-card>
      <v-card-title><h2>Adjust Tips Here</h2></v-card-title>
      <v-card-text>
        <v-form v-model="valid"
                ref="manageTipsForms"
                lazy-validation>
          <div v-for="(user, index) in billUsers"
               :key="index">
            <h3> {{ index }}</h3>
            <h3> {{ user.name }} </h3>
            <v-text-field label="Tip"
                          :rules="[rules.required, rules.max, rules.nonnegative]"
                          prefix="$"
                          v-model.number="tips[index]"
                          v-money="money"
                          @change="updateTip"
                          required />
          </div>
          <v-btn color="teal"
                 dark
                 v-on:click="updateBillUsersTipAssigned">
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
  },
  directives: { money: VMoney },
  methods: {
    updateTip () {
      var self = this
      console.log('before: ' + self.tips)
      self.billUsers.forEach(function (element, index) {
        if (self.tips[index] < 0) {
          self.tips[index] = -self.tips[index]
        }
      })
      console.log('after:' + self.tips)
    },
    updateBillUsersTipAssigned: function () {
      console.log('Tips: ' + this.tips)
      if (this.$refs.manageTipsForms.validate()) {
        this.tips.forEach(function (element, index) {
          this.tips[index] = this.convertFromUSDtoInt(this.tips[index])
        })
        console.log('Ayy ' + this.tips)
        this.$store.dispatch('updateBillUsersTipAssigned', this.tips)
      }
    },
    convertFromUSDtoInt: function (usDollars) {
      return this.$store.getters.convertFromUSDtoInt(usDollars)
    }
  },
  computed: {
    billUsers () {
      var users = this.$store.state.billUsers
      console.log('bill users count:' + users.length)
      return this.$store.state.billUsers
    }
  }
}
</script>

<style scoped>

</style>
