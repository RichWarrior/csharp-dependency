<template>
  <v-card>
    <v-toolbar>
      <v-toolbar-title>
        <span class="title">{{$t('repository.title')}}</span>
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
            :items="repository"
            show-expand
            :single-expand="false"
            :expanded.sync="expandedItem"
          >
            <template v-slot:expanded-item="{ item }">
              <v-row class="ma-3">
                <v-col class="pa-1" cols="1">
                  <v-btn
                    v-if="item.language==='C#'"
                    :disabled="item.loading || item.dependencies.length>0"
                    color="toolbarColor"
                    @click="showDependencies(item)"
                  >
                    <span class="white--text">{{$t('base.visualizeDependency')}}</span>
                  </v-btn>
                </v-col>
              </v-row>
              <v-row v-if="item.language==='C#'" class="ma-3">
                <v-col
                  class="pa-1 text-center"
                  cols="12"
                  v-for="(item,index) in item.dependencies"
                  :key="index"
                >
                  <v-card>
                    <v-toolbar>
                      <v-col class="pa-1 text-left">
                        <v-toolbar-title>
                          <span>{{item.name}}</span>
                        </v-toolbar-title>
                      </v-col>
                      <v-col class="pa-1 text-right">
                        <v-toolbar-title>
                          <span>{{item.targetFramework}}</span>
                        </v-toolbar-title>
                      </v-col>
                    </v-toolbar>
                    <v-card-text>
                      <v-data-table :headers="dependencyFields" :items="item.references">
                        <template v-slot:item.includeType="{item}">
                          <span v-if="item.includeType === 1">{{$t('repository.package')}}</span>
                          <span v-if="item.includeType === 2">{{$t('repository.project')}}</span>
                        </template>
                      </v-data-table>
                    </v-card-text>
                  </v-card>
                </v-col>
              </v-row>
              <v-row class="ma-3" v-else>Desteklenmeyen Proje Tipi</v-row>
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
    repository: {
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
      dependencyFields: [
        {
          value: "include",
          text: this.$t("repository.dependencyField.include"),
          sortable: false,
          align: "left"
        },
        {
          value: "includeType",
          text: this.$t("repository.dependencyField.includeType"),
          sortable: false,
          align: "left"
        },
        {
          value: "version",
          text: this.$t("repository.dependencyField.version"),
          sortable: false,
          align: "left"
        }
      ],
      expandedItem: []
    };
  },
  methods: {
    showItems() {
      if (this.expandedItem.length === 0) {
        this.repository.forEach(item => {
          this.expandedItem.push(item);
        });
      } else {
        this.expandedItem = [];
      }
    },
    showDependencies(item) {
      if (this.$socket.connected) {
        let indexOf = this.repository.indexOf(item);
        item.loading = false;
        this.repository[indexOf] = item;
        this.$socket.emit("visualizedependency", item);
        this.$swal(
          this.$t("base.succcessTitle"),
          this.$t("repository.dependencySwalText"),
          "success"
        );
        this.expandedItem = [];
      }
    }
  },
  created() {
    this.sockets.subscribe("_showDependency", result => {
      var item = this.repository.filter(x => x.id === result[0].id)[0];
      if (item !== undefined) {
        let indexOf = this.repository.indexOf(item);
        item.loading = false;
        item.dependencies = result[1].projects;
        this.repository[indexOf] = item;
        this.expandedItem.push(item);
      }
    });
  }
};
</script>