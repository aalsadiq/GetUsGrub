// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import { store } from './store/store.js'
import Vuetify from 'vuetify'
import VueCharts from 'vue-chartjs'
import Notifications from 'vue-notification'
import 'es6-promise/auto'

Vue.use(Vuetify)
Vue.use(Notifications)
Vue.use(VueCharts)
Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store: store,
  components: {App},
  template: '<App/>'
})
