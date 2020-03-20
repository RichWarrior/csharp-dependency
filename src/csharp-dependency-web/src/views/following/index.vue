<template>
  <v-row class="ma-0">
    <v-col class="pa-1">
      <followingcomponent :following="followings" />
    </v-col>
  </v-row>
</template>

<script>
import followingcomponent from "@/components/following";

import { GET_FOLLOWING } from "@/store/actions.type";

export default {
  components: {
    followingcomponent
  },
  data() {
    return {
      followings: []
    };
  },
  created() {
    this.$store
      .dispatch(GET_FOLLOWING)
      .then(() => {
        this.followings = this.$store.getters.getFollowing;
      })
      .catch(err => {
        this.$swal(this.$t("base.errorTitle"), this.$t(err.message), "error");
      });
  }
};
</script>