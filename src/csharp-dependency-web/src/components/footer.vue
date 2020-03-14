<template>
  <div>
    <v-footer height="50" dark class="transparent elevation-12 hidden-md-and-down" inset app>
      <span class="black--text">
          <strong>DashJs Footer</strong>
        </span>
        <div class="flex-grow-1"></div>
        <span class="black--text">
          <strong>Version: {{version}}</strong>
        </span>
    </v-footer>
    <v-footer height="50" dark class="transparent elevation-12 hidden-lg-and-up" inset app v-if="!drawerStatus">
        <span class="black--text">
          <strong>DashJs Footer</strong>
        </span>
        <div class="flex-grow-1"></div>
        <span class="black--text">
          <strong>Version: {{version}}</strong>
        </span>
    </v-footer>
  </div>
</template>

<script>
export default {
  props: {
    drawerStatus: {
      type: Boolean,
      required: true,
    }
  },
  data: () => ({
    version: ""
  }),
  created() {
    let versionCode = JSON.stringify(require("../../package.json").version);
    this.version = versionCode.replace('"','').replace('"','');
  }
};
</script>

<style scoped>
.transparent {
  background-color: white !important;
  opacity: 0.65;
  border-color: transparent !important;
}
</style>