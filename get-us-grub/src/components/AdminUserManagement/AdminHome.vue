<template>
    <div>
      <div id ="vue-app-admin-header" >
        <app-admin-header/>
      </div>
      <div id ='adminhomevue'>
        <h1> Welcome to home Admin! </h1>
         <!-- <img src="@/assets/GetUsGrub.png"> -->
         <img src="../../../../Images/bdaycake.jpg" />
        <!-- <img src= ""/> -->
      </div>
      <div id ="vue-app-footer">
        <app-footer/>
      </div>
    </div>
</template>

<script>
import AppAdminHeader from '@/components/AdminUserManagement/AdminHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'AdminHome',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter
  },
  mounted () {
  }
}
</script>
