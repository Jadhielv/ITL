<template>
  <div id="app">
    <AppNavBar />
    <router-view />
  </div>
</template>

<script>
import AppNavBar from './components/shared/AppNavBar'

export default {
  name: 'App',
  components: {
    AppNavBar
  }
}
</script>
