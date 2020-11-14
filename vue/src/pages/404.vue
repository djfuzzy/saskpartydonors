<template>
  <div class="oops">
    <main-layout>
      <section>
        <div class="content has-text-centered">
          <h1 class="title">Oops</h1>
          <p>
            Much like the Sask Party's responsibility to the Saskatchewan
            public, that page seems to be missing.
          </p>
          <v-link href="/">Go back to home</v-link>
        </div>
      </section>
    </main-layout>
  </div>
</template>

<script>
import VLink from "../components/VLink";
import MainLayout from "../layouts/Main.vue";
export default {
  components: {
    MainLayout,
    VLink
  }
};
</script>

<style scoped>
.oops {
  margin: 2em auto auto auto;
  padding-left: 1em;
  padding-right: 1em;
  max-width: 800px;
}
</style>
