import Vue from 'vue'
import Router from 'vue-router'
import 'vuetify/dist/vuetify.min.css'
import ResourceNotFound from '@/components/Errors/ResourceNotFound.vue'
import GeneralError from '@/components/Errors/GeneralError.vue'
import Unauthorized from '@/components/Errors/Unauthorized.vue'
import Forbidden from '@/components/Errors/Forbidden.vue'
import InternalServerError from '@/components/Errors/InternalServerError.vue'
import Home from '@/components/Home.vue'
import Registration from '@/components/Registration/Registration.vue'
import AdminHome from '@/components/AdminUserManagement/AdminHome.vue'
import CreateUser from '@/components/AdminUserManagement/AdminCreate.vue'
import DeactivateUser from '@/components/AdminUserManagement/AdminDeactivateUser.vue'
import ReactivateUser from '@/components/AdminUserManagement/AdminReactivateUser.vue'
import DeleteUser from '@/components/AdminUserManagement/AdminDeleteUser.vue'
import EditUser from '@/components/AdminUserManagement/AdminEditUser.vue'
import ImageUpload from '@/components/ImageUploadVues/ImageUpload.vue'
import RestaurantBillSplitter from '@/components/RestaurantBillSplitter/RestaurantBillSplitter.vue'
import FoodPreferences from '@/components/FoodPreferences/FoodPreferences.vue'
import EditFoodPreferences from '@/components/FoodPreferences/EditFoodPreferences.vue'
import Login from '@/components/Login/Login.vue'
import Testing from '@/components/Login/DecodeTestingGround.vue'
import Profile from '@/components/Profile/Profile.vue'
import FirstTimeRegistration from '@/components/Sso/FirstTimeRegistration.vue'
// import FoodPreferences from '@/components/FoodPreferences/FoodPreferences.vue'
// import EditFoodPreferences from '@/components/FoodPreferences/EditFoodPreferences.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/ResourceNotFound',
      name: 'ResourceNotFound',
      component: ResourceNotFound,
      beforeEnter: (to, from, next) => {
        document.title = 'Resource Not Found'
        next()
      }
    },
    {
      path: '/GeneralError',
      name: 'GeneralError',
      component: GeneralError,
      beforeEnter: (to, from, next) => {
        document.title = 'General Error'
        next()
      }
    },
    {
      path: '/Unauthorized',
      name: 'Unauthorized',
      component: Unauthorized,
      beforeEnter: (to, from, next) => {
        document.title = 'Unauthorized'
        next()
      }
    },
    {
      path: '/Forbidden',
      name: 'Forbidden',
      component: Forbidden,
      beforeEnter: (to, from, next) => {
        document.title = 'Forbidden'
        next()
      }
    },
    {
      path: '/InternalServerError',
      name: 'InternalServerError',
      component: InternalServerError,
      beforeEnter: (to, from, next) => {
        document.title = 'Internal Server Error'
        next()
      }
    },
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
    {
      path: '/Profile',
      name: 'Profile',
      component: Profile,
      beforeEnter: (to, from, next) => {
        document.title = 'Your profile'
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
      path: '/FoodPreferences',
      name: 'FoodPreferences',
      component: FoodPreferences
    },
    {
      path: '/FoodPreferences/Edit',
      name: 'EditFoodPreferences',
      component: EditFoodPreferences
    },
    {
      path: '/Login',
      name: 'Login',
      component: Login,
      beforeEnter: (to, from, next) => {
        document.title = 'Login'
        next()
      }
    },
    {
      path: '/Testing',
      name: 'Testing',
      component: Testing,
      beforeEnter: (to, from, next) => {
        document.title = 'Testing!'
        next()
      }
    },
    {
      path: '/FirstTimeRegistration',
      name: 'FirstTimeRegistration',
      component: FirstTimeRegistration,
      beforeEnter: (to, from, next) => {
        document.title = 'First Time Registration'
        next()
      }
    },
    {
      path: '*'
    }
  ]
})
