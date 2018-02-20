import Home from "../components/Home.vue"
import Registration from "../components/Registration.vue"
import Login from "../components/Login.vue"
import RestaurantBillSplitter from "../components/RestaurantBillSplitter/RestaurantBillSplitter.vue"

export default [
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
    path: '/Registration',
    name: 'Registration',
    component: Registration,
    beforeEnter: (to, from, next) => {
      document.title = 'Registration'
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
  /* May will be useful for REGISTERED USERS to get their dictionary
  {
    path: '/RestaurantBillSplitter/:id',
    component: RestaurantBillSplitter,
    beforeEnter: (to, from, next) => {
      document.title = 'Split Your Bill!'
      next()
    }
  }
  */
  {
    path: '*'
  }
]
