<template>
    <div>
        <app-admin-header/>
        <h1> Welcome to home Admin! </h1>
         <img src="@/assets/GetUsGrub.png">
         <!-- <img src="../../../../Images/bdaycake.jpg" /> -->
        <!-- <img src= ""/> -->
        <app-footer/>
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

<style>
/* .application--wrap{
  padding: 0em;
} */
/* .theme--light{
  margin:0em;
} */
</style>
