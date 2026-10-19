import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App'
import router from './router'

import './assets/css/bootstrap.scss'
import 'bootstrap-vue-next/dist/bootstrap-vue-next.css'
import './assets/css/app.css'

createApp(App)
  .use(createPinia())
  .use(router)
  .mount('#app')
