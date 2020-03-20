import Vue from 'vue'
import i18n from './locales/index';
import App from './App.vue'
import router from "./router/";
import store from "./store";
import VueSocketIO from 'vue-socket.io';
import ApiService from "./common/api.service";
import { setupComponents } from "./store/setup.component";
import vuetify from './plugins/vuetify';
import { CHECK_USER } from "./store/actions.type";

Vue.config.productionTip = false

ApiService.init();
setupComponents(Vue);

router.beforeEach((to, from, next) => {  
  store.dispatch(CHECK_USER).then();
  const isAuthenticated = store.getters.isAuthenticated;
  if(isAuthenticated){
    if(to.path === '/' || to.path==='/register'){
      router.push({path:'/home'})
    }
  }
  if(!isAuthenticated){
    if(to.path !=='/' && to.path!=='/register'){
      router.push({path:'/'})
    }
  } 
  next();
})

Vue.use(new VueSocketIO({
  connection: 'http://localhost:8081'
}));


new Vue({
  i18n,
  router,
  store,
  vuetify,
  render: h => h(App)
}).$mount('#app')
