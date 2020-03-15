import ApiService from "@/common/api.service";
import JwtService from "@/common/jwt.service";
import UserService from '@/common/user.service';
import LanguageService from '@/common/language.service'
import {
  CHECK_USER,
  CHECK_GITHUB_USER,
  REGISTER,
  LOGIN,
  LOGOUT
} from "@/store/actions.type";
import { PURGE_USER,SET_USER } from "@/store/mutations.type";

const state = {  
  user : JSON.parse(UserService.getUser()),
  isAuthenticated: !!JwtService.getToken()
};

const getters = {
  currentUser : state => {
    return state.user;
  },
  isAuthenticated: state => {
    return state.isAuthenticated;
  }
};

const actions = {
  [LOGIN](context,payload){
    return new Promise((resolve,reject)=>{
      ApiService.post('/user/login',payload).then((payload)=>{
        context.commit(SET_USER,payload.data);
        resolve(payload)
      }).catch((err)=>{
        reject(err)
      })
    })
  },
  [CHECK_USER](context) {
    if (JwtService.getToken()) {
      ApiService.setHeader();
      return true;
    } else {
      context.commit(PURGE_USER);
    }
  },
  [CHECK_GITHUB_USER](context, payload) {
    return new Promise((resolve, reject) => {
      ApiService.post("/user/checkgithubuser", payload)
        .then(payload => {
          resolve(payload);
        })
        .catch(err => {
          reject(err);
        });
    });
  },
  [REGISTER](context, payload) {
    return new Promise((resolve, reject) => {
      ApiService.post("/user/register", payload)
        .then(payload => {
          resolve(payload);
        })
        .catch(err => {
          reject(err);
        });
    });
  },
  [LOGOUT](context){
    return new Promise((resolve)=>{
      context.commit(PURGE_USER);
      resolve();
    })
  }
};

const mutations = {
  [PURGE_USER](state) {
    state.isAuthenticated = false;
    JwtService.destroyToken();
    UserService.destroyUser();
  },
  [SET_USER](state,payload){
    state.isAuthenticated = true;
    UserService.saveUser(JSON.stringify(payload.user));
    JwtService.saveToken(payload.token);    
    LanguageService.saveLang(payload.user.default_lang);
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
