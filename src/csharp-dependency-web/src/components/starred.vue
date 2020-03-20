<template>
  <v-card>
    <v-toolbar>
      <v-toolbar-title>
        <h1 class="title">{{$t('starred.title')}}</h1>
      </v-toolbar-title>
    </v-toolbar>
    <v-card-text class="ma-0">
      <v-row class="ma-0 mb-2">
        <v-col class="pa-0">
          <v-btn color="toolbarColor" dark @click="showItems">
            <v-icon>fa fa-arrows-alt-v</v-icon>
          </v-btn>
        </v-col>
      </v-row>
      <v-row class="ma-0">
        <v-col class="pa-0">
          <v-data-table
            class="elevation-12"
            :headers="fields"
            :items="starredRepository"
            show-expand
            :single-expand="false"
            :expanded.sync="expandedItem"
          >
            <template v-slot:expanded-item="{ headers, item }">
              <td :colspan="headers.length">More info about {{ item.name }}</td>
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
        { text: "", value: "data-table-expand" }
      ],
      expandedItem: []
    };
  },
  methods: {
    showItems() {
      if (this.expandedItem.length === 0) {
        this.starredRepository.forEach(item => {
          this.expandedItem.push(item);
        });
      } else {
        this.expandedItem = [];
      }
    },
    showDependecies(item) {
      console.log(item);
    }
  }
};
</script>