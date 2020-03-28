<template>
  <v-row class="ma-0">
    <v-col class="pa-1">
      <starredcomponent :starredRepository="starredRepositories" />
    </v-col>
  </v-row>
</template>

<script>
import starredcomponent from "@/components/starred";

import { GET_STARRED } from "@/store/actions.type";

export default {
  components: {
    starredcomponent
  },
  data() {
    return {
      starredRepositories: []
    };
  },
  created() {
    this.$store
      .dispatch(GET_STARRED)
      .then(() => {
        this.starredRepositories = this.$store.getters.getStarredRepositories;
        this.starredRepositories.forEach(item => {
          item.loading = false;
          item.dependencies = [];
        });
      })
      .catch(err => {
        this.$swal(this.$t("base.errorTitle"), this.$t(err.message), "error");
      });
  }
};
</script>