import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex);

import base from './modules/base.module';
import user from './modules/user.module';
import followers from './modules/followers.module';
import following from './modules/following.module';
import starred from './modules/starred.module';
import repository from './modules/repository.module';

export default new Vuex.Store({
  modules: {
    base,
    user,
    followers,
    following,
    starred,
    repository
  }
})
