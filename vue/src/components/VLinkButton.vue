<template>
  <b-button
    :click="url"
    :class="linkClass"
    @click.prevent="go"
    :icon-left="icon"
  >
    <slot></slot>
  </b-button>
</template>

<script>
import routes from '../routes';
export default {
  props: {
    url: {
      type: String,
      required: true,
    },
    linkClass: { type: String, required: false },
    icon: { type: String, required: false },
  },
  methods: {
    go() {
      event.preventDefault();
      this.$root.currentRoute = this.url;
      window.history.pushState(null, routes[this.url], this.url);
    },
  },
};
</script>
