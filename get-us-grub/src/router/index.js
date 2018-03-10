import Vue from 'vue'
import Router from 'vue-router'
import 'vuetify/dist/vuetify.min.css'
import Home from '@/components/Home'
import Registration from '@/components/Registration'
import AdminHome from '@/components/AdminUserManagement/AdminHome.vue'
import RestaurantBillSplitter from '@/components/RestaurantBillSplitter/RestaurantBillSplitter.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/Registration',
      name: 'Registration',
      component: Registration
    },
    {
      path: '/AdminHome',
      name: 'AdminHome',
      component: AdminHome
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
})
