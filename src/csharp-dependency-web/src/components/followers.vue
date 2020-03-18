<template>
  <v-card>
    <v-toolbar>
      <v-toolbar-title>
        <h1 class="title">{{$t('followers.title')}}</h1>
      </v-toolbar-title>
    </v-toolbar>
    <v-card-text class="pa-0">
      <v-row class="ma-0">
        <v-col class="pa-0">
          <v-data-table class="elevation-12" :headers="fields" :items="followers">
            <template v-slot:item.avatar_url="{item}">
              <v-avatar>
                <img :src="item.avatar_url" :alt="item.login" />
              </v-avatar>
            </template>
          </v-data-table>
        </v-col>
      </v-row>
    </v-card-text>
  </v-card>
</template>

<script>
export default {
  props: {
    followers: {
      required: true,
      type: Array
    }
  },
  data() {
    return {
      fields: [
        {
          value: "login",
          text: this.$t('followers.tableFields.name'),
          align:'left',
          sortable: false
        },
        {
          value: "avatar_url",
          align:'left',
          text: this.$t('followers.tableFields.avatar_url'),
          sortable: false
        }
      ]
    };
  }
};
</script>