import ApiService from '@/common/api.service';

import {GET_FOLLOWERS} from '@/store/actions.type';

import {SET_FOLLOWERS} from '@/store/mutations.type';

const state = {
    followers: []
}

const getters= {
    getFollowers: state => {
        return state.followers;
    }
}

const actions = {
    [GET_FOLLOWERS](context){
        return new Promise((resolve,reject)=>{
            ApiService.get('/followers/get').then((payload)=>{
                context.commit(SET_FOLLOWERS,payload.data);
                resolve(payload)
            }).catch((err)=>{
                reject(err)
            })
        })
    }
}

const mutations = { 
    [SET_FOLLOWERS](state,payload){
        state.followers = payload.followers;
    }
}

export default{
    state,
    getters,
    actions,
    mutations
}