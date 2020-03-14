import VueSweetalert2 from "vue-sweetalert2";
import "sweetalert2/dist/sweetalert2.css";
import moment from "moment";
import "vue-loaders/dist/vue-loaders.css";
import VueLoaders from "vue-loaders";

//Components
import toolbar from "@/components/toolbar";
import sidebar from "@/components/sidebar";
import footer from "@/components/footer";

//Layouts
import authorizelayout from "../layouts/authorizeLayout";
import blanklayout from "@/layouts/blankLayout";
import unauthorizelayout from "@/layouts/unAuthorizedLayout";

function setupComponents(Vue) {
  Vue.use(VueSweetalert2);
  Vue.use(VueLoaders);
  Vue.use(require("vue-moment"));
  Vue.prototype.moment = moment;
  moment.locale("tr");

  //Defined Components
  Vue.component("dashjs-toolbar", toolbar);
  Vue.component("dashjs-sidebar", sidebar);
  Vue.component("dashjs-footer", footer);

  //Defined Layouts
  Vue.component("dashjs-authorize", authorizelayout);
  Vue.component("dashjs-unauthorize", unauthorizelayout);
  Vue.component("dashjs-blank", blanklayout);

}

export { setupComponents };
