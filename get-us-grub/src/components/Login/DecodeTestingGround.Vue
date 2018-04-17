<template>
  <div>
    <app-header/>
    <v-form ref="form" v-model="validIdentificationInput" >
      <v-text-field v-model="token"
        prepend-icon="lock"
        label="token"
        name = "username"
        required
      ></v-text-field>
      <v-btn color="primary" @click="DecodeToken" >Submit</v-btn>
    </v-form>
    <app-footer/>
  </div>
</template>

<script>
import jwt from 'jsonwebtoken'

export default {
  name: 'Decoder',
  components: { },
  methods: {
    DecodeToken: function (token) {
      console.log('hello')
      console.log(jwt.decode('eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJSZWFkVXNlclByb2ZpbGUiOiJUcnVlIiwiUmVhZFByZWZlcmVuY2VzIjoiVHJ1ZSIsIlVzZXJuYW1lIjoiYmF0bWFuIiwibmJmIjoxNTIzNzU2MDAwLCJleHAiOjE1MjM3NTY4OTksImlhdCI6MTUyMzc1NTk5OSwiaXNzIjoiQ1NVTEIuR2V0VXNHcnViIiwiYXVkIjoiaHR0cHM6Ly93d3cuR2V0VXNHcnViLmNvbSJ9.koNZeKZnwWU9aBgQ5krnBD0rB3GvvkFF4d44ZknrUDw')['Username'])
    }
  }
}
</script>

<style>

</style>
