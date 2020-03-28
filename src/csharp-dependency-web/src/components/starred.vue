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
                          <span v-if="item.includeType === 1">{{$t('starred.package')}}</span>
                          <span v-if="item.includeType === 2">{{$t('starred.project')}}</span>
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
          text: this.$t("starred.tableFields.name"),
          sortable: false,
          align: "left"
        },
        {
          value: "language",
          text: this.$t("starred.tableFields.language"),
          sortable: false,
          align: "left"
        },
        { text: "", value: "data-table-expand" }
      ],
      dependencyFields: [
       {
          value: "include",
          text: this.$t("starred.dependencyField.include"),
          sortable: false,
          align: "left"
        },
        {
          value: "includeType",
          text: this.$t("starred.dependencyField.includeType"),
          sortable: false,
          align: "left"
        },
        {
          value: "version",
          text: this.$t("starred.dependencyField.version"),
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
        this.starredRepository.forEach(item => {
          this.expandedItem.push(item);
        });
      } else {
        this.expandedItem = [];
      }
    },
    showDependencies(item) {
      if (this.$socket.connected) {
        let indexOf = this.starredRepository.indexOf(item);
        item.loading = true;
        this.starredRepository[indexOf] = item;
        this.$socket.emit("visualizedependency", item);
        this.$swal(
          this.$t("base.succcessTitle"),
          this.$t("starred.dependencySwalText"),
          "success"
        );
        this.expandedItem = [];
      }
    }
  },
  created() {
    this.sockets.subscribe("_showDependency", result => {
      var item = this.starredRepository.filter(x => x.id === result[0].id)[0];
      if (item !== undefined) {
        let indexOf = this.starredRepository.indexOf(item);
        item.loading = false;
        item.dependencies = result[1].projects;
        this.starredRepository[indexOf] = item;
        this.expandedItem.push(item);
      }
    });
  }
};
</script>