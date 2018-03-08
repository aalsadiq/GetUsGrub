<template>
    <div>
      <app-admin-header></app-admin-header>
          <v-app>
            <v-content>
                <v-container>
                    <v-card height="350px">
                      <v-navigation-drawer permanent absolute v-model="drawer">
                        <v-toolbar flat class="transparent">
                          <v-list class="pa-0">
                            <v-list-tile avatar>
                              <v-list-tile-avatar>
                                <img src="https://randomuser.me/api/portraits/men/85.jpg" >
                              </v-list-tile-avatar>
                              <v-list-tile-content>
                                <v-list-tile-title>John Leider</v-list-tile-title>
                              </v-list-tile-content>
                            </v-list-tile>
                          </v-list>
                        </v-toolbar>
                        <v-list class="pt-0" dense>
                          <v-divider></v-divider>
                          <v-list-tile v-for="item in items" :key="item.title" @click="test1">
                            <v-list-tile-action>
                              <v-icon>{{ item.icon }}</v-icon>
                            </v-list-tile-action>
                            <v-list-tile-content>
                              <v-list-tile-title>{{ item.title }}</v-list-tile-title>
                            </v-list-tile-content>
                          </v-list-tile>
                        </v-list>
                      </v-navigation-drawer>
                    </v-card>
                  </v-container>
                </v-content>
            <v-btn color="warning" dark>Normal Button</v-btn>
        </v-app>
        <app-footer></app-footer>
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
  data () {
    return {
      drawer: true,
      items: [
        { title: 'AdminHome', icon: 'dashboard' },
        { title: 'CreateUser', icon: 'question_answer' }
      ]
    }
  }
}
</script>
