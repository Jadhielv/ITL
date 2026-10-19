<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light">
    <RouterLink class="navbar-brand" to="/">KCTest</RouterLink>
    <button
      class="navbar-toggler"
      type="button"
      aria-controls="navbarSupportedContent"
      :aria-expanded="isExpanded"
      aria-label="Toggle navigation"
      @click="isExpanded = !isExpanded"
    >
      <span class="navbar-toggler-icon"></span>
    </button>

    <div id="navbarSupportedContent" class="collapse navbar-collapse" :class="{ show: isExpanded }">
      <ul class="navbar-nav me-auto">
        <li class="nav-item">
          <router-link class="nav-link" to="/" @click="isExpanded = false">Permission</router-link>
        </li>
        <li class="nav-item">
          <router-link class="nav-link" to="/types" @click="isExpanded = false">Permission Types</router-link>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {
  name: 'app-navbar',
  data () {
    return {
      isExpanded: false
    }
  }
}
</script>
