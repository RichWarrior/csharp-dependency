<template>
  <h1 class="title">Index</h1>
</template>

<script>
import { GET_GITHUB_USER } from "@/store/actions.type";

export default {
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