<template>
  <div class="user-table">
    <h1>Users</h1>
    <div v-for="(billUser, billUserIndex) in billUsers" :key="billUserIndex">
      {{ billUser.name }} <p> $0.00</p>
      <v-btn v-on:click="RemoveUser(billUser.uID)"><v-icon>clear</v-icon></v-btn>
    </div>
    <v-divider />
    <add-bill-user class="add-bill-user" />
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
    RemoveUser: function (billUserUID) {
      console.log('Deleting ' + billUserUID)
      this.$store.dispatch('removeUser', billUserUID)
    }
  },
  computed: {
    billUsers () {
      return this.$store.state.billUsers
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
    width: auto;
    max-width: 200px;
    left: 50%;
    margin-left: -100px;
  }
</style>
