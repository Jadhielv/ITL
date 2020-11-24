import Vue from 'vue'
import Router from 'vue-router'
import Permission from '@/components/Permission'
import PermissionType from '@/components/PermissionType'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'permission',
      component: Permission
    },
    {
      path: '/types',
      name: 'permission-type',
      component: PermissionType
    }
  ]
})
