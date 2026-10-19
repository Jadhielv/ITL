import { createRouter, createWebHistory } from 'vue-router'

export default createRouter({
  history: createWebHistory(),
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
