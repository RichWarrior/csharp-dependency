<template>
  <v-container fill-height>
    <v-row align="center" justify="center">
      <v-col cols="12" class="text-center pa-1">
       <span class="display-1 black--text">404</span>
      </v-col>
      <v-col cols="12" class="text-center pa-1">
          <span class="caption">{{$t('error.pageNotFound')}}</span>
          <br>
          <span class="caption">{{$t('error.pageNotFound2')}}</span>
      </v-col>      
      <v-col cols="12" class="text-center">
          <span class="caption link" @click="$router.push({path:'/'})">{{$t('error.gotoApp')}}</span>   
      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import { CHANGE_BLANK_STATUS } from "@/store/actions.type";
export default {
    data:() => ({
        search:''
    }),
  created() {
    this.$store.dispatch(CHANGE_BLANK_STATUS, true);
  }
};
</script>

<style scoped>
.link{
    cursor: pointer;
}
</style>