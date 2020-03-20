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
                <v-col class="pa-1">
                  <v-btn
                    v-if="item.language==='C#'"
                    color="toolbarColor"
                    dark
                    @click="showDependencies(item)"
                  >Visualize Dependency</v-btn>
                </v-col>
              </v-row>
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
      expandedItem: []
    };
  },
  methods: {
    showItems(){
      if(this.expandedItem.length ===0){
        this.repository.forEach((item)=>{
          this.expandedItem.push(item)
        })
      }else{
        this.expandedItem = [];
      }
    },
    showDependencies(item) {
      if (this.$socket.connected) {
        let indexOf = this.repository.indexOf(item);
        item.loading = true;
        this.$socket.emit("visualizedependency", item);
        this.repository[indexOf] = item;
      }
    }
  }
};
</script>