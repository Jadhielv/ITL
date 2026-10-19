import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'permission',
      component: () => import(/* webpackChunkName: "permission" */ '@/components/Permission')
    },
    {
      path: '/types',
      name: 'permission-type',
      component: () => import(/* webpackChunkName: "permission-type" */ '@/components/PermissionType')
    }
  ]
})
