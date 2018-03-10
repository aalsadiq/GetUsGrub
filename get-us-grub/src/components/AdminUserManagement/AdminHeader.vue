<template>
<div>
  <!-- <v-card height="350px"> -->
    <v-navigation-drawer permanent absolute v-model="drawer"><!-- used to navigate through application to work with or without vue-router outside the box-->
      <!--permanent: remains visible regardless of screensize absolute: Position the element absolutely is false v-model= drawer-->
      <!--v-toolbar:flat: removes the toolbar box-shadow-->
      <v-toolbar flat class="transparent"> <!-- the square to the left -->
      <!--v-list: where our items are held..-->
        <v-list class="pa-0">
          <v-list-tile avatar> <!--avatar: used to set minimum tile height on a single-line list item -->
            <v-list-tile-avatar>
              <img src="https://randomuser.me/api/portraits/men/85.jpg" >
            </v-list-tile-avatar>
            <v-list-tile-content>
              <v-list-tile-title>John Leider</v-list-tile-title><!-- Grab username when logged in?-->
            </v-list-tile-content>
          </v-list-tile>
        </v-list>
      </v-toolbar>
      <v-list class="header-admin" dense>
        <v-list-tile v-for="item in items" :key="item.title" v-on:click="items">
          <router-link :to="item">
            <v-list-tile-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content ref="items">
            <v-list-tile-title>{{ item.title }}</v-list-tile-title>
          </v-list-tile-content>
          </router-link>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  data () {
    return {
      drawer: true,
      items: [
        { title: 'Home', icon: 'home', path: '/AdminHome' },
        { title: 'Create User', icon: 'face', path: '/Registration' },
        { title: 'Edit User', icon: 'edit' },
        { title: 'Deactivate User', icon: 'clear', path: '/DeactivateUser', method: 'testDeactivateUser' },
        { title: 'Reactivate User', icon: 'add' },
        { title: 'Delete User', icon: 'delete_forever' }
      ],
      right: null
    }
  },
  methods: {
    x: function () {
      console.log(this.$refs)
    },
    deactivateUser: function () {
      axios.put(
        '/User/DeactivateUser', {
          username: 'name1'
        }
      ).catch(function (thrown) {
      })
    },
    testDeactivateUser: function () {
      axios.post('/User/DeactivateUser', this.form)
        .then(response => {
          console.log('DONE!!!!')
        })
    }
  }
}
</script>

<style>
.header-admin{
  position: relative;;
}
</style>
