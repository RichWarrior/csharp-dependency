import ApiService from '@/common/api.service';

import {GET_FOLLOWING} from '@/store/actions.type';

import {SET_FOLLOWING} from '@/store/mutations.type';

const state = {
    following :[]
}

const getters= {
    getFollowing :state =>{
        return state.following;
    }
}

const actions = {
    [GET_FOLLOWING](context){
        return new Promise((resolve,reject)=>{            
            ApiService.get('/following/get').then((payload)=>{
                context.commit(SET_FOLLOWING,payload.data);
                resolve(payload)
            }).catch((err)=>{
                reject(err)
            })
        })
    }
}

const mutations = {
    [SET_FOLLOWING](state,payload){
        state.following = payload.followings;
    }
}

export default{
    state,
    getters,
    actions,
    mutations
}