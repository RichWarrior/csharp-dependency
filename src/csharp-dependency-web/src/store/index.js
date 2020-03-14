import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex);

import base from './modules/base.module';
import user from './modules/user.module'

export default new Vuex.Store({
  modules: {
    base,
    user
  }
})
