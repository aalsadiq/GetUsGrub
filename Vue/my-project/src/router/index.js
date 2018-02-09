import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Login from '@/components/Login'
import RestaurantBillSplitter from '@/components/RestaurantBillSplitter'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      component: Home
    },
    {
      path: '/Login',
      name: 'Login',
      component: Login /*,
      beforeRouteLeave: (to, from, next) => {
        if (to.path === 'SignIn') {
          next(false)
        }
      }*/
    },
    {
      path: '/RestaurantBillSplitter',
      name: 'RestaurantBillSplitter',
      component: RestaurantBillSplitter
    },
    {
      path: '*'
    }
  ]
})
