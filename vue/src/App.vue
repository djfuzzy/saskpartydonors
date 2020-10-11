<script>
import { h } from 'vue';
import List from './pages/List.vue';
import routes from './routes';

export default {
  name: 'App',
  components: {
    List,
  },
  data: () => ({
    currentRoute: window.location.pathname,
  }),

  computed: {
    ViewComponent() {
      if (this.currentRoute === '/') {
        return require(`./App.vue`).default;
      }
      const matchingPage = routes[this.currentRoute] || '404';
      return require(`./pages/${matchingPage}.vue`).default;
    },
  },

  render() {
    return h(this.ViewComponent);
  },

  created() {
    window.addEventListener('popstate', () => {
      this.currentRoute = window.location.pathname;
    });
  },
};
</script>

<style>
#app {
  margin: 2em auto auto auto;
  padding-left: 1em;
  padding-right: 1em;
  max-width: 800px;
}
</style>
