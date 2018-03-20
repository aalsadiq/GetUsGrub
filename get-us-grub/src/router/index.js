import Vue from 'vue'
import Router from 'vue-router'
import 'vuetify/dist/vuetify.min.css'
import Home from '@/components/Home'
import Registration from '@/components/Registration'
import AdminHome from '@/components/AdminUserManagement/AdminHome.vue'
import CreateUser from '@/components/AdminUserManagement/AdminCreate.vue'
import DeactivateUser from '@/components/AdminUserManagement/DeactivateUser.vue'
import RestaurantBillSplitter from '@/components/RestaurantBillSplitter/RestaurantBillSplitter.vue'
import FoodPreferences from '@/components/FoodPreferences/FoodPreferences.vue'
import EditFoodPreferences from '@/components/FoodPreferences/EditFoodPreferences.vue'

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
      path: '/AdminHome/CreateUser',
      name: 'CreateUser',
      component: CreateUser
    },
    {
      path: '/User/DeactivateUser',
      name: 'DeactivateUser',
      component: DeactivateUser
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
      path: '/FoodPreferences',
      name: 'FoodPreferences',
      component: FoodPreferences
    },
    {
      path: '/EditFoodPreferences',
      name: 'EditFoodPreferences',
      component: EditFoodPreferences
    },    
    {
      path: '*'
    }
  ]
})
