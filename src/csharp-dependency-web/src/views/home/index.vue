<template>
  <v-row class="ma-2">
    <v-col class="pa-1" cols="12" md="6">
      <v-card class="elevation-12">
        <v-card-text class="pa-0">
          <v-sheet tile color="toolbarColor" height="100%" width="100%" class="pa-2">
            <v-row class="ma-0" justify="center" align="center">
              <v-col class="pa-1 text-left" cols="6">
                <v-icon size="64" color="white">fa fa-users</v-icon>
              </v-col>
              <v-col class="pa-2 text-right" cols="6">
                <h1 class="display-1 white--text">{{followers.length}}</h1>
                <h1 class="title white--text">{{$t('followers.title')}}</h1>
              </v-col>
            </v-row>
          </v-sheet>
        </v-card-text>
        <v-card-text class="pa-0">
          <v-sheet tile color="sidebarColor" height="100%" width="100%" class="pa-2">
            <v-row class="ma-0" justify="center" align="center">
              <v-col class="pa-1 text-left" cols="6">
                <h1 class="caption white--text">{{$t('followers.viewDetails')}}</h1>
              </v-col>
              <v-col class="pa-1 text-right" cols="6">
                <v-btn icon small>
                  <v-icon color="white">fa fa-arrow-right</v-icon>
                </v-btn>
              </v-col>
            </v-row>
          </v-sheet>
        </v-card-text>
      </v-card>
    </v-col>
    <v-col class="pa-1" cols="12" md="6">
      <v-card class="elevation-12">
        <v-card-text class="pa-0">
          <v-sheet tile color="toolbarColor" height="100%" width="100%" class="pa-2">
            <v-row class="ma-0" justify="center" align="center">
              <v-col class="pa-1 text-left" cols="6">
                <v-icon size="64" color="white">fa fa-users</v-icon>
              </v-col>
              <v-col class="pa-2 text-right" cols="6">
                <h1 class="display-1 white--text">{{followings.length}}</h1>
                <h1 class="title white--text">{{$t('following.title')}}</h1>
              </v-col>
            </v-row>
          </v-sheet>
        </v-card-text>
        <v-card-text class="pa-0">
          <v-sheet tile color="sidebarColor" height="100%" width="100%" class="pa-2">
            <v-row class="ma-0" justify="center" align="center">
              <v-col class="pa-1 text-left" cols="6">
                <h1 class="caption white--text">{{$t('following.viewDetails')}}</h1>
              </v-col>
              <v-col class="pa-1 text-right" cols="6">
                <v-btn icon small>
                  <v-icon color="white">fa fa-arrow-right</v-icon>
                </v-btn>
              </v-col>
            </v-row>
          </v-sheet>
        </v-card-text>
      </v-card>
    </v-col>
    <v-col class="pa-1" cols="12" md="6">
      <v-card class="elevation-12">
        <v-card-text class="pa-0">
          <v-sheet tile color="toolbarColor" height="100%" width="100%" class="pa-2">
            <v-row class="ma-0" justify="center" align="center">
              <v-col class="pa-1 text-left" cols="6">
                <v-icon size="64" color="white">fas fa-code-branch</v-icon>
              </v-col>
              <v-col class="pa-2 text-right" cols="6">
                <h1 class="display-1 white--text">{{repositories.length}}</h1>
                <h1 class="title white--text">{{$t('repository.title')}}</h1>
              </v-col>
            </v-row>
          </v-sheet>
        </v-card-text>
        <v-card-text class="pa-0">
          <v-sheet tile color="sidebarColor" height="100%" width="100%" class="pa-2">
            <v-row class="ma-0" justify="center" align="center">
              <v-col class="pa-1 text-left" cols="6">
                <h1 class="caption white--text">{{$t('repository.viewDetails')}}</h1>
              </v-col>
              <v-col class="pa-1 text-right" cols="6">
                <v-btn icon small>
                  <v-icon color="white">fa fa-arrow-right</v-icon>
                </v-btn>
              </v-col>
            </v-row>
          </v-sheet>
        </v-card-text>
      </v-card>
    </v-col>
    <v-col class="pa-1" cols="12" md="6">
      <v-card class="elevation-12">
        <v-card-text class="pa-0">
          <v-sheet tile color="toolbarColor" height="100%" width="100%" class="pa-2">
            <v-row class="ma-0" justify="center" align="center">
              <v-col class="pa-1 text-left" cols="6">
                <v-icon size="64" color="white">fa fa-star</v-icon>
              </v-col>
              <v-col class="pa-2 text-right" cols="6">
                <h1 class="display-1 white--text">{{starredRepositories.length}}</h1>
                <h1 class="title white--text">{{$t('starred.title')}}</h1>
              </v-col>
            </v-row>
          </v-sheet>
        </v-card-text>
        <v-card-text class="pa-0">
          <v-sheet tile color="sidebarColor" height="100%" width="100%" class="pa-2">
            <v-row class="ma-0" justify="center" align="center">
              <v-col class="pa-1 text-left" cols="6">
                <h1 class="caption white--text">{{$t('starred.viewDetails')}}</h1>
              </v-col>
              <v-col class="pa-1 text-right" cols="6">
                <v-btn icon small>
                  <v-icon color="white">fa fa-arrow-right</v-icon>
                </v-btn>
              </v-col>
            </v-row>
          </v-sheet>
        </v-card-text>
      </v-card>
    </v-col>
    <v-col class="pa-1" cols="12" md="6">
      <followerscomponent :followers="followers" />
    </v-col>
    <v-col class="pa-1" cols="12" md="6">
      <followingcomponent :following="followings" />
    </v-col>
    <v-col class="pa-1" cols="12" md="6">
      <repositorycomponent :repository="repositories" />
    </v-col>
    <v-col class="pa-1" cols="12" md="6">
      <starredrepository :starredRepository="starredRepositories" />
    </v-col>
  </v-row>
</template>

<script>
import { GET_GITHUB_USER } from "@/store/actions.type";

import followerscomponent from "@/components/followers";
import followingcomponent from "@/components/following";
import repositorycomponent from "@/components/repository";
import starredrepository from '@/components/starred';

export default {
  components: {
    followerscomponent,
    followingcomponent,
    repositorycomponent,
    starredrepository
  },
  data() {
    return {
      user: {},
      followers: [],
      followings: [],
      starredRepositories: [],
      repositories: []
    };
  },
  created() {
    this.$store
      .dispatch(GET_GITHUB_USER)
      .then(() => {
        this.user = this.$store.getters.getGithubUser;
        this.followers = this.$store.getters.getGithubFollowers;
        this.followings = this.$store.getters.getGithubFollowings;
        this.starredRepositories = this.$store.getters.getGithubStarredRepository;
        this.repositories = this.$store.getters.getGithubRepository;
      })
      .catch(err => {
        this.$swal(this.$t("base.errorTitle"), this.$t(err.message), "error");
      });
  }
};
</script>