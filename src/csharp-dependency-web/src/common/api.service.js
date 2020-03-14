import Vue from "vue";
import axios from "axios";
import VueAxios from "vue-axios";
import JwtService from "@/common/jwt.service";
import { API_URL } from "@/common/config";
import router from '@/router';
import store from '@/store';
import { IS_LOADING, LOGOUT } from "@/store/actions.type";
import objectToFormData from 'object-to-formdata';

const ApiService = {
    init() {
        Vue.use(VueAxios, axios);
        Vue.axios.defaults.baseURL = API_URL;
    },

    setHeader() {
        Vue.axios.defaults.headers.common[
            "Authorization"
        ] = `Bearer ${JwtService.getToken()}`;
    },

    purgeHeader() {
        delete axios.defaults.headers.common["Authorization"];
    },

    post(resource, params) {
        delete Vue.axios.defaults.headers.post['Content-Type']
        return new Promise((resolve, reject) => {
            store.dispatch(IS_LOADING);

            Vue.axios.post(`${resource}`, params).then((response) => {
                resolve(response.data);
            }).catch((err) => {
                if (!err.response) {
                    const data = {
                        message: 'Api Bağlantı Hatası'
                    };
                    reject(data);
                } else {
                    const statusCode = err.response.status;
                    if (statusCode === 401) {
                        if (err.response.data.statusCode === undefined) {
                            store.dispatch(LOGOUT)
                            router.push({ path: '/' })
                            err.response.data.message = 'Oturumunuzun Süresi Bitmiş. Lütfen Tekrar Giriş Yapınız.';
                        }
                        reject(err.response.data)
                    } else if (statusCode === 404) {
                        reject(err.response.data);
                    } else if (statusCode === 500) {
                        err.response.data.message = 'Sistemsel Bir Hata Oluştu'
                        reject(err.response.data);
                    }
                }
            }).finally(() => {
                store.dispatch(IS_LOADING);
            })
        })
    },

    get(resource) {
        delete Vue.axios.defaults.headers.post['Content-Type']
        return new Promise((resolve, reject) => {
            store.dispatch(IS_LOADING);

            Vue.axios.get(`${resource}`).then((response) => {
                resolve(response.data);
            }).catch((err) => {
                if (!err.response) {
                    const data = {
                        message: 'Api Bağlantı Hatası'
                    };
                    reject(data);
                } else {
                    const statusCode = err.response.status;
                    if (statusCode === 401) {
                        if (err.response.data.statusCode === undefined) {
                            store.dispatch(LOGOUT)
                            router.push({ path: '/' })
                            err.response.data.message = 'Oturumunuzun Süresi Bitmiş. Lütfen Tekrar Giriş Yapınız.';
                        }
                        reject(err.response.data)
                    } else if (statusCode === 404) {
                        reject(err.response.data);
                    } else if (statusCode === 500) {
                        err.response.data.message = 'Sistemsel Bir Hata Oluştu'
                        reject(err.response.data);
                    }
                }
            }).finally(() => {
                store.dispatch(IS_LOADING);
            })
        })
    },

    put(resource, params) {
        return new Promise((resolve, reject) => {
            store.dispatch(IS_LOADING);
            Vue.axios.defaults.headers.post['Content-Type'] = 'multipart/form-data';
            let formData = this.getFormData(params);
            Vue.axios.put(`${resource}`, formData).then((response) => {
                resolve(response.data);
            }).catch((err) => {
                if (!err.response) {
                    const data = {
                        message: 'Api Bağlantı Hatası'
                    };
                    reject(data);
                } else {
                    const statusCode = err.response.status;
                    if (statusCode === 401) {
                        store.dispatch(LOGOUT)
                        router.push({ path: '/' })
                        reject(err.response.data)
                    } else if (statusCode === 404) {
                        reject(err.response.data);
                    }
                }
            }).finally(() => {
                store.dispatch(IS_LOADING);
            })
        })
    },

    delete(resource, params) {
        delete Vue.axios.defaults.headers.post['Content-Type']
        Vue.axios.defaults.headers.post['Content-Type'] = 'application/json';
        return new Promise((resolve, reject) => {
            store.dispatch(IS_LOADING);

            Vue.axios.delete(`${resource}`, { data: params }).then((response) => {
                resolve(response.data);
            }).catch((err) => {
                if (!err.response) {
                    const data = {
                        message: 'Api Bağlantı Hatası'
                    };
                    reject(data);
                } else {
                    const statusCode = err.response.status;
                    if (statusCode === 401) {
                        if (err.response.data.statusCode === undefined) {
                            store.dispatch(LOGOUT)
                            router.push({ path: '/' })
                            err.response.data.message = 'Oturumunuzun Süresi Bitmiş. Lütfen Tekrar Giriş Yapınız.';
                        }
                        reject(err.response.data)
                    } else if (statusCode === 404) {
                        reject(err.response.data);
                    } else if (statusCode === 500) {
                        err.response.data.message = 'Sistemsel Bir Hata Oluştu'
                        reject(err.response.data);
                    }
                }
            }).finally(() => {
                store.dispatch(IS_LOADING);
            })
        })
    },

    postFile(resource, params) {
        return new Promise((resolve, reject) => {
            store.dispatch(IS_LOADING);
            Vue.axios.defaults.headers.post['Content-Type'] = 'multipart/form-data';
            let formData = this.getFormData(params);
            Vue.axios.post(`${resource}`, formData).then((response) => {
                resolve(response.data);
            }).catch((err) => {
                if (!err.response) {
                    const data = {
                        message: 'Api Bağlantı Hatası'
                    };
                    reject(data);
                } else {
                    const statusCode = err.response.status;
                    if (statusCode === 401) {
                        store.dispatch(LOGOUT)
                        router.push({ path: '/' })
                        reject(err.response.data)
                    } else if (statusCode === 404) {
                        reject(err.response.data);
                    }
                }
            }).finally(() => {
                store.dispatch(IS_LOADING);
            })
        })
    },

    getFormData(object) {
        let formData = new FormData();
        formData = objectToFormData(object);
        return formData;
    }
};

export default ApiService;