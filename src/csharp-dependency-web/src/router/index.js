import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'login',
    component: () =>import('../views/user/login')
  },
  {
    path:'/register',
    component:()=>import('../views/user/register')
  },
  {
    path:'/home',
    component:()=>import('../views/home/index')
  },
  {
    path:'/followers',
    component:() =>import('../views/followers')
  },
  {
    path:'/following',
    component:()=>import('../views/following')
  },
  {
    path:'/starred',
    component:()=>import('../views/starred')
  },
  {
    path:'/repo',
    component:()=>import('../views/repository')
  },
  {
    path:'*',
    component:()=>import('../views/error/404')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
