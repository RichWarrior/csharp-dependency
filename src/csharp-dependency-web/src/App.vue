<template>
  <v-app>
  <dashjs-authorize v-if="$store.getters.isAuthenticated && !$store.getters.isBlankTemplate"></dashjs-authorize>
    <dashjs-unauthorize v-if="!$store.getters.isAuthenticated && !$store.getters.isBlankTemplate"></dashjs-unauthorize>
    <dashjs-blank v-if="$store.getters.isBlankTemplate"></dashjs-blank>
  </v-app>
</template>

<script>

export default {
  name: 'App',
};
</script>
