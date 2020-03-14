<template>
    <v-app>
    <v-content class="mb-5">
      <router-view />
    </v-content>
    <dashjs-footer :drawerStatus="drawer"></dashjs-footer>
  </v-app>
</template>

<script>
export default {
  data : () => ({
    drawer:false
  })
}
</script>