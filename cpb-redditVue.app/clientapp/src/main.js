import Vue from 'vue'
import { BootstrapVue, BIconArrowUp, BIconArrowDown  } from 'bootstrap-vue'
import moment from 'moment'
import App from './App.vue'

Vue.use(BootstrapVue)
Vue.component('BIconArrowUp', BIconArrowUp)
Vue.component('BIconArrowDown', BIconArrowDown)
Vue.config.productionTip = false

Vue.filter('formatDate', function (value) {
    if (value) {
        return moment(String(value)).format('DD/MM/YYYY')
    }
})

new Vue({
  render: h => h(App),
}).$mount('#app')
