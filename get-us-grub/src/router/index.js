import Vue from 'vue'
import Router from 'vue-router'
import 'vuetify/dist/vuetify.min.css'
import Home from '@/components/Home'
import AdminHome from '@/components/AdminUserManagement/AdminHome.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/Home',
      name: 'Home',
      component: Home
    },
    {
      path: '/AdminHome',
      name: 'AdminHome',
      component: AdminHome
    }
  ]
})
