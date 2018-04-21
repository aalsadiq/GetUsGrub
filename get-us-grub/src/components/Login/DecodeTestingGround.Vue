<template>
  <div>
    <v-form ref="form" v-model="validIdentificationInput" >
      <v-text-field v-model="token"
        prepend-icon="lock"
        label="token"
        name = "username"
        required
      ></v-text-field>
      <v-btn color="primary" @click="DecodeToken" >Submit</v-btn>
    </v-form>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'Decoder',
  components: { },
  methods: {
    DecodeToken: function () {
      axios.post('http://localhost:8081/RenewSession', {}, {
        headers: {
          Authorization: `Bearer ${this.$store.state.authenticationToken}`
        }
      }).then(response => {
        this.$store.state.authenticationToken = response.data
        console.log(this.$store.state.authenticationToken)
      }).catch(error => {
        console.log(error.response)
      })
    }
  }
}
</script>

<style>

</style>
