<template>
  <v-app>
    <v-content class="mb-5">
      <router-view />
    </v-content>
    <dashjs-footer :drawerStatus="drawer"></dashjs-footer>
    <v-overlay :value="$store.getters.getLoadingState">
      <vue-loaders-pacman scale="1"></vue-loaders-pacman>
    </v-overlay>
  </v-app>
</template>

<script>
export default {
  data: () => ({
    drawer: false
  })
};
</script>