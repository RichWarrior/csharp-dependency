import languageService from '@/common/language.service'
import {
    IS_LOADING,
    CHANGE_BLANK_STATUS
} from "../actions.type";

import {
    SET_LOADING,
    SET_BLANK_STATUS
} from "../mutations.type";

const state ={
    isLoading : false,
    defaultLocale : languageService.getLang(),
    isBlankTemplate : false
}

const getters = {
    getLoadingState : state =>{
        return state.isLoading;
    },
    getDefaultLocale:state =>{
        return state.defaultLocale === null ? 'tr' : state.defaultLocale;
    },
    isBlankTemplate : state => {
        return state.isBlankTemplate;
    }
}

const actions ={
    [IS_LOADING](context) {
        context.commit(SET_LOADING)
    },
    [CHANGE_BLANK_STATUS](context,data){
        context.commit(SET_BLANK_STATUS,data);
    }, 
}

const mutations ={
    [SET_LOADING](state) {
        state.isLoading = !state.isLoading;
    },
    [SET_BLANK_STATUS](state,payload){
        state.isBlankTemplate = payload;
    }
}

export default{
    state,
    getters,
    actions,
    mutations
}