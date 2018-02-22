// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
//import '../static/bootstrap-3.3.7-dist/css/bootstrap.min.css';
import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import App from './App.vue';
import Routes from "./router/routes.js";
import { store } from "./store.js";
//import BootstrapVue from 'bootstrap-vue'
import * as uiv from 'uiv';


/* eslint-disable no-new */
export const EventBus = new Vue();
Vue.use(uiv);
Vue.use(VueRouter);
//Vue.use(BootstrapVue);

const router = new VueRouter({
  routes: Routes
});


const app = new Vue({
  el: '#app',
  router: router,
  store: store,
  components: {
    App
  },
  template: '<App/>'
})
