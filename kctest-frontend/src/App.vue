<template>
  <BApp>
    <AppNavBar />
    <RouterView />
  </BApp>
</template>

<script>
import { BApp } from 'bootstrap-vue-next'
import AppNavBar from './components/shared/AppNavBar'

export default {
  name: 'App',
  components: {
    AppNavBar,
    BApp
  }
}
</script>
