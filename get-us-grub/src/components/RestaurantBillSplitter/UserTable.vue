<template>
  <div class="user-table">
    <h1> Bill Friends</h1>
    <v-divider />
      <div class="user-list" v-for="(billUser, billUserIndex) in billUsers" :key="billUserIndex">
        <ul class="user">
          <li>
            <h2>{{ billUser.name }} owes ${{ (billUser.moneyOwes / 100).toFixed(2) }}</h2>
          </li>
          <li>
            <v-btn dark small color="red" v-on:click="RemoveUser(billUserIndex, billUser.uID)"> Delete</v-btn>
          </li>
        </ul>
        <v-divider />
      </div>
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
    RemoveUser: function (billUserIndex, billUserUID) {
      this.$store.dispatch('updateUserMoneyOwesFromDeleteUser', { billUserIndex, billUserUID })
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
  }

    .user-table > h1 {
      text-align: center;
    }

  #user-list {
    margin-bottom: 150px;
  }

  .user {
    list-style: none;
    margin: 0;
    padding: 0;
  }

  .user > li {
    display: inline;
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
