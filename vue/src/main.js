import Buefy from "buefy";
import "buefy/dist/buefy.css";
import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faAngleLeft,
  faAngleRight,
  faArrowUp,
  faBuilding,
  faChevronDown,
  faChevronUp,
  faDownload,
  faInfoCircle,
  faLandmark,
  faSearch,
  faSearchDollar,
  faToolbox,
  faUser
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import JsonCSV from "vue-json-csv";
import Vue from "vue";
import VueCurrencyFilter from "vue-currency-filter";
import routes from "./routes";
import store from "./store";
// import './assets/style.scss';

Vue.config.productionTip = false;

library.add(
  faAngleLeft,
  faAngleRight,
  faArrowUp,
  faBuilding,
  faChevronDown,
  faChevronUp,
  faDownload,
  faInfoCircle,
  faLandmark,
  faSearch,
  faSearchDollar,
  faToolbox,
  faUser
);
Vue.component("vue-fontawesome", FontAwesomeIcon);
Vue.use(Buefy, {
  defaultIconComponent: "vue-fontawesome",
  defaultIconPack: "fas"
});

Vue.component("downloadCsv", JsonCSV);

Vue.use(VueCurrencyFilter, {
  symbol: "$",
  thousandsSeparator: ",",
  fractionCount: 2,
  fractionSeparator: ".",
  symbolPosition: "front",
  symbolSpacing: true,
  avoidEmptyDecimals: undefined
});

const app = new Vue({
  el: "#app",
  store,
  data: {
    currentRoute: window.location.pathname
  },
  computed: {
    ViewComponent() {
      const matchingPage = routes[this.currentRoute];
      return matchingPage
        ? require(`./pages/${matchingPage}.vue`).default
        : require("./pages/404.vue").default;
    }
  },
  render(h) {
    return h(this.ViewComponent);
  }
});

window.addEventListener("popstate", () => {
  app.currentRoute = window.location.pathname;
});
