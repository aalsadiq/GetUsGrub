import Vue from 'vue'
import Router from 'vue-router'
import 'vuetify/dist/vuetify.min.css'
import Home from '@/components/Home'
import Registration from '@/components/Registration/Registration.vue'
import AdminHome from '@/components/AdminUserManagement/AdminHome.vue'
import CreateUser from '@/components/AdminUserManagement/AdminCreate.vue'
import DeactivateUser from '@/components/AdminUserManagement/AdminDeactivateUser.vue'
import ReactivateUser from '@/components/AdminUserManagement/AdminReactivateUser.vue'
import DeleteUser from '@/components/AdminUserManagement/AdminDeleteUser.vue'
import EditUser from '@/components/AdminUserManagement/AdminEditUser.vue'
import ImageUpload from '@/components/ImageUploadVues/ImageUpload.vue'
import RestaurantBillSplitter from '@/components/RestaurantBillSplitter/RestaurantBillSplitter.vue'
import Login from '@/components/Login/Login.vue'

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
      path: '/Login',
      name: 'Login',
      component: Login,
      beforeEnter: (to, from, next) => {
        document.title = 'Login Brh!'
        next()
      }
    },
    {
      path: '*'
    }
  ]
})
