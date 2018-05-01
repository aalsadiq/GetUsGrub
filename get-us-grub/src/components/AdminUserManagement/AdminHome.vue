<template>
    <div>
        <app-admin-header/>
        <h1> Welcome home, Admin! </h1>
         <img src="@/assets/GetUsGrub.png">
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
  }
}
</script>
