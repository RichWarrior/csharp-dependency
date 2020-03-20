import ApiService from "@/common/api.service";
import { GET_REPOSITORY } from "@/store/actions.type";
import { SET_REPOSITORY } from "@/store/mutations.type";

const state = {
  repositories: []
};

const getters = {
  getRepositories: state => {
    return state.repositories;
  }
};

const actions = {
  [GET_REPOSITORY](context) {
    return new Promise((resolve, reject) => {
      ApiService.get("/repository/get")
        .then(payload => {
          context.commit(SET_REPOSITORY, payload.data);
          resolve(payload);
        })
        .catch(err => {
          reject(err);
        });
    });
  }
};

const mutations = {
    [SET_REPOSITORY](state,payload){
        state.repositories = payload.repositories
    }
};

export default {
  state,
  getters,
  actions,
  mutations
};
