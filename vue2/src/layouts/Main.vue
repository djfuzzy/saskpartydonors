<template>
  <div id="main">
    <slot></slot>
    <SiteFooter />
  </div>
</template>

<script>
import SiteFooter from '../components/SiteFooter';

export default {
  components: {
    SiteFooter,
  },
};
</script>

<style>
#main {
  margin: 2em auto auto auto;
  padding-left: 1em;
  padding-right: 1em;
  max-width: 800px;
}
</style>
