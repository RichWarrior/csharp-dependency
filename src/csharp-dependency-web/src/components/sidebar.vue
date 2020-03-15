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
  data: () => ({
    menu: [
      {
        id: 1,
        parentMenuId: 0,
        name: "Dashboard",
        icon: "fa fa-tachometer-alt",
        route: "/"
      },
      {
        id: 2,
        parentMenuId: 0,
        name: "Widgets",
        icon: "fa fa-th-large",
        route: "/Widgets"
      },
      {
        id: 3,
        parentMenuId: 0,
        name: "Elements",
        icon: "fa fa-atom",
        route: "/Elements"
      },
      {
        id: 4,
        parentMenuId: 3,
        name: "Button",
        icon: "fa fa-mouse",
        route: "/Buttons"
      },
      {
        id: 5,
        parentMenuId: 3,
        name: "Notification",
        icon: "fa fa-comment-dots",
        route: "/Notification"
      },
      {
        id: 6,
        parentMenuId: 3,
        name: "Carousel",
        icon: "fa fa-hand-point-up",
        route: "/Carousel"
      },
      {
        id: 7,
        parentMenuId: 3,
        name: "Spinner",
        icon: "fa fa-spinner",
        route: "/Spinner"
      },
      {
        id: 8,
        parentMenuId: 3,
        name: "Tree",
        icon: "fas fa-stream",
        route: "/Tree"
      },
      {
        id: 9,
        parentMenuId: 3,
        name: "Cards",
        icon: "fas fa-id-card-alt",
        route: "/Cards"
      },
      {
        id: 10,
        parentMenuId: 0,
        name: "Forms",
        icon: "fab fa-wpforms",
        route: "/Forms"
      },
      {
        id: 11,
        parentMenuId: 10,
        name: "Standart",
        icon: "fa fa-font",
        route: "/Forms"
      },
      {
        id: 12,
        parentMenuId: 10,
        name: "Wizard",
        icon: "fa fa-magic",
        route: "/WizardForms"
      },
      {
        id: 13,
        parentMenuId: 0,
        name: "Charts",
        icon: "fas fa-chart-pie",
        route: "/Charts"
      },
      {
        id: 14,
        parentMenuId: 0,
        name: "Table",
        icon: "fa fa-table",
        route: "/Table"
      },
      {
        id: 15,
        parentMenuId: 0,
        name: "Pages",
        icon: "fas fa-folder",
        route: "/Pages"
      },
      {
        id: 16,
        parentMenuId: 15,
        name: "Login",
        icon: "fas fa-sign-in-alt",
        route: "/Login"
      },
      {
        id: 17,
        parentMenuId: 15,
        name: "Register",
        icon: "fas fa-user-plus",
        route: "/register"
      },
      {
        id: 18,
        parentMenuId: 15,
        name: "Recover Password",
        icon: "fa fa-unlock-alt",
        route: "/RecoverPassword"
      },
      {
        id: 19,
        parentMenuId: 15,
        name: "Lock",
        icon: "fa fa-lock",
        route: "/Lock"
      },
      {
        id: 20,
        parentMenuId: 15,
        name: "Not Found",
        icon: "fas fa-exclamation-triangle",
        route: "*"
      },
      {
        id: 21,
        parentMenuId: 15,
        name: "Error 500",
        icon: "fas fa-bug",
        route: "/500"
      },
      {
        id: 22,
        parentMenuId: 0,
        name: "Extras",
        icon: "fa fa-mug-hot",
        route: "/Extras"
      },
      {
        id: 23,
        parentMenuId: 22,
        name: "Bug Tracker",
        icon: "fa fa-bug",
        route: "/BugTracker"
      },
      {
        id: 24,
        parentMenuId: 22,
        name: "FAQ",
        icon: "fa fa-headset",
        route: "/faq"
      },
      {
        id: 25,
        parentMenuId: 22,
        name: "File Manager",
        icon: "fa fa-hdd",
        route: "/FileManager"
      },
      {
        id: 26,
        parentMenuId: 22,
        name: "Plans",
        icon: "fa fa-file-invoice-dollar",
        route: "/Plans"
      },
      {
        id: 27,
        parentMenuId: 22,
        name: "Timeline",
        icon: "fa fa-stopwatch",
        route: "/Timeline"
      },
      {
        id: 28,
        parentMenuId: 22,
        name: "Calendar",
        icon: "fa fa-calendar-alt",
        route: "/Calendar"
      },    
      {
        id:29,
        parentMenuId:3,
        name:'Progress Bars',
        icon:'fa fa-spinner',
        route:'/ProgressBar'
      },
      {
        id:30,
        parentMenuId:3,
        name:'Chips',
        icon:'fa fa-bookmark',
        route:'/Chips'
      },
      {
        id:31,
        parentMenuId:3,
        name:'Alerts',
        icon:'fa fa-exclamation',
        route:'/Alerts'
      },
      {
        id:32,
        parentMenuId:10,
        name:'Pickers',
        icon:'fa fa-crosshairs',
        route:'/Pickers'
      },
      {
        id:33,
        parentMenuId:22,
        name:'Payment',
        icon:'far fa-credit-card',
        route:'/Payment'
      },
      {
        id:34,
        parentMenuId:10,
        name:'Editors',
        icon:'fa fa-text-width',
        route:'/Editors'
      },
      {
        id:35,
        parentMenuId:3,
        name:'Tour',
        icon:'fas fa-map-marker-alt',
        route:'/Tour'
      }
    ]
  }),
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