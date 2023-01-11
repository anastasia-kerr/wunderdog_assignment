import Vue from 'vue'
import App from './App.vue'
import vuetify from './plugins/vuetify'
import { setApiDefaults } from './api'

Vue.config.productionTip = false
setApiDefaults(process.env.VUE_APP_API)

new Vue({
  vuetify,
  render: h => h(App)
}).$mount('#app')
