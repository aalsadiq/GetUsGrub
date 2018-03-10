<template>
<div>
  <!-- <v-card height="350px"> -->
    <v-navigation-drawer permanent absolute v-model="drawer">
      <v-toolbar flat class="transparent"> <!-- the square to the left -->
        <v-list class="pa-0">
          <v-list-tile avatar>
            <v-list-tile-avatar>
              <img src="https://randomuser.me/api/portraits/men/85.jpg" >
            </v-list-tile-avatar>
            <v-list-tile-content>
              <v-list-tile-title>John Leider</v-list-tile-title>
            </v-list-tile-content>
          </v-list-tile>
        </v-list>
      </v-toolbar>
      <v-list class="pt-0" dense>
        <v-divider></v-divider>
        <v-list-tile v-for="item in items" :key="item.title" @click="x">
          <v-list-tile-action>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content ref="items">
            <v-list-tile-title>{{ item.title }}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
  <!-- </v-card> -->
  </div>
</template>

<script>
export default {
  data () {
    return {
      drawer: true,
      items: [
        { title: 'Home', icon: 'dashboard' },
        { title: 'About', icon: 'question_answer' }
      ],
      right: null
    }
  },
  methods: {
    x: function () {
      console.log(this.$refs)
    }
  }
}
</script>

<style>
body{
  background-color:black;
}
</style>
