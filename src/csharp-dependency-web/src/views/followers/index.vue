<template>
  <v-row class="ma-0">
    <v-col class="pa-1">
      <followerscomponent :followers="followers" />
    </v-col>
  </v-row>
</template>

<script>
import followerscomponent from "@/components/followers";

import { GET_FOLLOWERS } from "@/store/actions.type";

export default {
  components: {
    followerscomponent
  },
  data() {
    return {
      followers: []
    };
  },
  created() {
    this.$store
      .dispatch(GET_FOLLOWERS)
      .then(() => {
          this.followers = this.$store.getters.getFollowers;
      })
      .catch(err => {
        this.$swal(this.$t("base.errorTitle"), this.$t(err.message), "error");
      });
  }
};
</script>