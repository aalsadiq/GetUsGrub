// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import './bootstrap-3.3.7/dist/css/bootstrap.css'
import draggable from 'vuedraggable'

Vue.component('draggable', draggable) // Allows usage of Vue Draggable across the entire application

Vue.config.productionTip = false


/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: {
    App, draggable
  },
  template: '<App/>'
})
