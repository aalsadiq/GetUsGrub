import Vue from 'vue'
import Router from 'vue-router'
import 'vuetify/dist/vuetify.min.css'
import ResourceNotFound from '@/components/Errors/ResourceNotFound.vue'
import GeneralError from '@/components/Errors/GeneralError.vue'
import Home from '@/components/Home.vue'
import RestaurantSelectionRegisteredUser from '@/components/RestaurantSelection/RegisteredUser/Main.vue'
import Registration from '@/components/Registration/Registration.vue'
import AdminHome from '@/components/AdminUserManagement/AdminHome.vue'
import CreateUser from '@/components/AdminUserManagement/AdminCreate.vue'
import DeactivateUser from '@/components/AdminUserManagement/AdminDeactivateUser.vue'
import ReactivateUser from '@/components/AdminUserManagement/AdminReactivateUser.vue'
import DeleteUser from '@/components/AdminUserManagement/AdminDeleteUser.vue'
import EditUser from '@/components/AdminUserManagement/AdminEditUser.vue'
import ImageUpload from '@/components/ImageUploadVues/ImageUpload.vue'
import RestaurantBillSplitter from '@/components/RestaurantBillSplitter/RestaurantBillSplitter.vue'
// import FoodPreferences from '@/components/FoodPreferences/FoodPreferences.vue'
// import EditFoodPreferences from '@/components/FoodPreferences/EditFoodPreferences.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/ResourceNotFound',
      name: 'ResourceNotFound',
      component: ResourceNotFound
    },
    {
      path: '/GeneralError',
      name: 'GeneralError',
      component: GeneralError
    },
    {
      path: '/',
      name: 'Home',
      component: Home,
      beforeEnter: (to, from, next) => {
        document.title = 'Search Restaurant'
        next()
      }
    },
    {
      path: '/RestaurantSelection/Registered',
      name: 'RestaurantSelectionRegisteredUser',
      component: RestaurantSelectionRegisteredUser,
      beforeEnter: (to, from, next) => {
        document.title = 'Search Restaurant'
        next()
      }
    },
    {
      path: '/Registration',
      name: 'Registration',
      component: Registration,
      beforeEnter: (to, from, next) => {
        document.title = 'Register'
        next()
      }
    },
    {
      path: '/User/Admin',
      name: 'User',
      component: AdminHome
    },
    {
      path: '/User/CreateUser',
      name: 'CreateUser',
      component: CreateUser
    },
    {
      path: '/User/DeactivateUser',
      name: 'DeactivateUser',
      component: DeactivateUser
    },
    {
      path: '/User/ReactivateUser',
      name: 'ReactivateUser',
      component: ReactivateUser
    },
    {
      path: '/User/DeleteUser',
      name: 'DeleteUser',
      component: DeleteUser
    },
    {
      path: '/User/EditUser',
      name: 'EditUser',
      component: EditUser
    },
    {
      path: '/User/Profile/ImageUpload',
      name: 'ImageUpload',
      component: ImageUpload
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
    // {
    //   path: '/FoodPreferences',
    //   name: 'FoodPreferences',
    //   component: FoodPreferences
    // },
    // {
    //   path: '/EditFoodPreferences',
    //   name: 'EditFoodPreferences',
    //   component: EditFoodPreferences
    // },
    {
      path: '*'
    }
  ]
})
