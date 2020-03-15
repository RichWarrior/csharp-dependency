<template>
  <div class="sidebar">
    <v-navigation-drawer
      app
      v-model="drawerStatus"
      fixed
      color="sidebarColor"
      dark
      inset
      @transitionend="transitionend"
    >
      <v-app-bar dark color="toolbarColor" class="elevation-12">
        <v-toolbar-title class="text-uppercase">
          <span class="align-center">C#</span>
          <span class="font-weight-black">Dependency</span>
        </v-toolbar-title>
      </v-app-bar>
      <v-list>
        <div v-for="(item,index) in getMainMenu" v-bind:key="index">
          <v-list-item v-if="!isNested(item.id)" @click="route(item.id)">
            <v-list-item-icon>
              <v-icon class="white--text">{{item.icon}}</v-icon>
            </v-list-item-icon>

            <v-list-item-title class="white--text caption">{{item.name}}</v-list-item-title>
          </v-list-item>
          <v-list-group v-if="isNested(item.id)" color="white" :prepend-icon="item.icon">
            <template v-slot:activator>
              <v-list-item-title class="white--text caption">{{item.name}}</v-list-item-title>
            </template>

            <v-list-item
              v-for="(crud, i) in getSubMenu(item.id)"
              :key="i"
              @click="route(crud.id)"
              class="ma-2"
            >
              <v-list-item-action>
                <v-icon size="16" class="white--text">{{crud.icon}}</v-icon>
              </v-list-item-action>
              <v-list-item-title class="white--text text-left caption">{{crud.name}}</v-list-item-title>
            </v-list-item>
          </v-list-group>
        </div>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
export default {
  props: {
    drawerStatus: {
      required: true,
      type: Boolean
    }
  },
  data() {
    return {
      menu: [
        {
          id: 1,
          parentMenuId: 0,
          name: this.$t('menu.home'),
          icon: "fa fa-home",
          route: "/home"
        },
        {
          id: 2,
          parentMenuId: 0,
          name: this.$t('menu.followers'),
          icon: "fa fa-users",
          route: "/followers"
        },
        {
          id :3,
          parentMenuId:0,
          name:this.$t('menu.following'),
          icon:'fa fa-users',
          route:'/following'
        },
        {
          id:4,
          parentMenuId:0,
          name:this.$t('menu.starredRepo'),
          icon:'fa fa-star',
          route:'/starredrepo'
        },
        {
          id:5,
          parentMenuId:0,
          name:this.$t('menu.repository'),
          icon:'fas fa-code-branch',
          route:'/repository'
        }
      ]
    };
  },
  computed: {
    getMainMenu() {
      return this.menu.filter(x => x.parentMenuId === 0);
    }
  },
  methods: {
    transitionend() {
      if (!this.drawerStatus) {
        this.$emit("transtionend");
      }
    },
    isNested(menuId) {
      const arr = this.menu.filter(x => x.parentMenuId === menuId);
      return arr.length > 0 ? true : false;
    },
    getSubMenu(menuId) {
      return this.menu.filter(x => x.parentMenuId === menuId);
    },
    route(menuId) {
      const menu = this.menu.filter(x => x.id === menuId);
      if (menu.length > 0) {
        this.$router.push({ path: menu[0].route });
      }
    }
  }
};
</script>