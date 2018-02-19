// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import App from './App.vue';
import Home from "./components/Home.vue"
import Login from "./components/Login.vue"
import RestaurantBillSplitter from "./components/RestaurantBillSplitter/RestaurantBillSplitter.vue"
// import './bootstrap-3.3.7/dist/css/bootstrap.min.css';
// import draggable from 'vuedraggable'

//Vue.component('draggable', draggable) // Allows usage of Vue Draggable across the entire application

/* eslint-disable no-new */
export const EventBus = new Vue();

const router = new VueRouter({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home,
      beforeEnter: (to, from, next) => {
        document.title = 'Home'
        next()
      }
    },
    {
      path: '/Login',
      name: 'Login',
      component: Login,
      beforeEnter: (to, from, next) => {
        document.title = 'Login'
        next()
      }/*,
      beforeRouteLeave: (to, from, next) => {
        if (to.path === 'SignIn') {
          next(false)
        }
      }*/
    },
    {
      path: '/RestaurantBillSplitter',
      name: 'RestaurantBillSplitter',
      component: RestaurantBillSplitter,
      beforeEnter: (to, from, next) => {
        document.title = 'Split Your Bill!'
        next()
      }
    },
    {
      path: '*'
    }
    ]
});

Vue.use(VueRouter);
const app = new Vue({
  el: '#app',
  router,
  components: {
    App//, draggable
  },
  template: '<router-view></router-view>'
})
