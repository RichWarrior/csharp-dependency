<template>
  <v-row class="ma-0">
      <v-col class="pa-1">
          <repositorycomponent :repository="repositories"/> 
      </v-col>
  </v-row>
</template>

<script>
import repositorycomponent from '@/components/repository';

import { GET_REPOSITORY } from "@/store/actions.type";

export default {
  components: {
      repositorycomponent
  },
  data() {
    return {
      repositories: []
    };
  },
  created() {
    this.$store
      .dispatch(GET_REPOSITORY)
      .then(() => {
          this.repositories  = this.$store.getters.getRepositories;
          this.repositories.forEach((item)=>{
            item.loading = false;
            item.dependencies = []
          });
      })
      .catch(err => {
        this.$swal(this.$t("base.errorTitle"), this.$t(err.message), "error");
      });
  }
};
</script>