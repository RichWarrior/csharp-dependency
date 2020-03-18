import ApiService from "@/common/api.service";
import JwtService from "@/common/jwt.service";
import UserService from "@/common/user.service";
import LanguageService from "@/common/language.service";
import {
  CHECK_USER,
  CHECK_GITHUB_USER,
  REGISTER,
  LOGIN,
  LOGOUT,
  GET_GITHUB_USER
} from "@/store/actions.type";
import { PURGE_USER, SET_USER, SET_GITHUB_USER } from "@/store/mutations.type";

const state = {
  user: JSON.parse(UserService.getUser()),
  isAuthenticated: !!JwtService.getToken(),
  githubUser: {},
  githubFollowers: [],
  githubFollowings: [],
  githubStarredRepository: [],
  githubRepository: []
};

const getters = {
  currentUser: state => {
    return state.user;
  },
  isAuthenticated: state => {
    return state.isAuthenticated;
  },
  getGithubUser: state => {
    return state.githubUser;
  },
  getGithubFollowers: state => {
    return state.githubFollowers;
  },
  getGithubFollowings: state => {
    return state.githubFollowings;
  },
  getGithubStarredRepository: state => {
    return state.githubStarredRepository;
  },
  getGithubRepository: state => {
    return state.githubRepository;
  }
};

const actions = {
  [LOGIN](context, payload) {
    return new Promise((resolve, reject) => {
      ApiService.post("/user/login", payload)
        .then(payload => {
          context.commit(SET_USER, payload.data);
          resolve(payload);
        })
        .catch(err => {
          reject(err);
        });
    });
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
  [LOGOUT](context) {
    return new Promise(resolve => {
      context.commit(PURGE_USER);
      resolve();
    });
  },
  [GET_GITHUB_USER](context) {
    return new Promise((resolve, reject) => {
      ApiService.get("/user/getuser")
        .then(payload => {
          context.commit(SET_GITHUB_USER, payload.data);
          resolve(payload);
        })
        .catch(err => {
          reject(err);
        });
    });
  }
};

const mutations = {
  [PURGE_USER](state) {
    state.isAuthenticated = false;
    JwtService.destroyToken();
    UserService.destroyUser();
  },
  [SET_USER](state, payload) {
    state.isAuthenticated = true;
    UserService.saveUser(JSON.stringify(payload.user));
    JwtService.saveToken(payload.token);
    LanguageService.saveLang(payload.user.default_lang);
  },
  [SET_GITHUB_USER](state,payload){
    state.githubUser = payload.user;
    state.githubFollowers = payload.followers;
    state.githubFollowings = payload.followings;
    state.githubStarredRepository = payload.starredRepositories;
    state.githubRepository = payload.repositories;
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
