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
        <v-menu class="hidden-sm-and-down">
          <template v-slot:activator="{ on, attrs }">
            <v-btn v-bind="attrs" v-on="on" icon>
              <v-icon>fa fa-flag</v-icon>
            </v-btn>
          </template>

          <v-list>
            <v-list-item @click="changeLanguage('tr')">
              <v-row class="ma-0">
                <country-flag country="tr" />
              </v-row>
            </v-list-item>
            <v-list-item @click="changeLanguage('en')">
              <v-row class="ma-0">
                <country-flag country="gb" />
              </v-row>
            </v-list-item>
          </v-list>
        </v-menu>
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
import changeLanguageEntity from "@/entity/request/user/ChangeLanguage";

const languageInitialize = () => {
  return Object.assign({}, changeLanguageEntity);
};

import { LOGOUT, CHANGE_LANGUAGE } from "@/store/actions.type";
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
    },
    changeLanguage(lang) {
      let langItem = languageInitialize();
      langItem.locale = lang;
      this.$store
        .dispatch(CHANGE_LANGUAGE, langItem)
        .then(() => {
          this.$i18n.locale = lang;
          window.location.reload();
        })
        .catch(err => {
          this.$swal(this.$t("base.errorTitle"), this.$t(err.message), "error");
        });
    }
  }
};
</script>