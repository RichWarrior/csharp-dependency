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
            <v-form v-model="valid">
              <v-row class="ma-0">
                <v-col cols="12" md="12" sm="12" class="text-center pa-1">
                  <span class="black--text">{{$t('login.title')}}</span>
                </v-col>
                <v-col class="pa-1" cols="12" md="12" sm="12">
                  <v-text-field
                    v-model="email"
                    outlined
                    :label="$t('login.email')"
                    :placeholder="$t('login.emailPlaceholder')"
                    type="email"
                    prepend-icon="fa fa-envelope"
                  ></v-text-field>
                  <v-text-field
                    v-model="password"
                    outlined
                    :label="$t('login.password')"
                    :placeholder="$t('login.passwordPlaceholder')"
                    :type="passwordFieldType"
                    prepend-icon="fa fa-key"
                    :append-icon="passwordAppendIcon"
                    @click:append="passwordAppendIconClick"
                  ></v-text-field>
                </v-col>
              </v-row>
              <v-card-actions>
                <v-spacer></v-spacer>
                <span class="caption">{{$t('login.forgotPassword')}}</span>
              </v-card-actions>
              <v-row>
                <v-col cols="12" md="12" sm="12">
                  <v-btn block color="#5D9CEC" dark @click="login">{{$t('login.login')}}</v-btn>
                </v-col>               
                <v-col cols="12" md="12" sm="12">
                  <v-btn block @click="$router.push({path:'/register'})">{{$t('login.register')}}</v-btn>
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
export default {
  data: () => ({
    valid: false,
    email: "",
    password: "",
    passwordFieldType: "password",
    passwordAppendIcon: "fa fa-eye",
    rememberMe: false
  }),
  methods: {
    passwordAppendIconClick() {
      this.passwordFieldType =
        this.passwordFieldType === "password" ? "text" : "password";
      this.passwordAppendIcon =
        this.passwordAppendIcon === "fa fa-eye"
          ? "fa fa-eye-slash"
          : "fa fa-eye";
    },
    login() {
      this.$router.push({ path: "/" });
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