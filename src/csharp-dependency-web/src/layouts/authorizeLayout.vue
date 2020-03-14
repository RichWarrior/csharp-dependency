<template>
  <v-app>
    <dashjs-toolbar v-on:DrawerStatusChange="DrawerStatusChange" :drawerStatus="drawerStatus"></dashjs-toolbar>
    <dashjs-sidebar :drawerStatus="drawerStatus" v-on:transtionend="transtionend"></dashjs-sidebar>
    <v-content class="mb-5">
      <transition name="fade" mode="out-in">
        <router-view />
      </transition>
    </v-content>
    <dashjs-footer :drawerStatus="drawerStatus"></dashjs-footer>
  </v-app>
</template>

<script>
export default {
  data: () => ({
    drawerStatus: true,
    prevHeight: 0
  }),
  methods: {
    DrawerStatusChange() {
      this.drawerStatus = !this.drawerStatus;
    },
    transtionend() {
      this.drawerStatus = false;
    }
  }
};
</script>

<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition-duration: 0.3s;
  transition-property: opacity;
  transition-timing-function: ease;
}
.fade-enter,
.fade-leave-active {
  opacity: 0;
}
</style>