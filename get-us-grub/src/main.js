// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import { store } from './store/store.js'
import Vuetify from 'vuetify'
import axios from 'axios'
import VueAxios from 'vue-axios'

// Ensure you are using css-loader

// window.axios.defaults.headers.common = {
//   'X-Requested-With' : 'XMLHttpRequest'
// };

Vue.use(VueAxios, axios)
Vue.use(Vuetify)

Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store: store,
  components: {App},
  template: '<App/>'
})
