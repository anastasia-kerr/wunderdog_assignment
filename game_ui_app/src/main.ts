import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import vuetify from './plugins/vuetify'
import { setApiDefaults } from './api'

Vue.config.productionTip = false
setApiDefaults(process.env.VUE_APP_API)

new Vue({
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
