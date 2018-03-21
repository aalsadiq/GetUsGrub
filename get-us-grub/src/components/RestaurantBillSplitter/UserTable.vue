<template>
  <div class="user-table">
    <h1>Users</h1>
    <div v-for="(billUser, billUserIndex) in BillUsers" :key="billUserIndex">
      {{ billUser.name }}
      <v-btn v-on:click="RemoveUser(billUserIndex)"><v-icon>clear</v-icon></v-btn>
    </div>
    <v-divider/>
    <add-bill-user class="add-bill-user"/>
  </div>
</template>
<script>
import AddBillUser from './AddBillUser.vue'
import ManageUsers from './ManageUsers.vue'
import draggable from 'vuedraggable'
import { VMoney } from 'v-money'

export default {
  name: 'UserTable',
  components: {
    'add-bill-user': AddBillUser,
    'manage-users': ManageUsers,
    draggable
  },
  directives: { money: VMoney },
  data () {
    return {
      editType: 'BillTable',
      deleteType: 'BillTable',
      money: {
        decimal: '.',
        thousands: '',
        prefix: '$',
        suffix: '',
        precision: 2,
        masked: false
      }
    }
  },
  methods: {
    RemoveUser: function (billUserIndex) {
      console.log('Deleting' + billUserIndex)
      this.$store.dispatch('RemoveUser', billUserIndex)
    }
  },
  computed: {
    BillItems () {
      return this.$store.state.BillItems
    },
    BillUsers () {
      return this.$store.state.BillUsers
    },
    TotalPrice () {
      return this.$store.getters.totalPrice
    }
  }
}
</script>

<style>
  .user-table {
    position: relative;
    grid-column: 1 / 2;
    grid-row: 1 / 4;
    outline: solid;
  }
    .user-table > h1 {
      text-align: center;
    }

  .add-bill-user {
    position: absolute;
    bottom: 0;
  }
</style>
