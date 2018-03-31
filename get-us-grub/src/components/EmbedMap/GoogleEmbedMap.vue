<template>
<div id="vue-frame">
   <vue-frame text="VueJS" url="https://vuejs.org" frame="myframe" type="button" class="form-control"></vue-frame>
   <vue-frame text="Api Vue" url="https://vuejs.org/v2/api" frame="myframe" type="a"></vue-frame>
   <br />
 
   <iframe id="myframe" width="800"></iframe>
</div>
 </template>
<script src="/dist/vue-frame.js"></script>
<script>
import AppHeader from '@/components/AppHeader'
import AppFooter from '@/components/AppFooter'
export default {
  name: 'GoogleEmbedMap',
  components: {
    'app-header': AppHeader,
    'app-footer': AppFooter,
    'app-create-user': AppCreateUser
  }
}
</script>

AIzaSyCfKElVtKARYlgvCdQXBImfjRH5rmUF0mg