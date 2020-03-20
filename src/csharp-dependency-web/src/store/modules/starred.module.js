import ApiService from "@/common/api.service";

import { GET_STARRED } from "@/store/actions.type";

import { SET_STARRED } from "@/store/mutations.type";

const state = {
  starredRepositories: []
};

const getters = {
  getStarredRepositories: state => {
    return state.starredRepositories;
  }
};

const actions = {
  [GET_STARRED](context) {
    return new Promise((resolve, reject) => {
      ApiService.get("/starredrepository/get")
        .then(payload => {
          context.commit(SET_STARRED, payload.data);
          resolve(payload);
        })
        .catch(err => {
          reject(err);
        });
    });
  }
};

const mutations = {
    [SET_STARRED](state,payload){
        state.starredRepositories = payload.starredRepositories;
    }
};

export default {
  state,
  getters,
  actions,
  mutations
};
