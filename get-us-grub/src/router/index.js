import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import Registration from '@/components/Registration'

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
    }
  ]
})
