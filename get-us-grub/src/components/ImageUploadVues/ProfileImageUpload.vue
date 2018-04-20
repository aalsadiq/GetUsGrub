<template>
    <div id = 'profile-image-upload'>
      <h1> Profile Image Upload </h1>
      <app-image-upload :viewType="submitType"/>
      <app-footer/>
  </div>
</template>

<script>
import AppFooter from '@/components/AppFooter'
import AppImageUpload from '@/components/ImageUploadVues/ImageUpload'
export default {
  name: 'ProfileImageUpload',
  components: {
    'app-admin-header': AppAdminHeader,
    'app-footer': AppFooter,
    'app-image-upload': AppImageUpload
  },
  data: () => ({
    submitType: 'profileImageupload'
  })
}
</script>
