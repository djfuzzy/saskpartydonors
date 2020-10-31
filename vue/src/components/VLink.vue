<template>
  <a :href="href" :class="linkClass" @click.prevent="go" :icon-right="icon">
    <slot></slot>
  </a>
</template>

<script>
import routes from '../routes';
export default {
  props: {
    href: {
      type: String,
      required: true,
    },
    linkClass: { type: String, required: false },
    icon: { type: String, required: false },
  },
  methods: {
    go() {
      event.preventDefault();
      this.$root.currentRoute = this.href;
      window.history.pushState(null, routes[this.href], this.href);
    },
  },
};
</script>
