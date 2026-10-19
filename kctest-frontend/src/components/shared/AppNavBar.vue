<template>
  <header class="app-header">
    <nav class="navbar navbar-expand-md app-navbar">
      <div class="container-xxl app-navbar-inner">
        <RouterLink class="app-brand" to="/" aria-label="ITL home">
          <span class="brand-mark" aria-hidden="true">I</span>
          <span class="brand-copy">
            <strong>ITL</strong>
            <small>Permission management</small>
          </span>
        </RouterLink>
        <button
          class="navbar-toggler"
          type="button"
          aria-controls="app-navigation"
          :aria-expanded="isExpanded"
          aria-label="Toggle navigation"
          @click="isExpanded = !isExpanded"
        >
          <span class="navbar-toggler-icon"></span>
        </button>

        <div id="app-navigation" class="collapse navbar-collapse" :class="{ show: isExpanded }">
          <ul class="navbar-nav app-nav-links">
            <li class="nav-item">
              <RouterLink class="nav-link" to="/" @click="isExpanded = false">
                Permissions
              </RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink class="nav-link" to="/types" @click="isExpanded = false">
                Permission types
              </RouterLink>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  </header>
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
