import ApiService from "@/common/api.service";
import JwtService from "@/common/jwt.service";
import { CHECK_USER, CHECK_GITHUB_USER } from "@/store/actions.type";
import { PURGE_USER } from "@/store/mutations.type";

const state = {
  isAuthenticated: !!JwtService.getToken()
};

const getters = {
  isAuthenticated: state => {
    return state.isAuthenticated;
  }
};

const actions = {
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
  }
};

const mutations = {
  [PURGE_USER](state) {
    state.isAuthenticated = false;
    JwtService.destroyToken();
  }
};

export default {
  state,
  getters,
  actions,
  mutations
};
