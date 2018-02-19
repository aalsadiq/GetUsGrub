import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Login from '@/components/Login'
import RestaurantBillSplitter from '@/components/RestaurantBillSplitter/RestaurantBillSplitter.vue'

Vue.use(Router)

export default new Router({
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
      beforeEnter: (to, from, next) =>{
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
})
