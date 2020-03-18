<template>
  <v-card>
    <v-toolbar>
      <v-toolbar-title>
        <h1 class="title">{{$t('starred.title')}}</h1>
      </v-toolbar-title>
    </v-toolbar>
    <v-card-text class="pa-0">
      <v-row class="ma-0">
        <v-col class="pa-0">
          <v-data-table class="elevation-12" :headers="fields" :items="starredRepository">
            <template v-slot:item.visualize="{item}">
              <v-icon color="secondary" v-if="item.language === 'C#'" @click="showDependecies(item)">fa fa-laptop-code</v-icon>
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
    starredRepository: {
      required: true,
      type: Array
    }
  },
  data() {
    return {
      fields: [
        {
          value: "name",
          text: this.$t("repository.tableFields.name"),
          sortable: false,
          align: "left"
        },
        {
          value: "language",
          text: this.$t("repository.tableFields.language"),
          sortable: false,
          align: "left"
        },
        {
          value: "visualize",
          text: this.$t("repository.tableFields.visualize"),
          sortable: false,
          align: "left"
        }
      ]
    };
  },
  methods:{
      showDependecies(item){
          console.log(item)
      }
  }
};
</script>