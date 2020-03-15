<template>
  <v-row align="center" justify="center" class="ma-3">
    <v-col class="pa-0" cols="12" md="4" sm="10">
      <v-hover v-slot:default="{hover}">
        <v-card :elevation="hover ? 12 : 2">
          <v-toolbar color="#3A3F51" dark>
            <v-row class="ma-0">
              <v-col class="pa-1 text-center">
                <v-toolbar-title>
                  <span>CSharp Dependency</span>
                </v-toolbar-title>
              </v-col>
            </v-row>
          </v-toolbar>
          <v-card-text>
            <v-form v-model="valid" autocomplete="off">
              <v-row class="ma-0">
                <v-col cols="12" md="12" sm="12" class="text-center pa-1">
                  <span class="black--text">{{$t('register.title')}}</span>
                </v-col>
                <v-col class="pa-1" cols="12" md="12" sm="12">
                  <v-text-field
                    id="github_username"
                    v-model="registerItem.github_username"
                    :rules="usernameRule"
                    :label="$t('register.github')"
                    :placeholder="$t('register.githubPlaceholder')"
                    type="text"
                    prepend-icon="fab fa-github"
                    @change="githubUserChanged"
                    outlined
                  ></v-text-field>
                  <v-text-field
                    v-model="registerItem.email"
                    :rules="emailRule"
                    :label="$t('register.email')"
                    :placeholder="$t('register.emailPlaceholder')"
                    type="email"
                    prepend-icon="fa fa-envelope"
                    outlined
                  ></v-text-field>
                  <v-text-field
                    v-model="registerItem.password"
                    :rules="passwordRule"
                    :label="$t('register.password')"
                    :placeholder="$t('register.passwordPlaceholder')"
                    :type="passwordFieldType"
                    prepend-icon="fa fa-key"
                    :append-icon="passwordAppendIcon"
                    @click:append="passwordAppendIconClick"
                    outlined
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="12" md="12" sm="12">
                  <v-btn :disabled="!valid" block color="#5D9CEC" @click="register">
                    <span class="white--text">{{$t('register.title')}}</span>
                  </v-btn>
                </v-col>
                <v-col cols="12" md="12" sm="12">
                  <v-btn block @click="$router.push({path:'/'})">{{$t('register.login')}}</v-btn>
                </v-col>
              </v-row>
            </v-form>
          </v-card-text>
        </v-card>
      </v-hover>
    </v-col>
  </v-row>
</template>

<script>
import checkGithubUserEntity from "@/entity/request/user/CheckGithubUser";
import registerEntity from "@/entity/request/user/Register";

const checkGithubUserInitialize = () => {
  return Object.assign({}, checkGithubUserEntity);
};

const registerInitialize = () => {
  return Object.assign({}, registerEntity);
};

import { CHECK_GITHUB_USER,REGISTER } from "@/store/actions.type";

export default {
  data() {
    return {
      valid: false,
      registerItem: registerInitialize(),
      usernameRule: [v => !!v || this.$t("register.githubRule")],
      emailRule: [
        v => !!v || this.$t("register.emailRule.required"),
        v => /.+@.+\..+/.test(v) || this.$t("register.emailRule.valid")
      ],
      passwordRule: [v => !!v || this.$t("register.passwordRule")],
      passwordFieldType: "password",
      passwordAppendIcon: "fa fa-eye"
    };
  },
  methods: {
    passwordAppendIconClick() {
      this.passwordFieldType =
        this.passwordFieldType === "password" ? "text" : "password";
      this.passwordAppendIcon =
        this.passwordAppendIcon === "fa fa-eye"
          ? "fa fa-eye-slash"
          : "fa fa-eye";
    },
    githubUserChanged() {
      if (!this.registerItem.github_username) return;
      let entity = checkGithubUserInitialize();
      entity.username = this.registerItem.github_username;
      this.$store.dispatch(CHECK_GITHUB_USER, entity).catch((err)=>{
        this.$swal(this.$t('base.errorTitle'),this.$t(err.message),'error');
        this.valid = false;
      });
    },
    register() {
      if(this.valid){
        this.$store.dispatch(REGISTER,this.registerItem).then((payload)=>{
          this.$swal(this.$t('base.succcessTitle'),this.$t(payload.message),'success')
          this.registerItem = registerInitialize();
          this.$router.push({path:'/'})
        }).catch((err)=>{
          this.$swal(this.$t('base.errorTitle'),this.$t(err.message),'error')
        })
      }
    }
  }
};
</script>

<style scoped>
.caption {
  cursor: pointer;
}
.subtitle-1 {
  cursor: pointer;
}
</style>