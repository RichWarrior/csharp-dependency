<template>
  <div class="toolbar">
    <v-app-bar app dark color="toolbarColor">
      <v-app-bar-nav-icon text @click="drawerStatusChange">
        <v-icon>fa fa-bars</v-icon>
      </v-app-bar-nav-icon>
      <v-toolbar-title class="text-uppercase hidden-lg-and-up">
        <span>C#</span>
        <span class="font-weight-black">Dependency</span>
      </v-toolbar-title>
      <v-toolbar-title class="text-uppercase hidden-md-and-down" v-if="!drawerStatus">
        <span>C#</span>
        <span class="font-weight-black">Dependency</span>
      </v-toolbar-title>
      <v-row class="ml-2 hidden-sm-and-down">
        <v-spacer></v-spacer>
        <v-btn icon @click="logOut">
          <v-icon small>fa fa-sign-out-alt</v-icon>
        </v-btn>
      </v-row>
      <v-row justify="end" class="hidden-sm-and-up">
        <v-menu>
          <template v-slot:activator="{ on, attrs }">
            <v-btn v-bind="attrs" v-on="on" icon>
              <v-icon>fa fa-ellipsis-h</v-icon>
            </v-btn>
          </template>

          <v-list>          
            <v-list-item @click="logOut">
              <v-row class="ma-0">
                <v-icon small>fa fa-sign-out-alt</v-icon>
                <v-col class="pa-1">
                  <span>{{$t('base.logOut')}}</span>
                </v-col>
              </v-row>
            </v-list-item>
          </v-list>
        </v-menu>
      </v-row>
    </v-app-bar>
  </div>
</template>

<script>
import { LOGOUT } from "@/store/actions.type";
export default {
  name: "Toolbar",
  props: {
    drawerStatus: {
      type: Boolean,
      required: true
    }
  },
  methods: {
    drawerStatusChange() {
      this.$emit("DrawerStatusChange");
    },
    logOut() {
      this.$store.dispatch(LOGOUT).then(() => {
        this.$router.push({ path: "/" });
      });
    }
  }
};
</script>