<template>
  <main-layout>
    <section class="contributions-list mt-4">
      <div class="container">
        <v-link href="/">Go back to home</v-link>
        <ContributionsTable :contributions="contributions" />
      </div>
    </section>
  </main-layout>
</template>

<script>
import { ref } from 'vue';
import ContributionsTable from '../components/ContributionsTable.vue';
import VLink from '../components/VLink';
import MainLayout from '../layouts/Main.vue';

export default {
  name: 'List',
  components: {
    MainLayout,
    ContributionsTable,
    VLink,
  },
  setup() {
    const contributions = ref([
      {
        contributorName: 'John Smith',
        date: 'June 1, 2019',
        contributorType: 'Individual',
        amount: 275.0,
      },
      {
        contributorName: 'Appleseed Industries',
        date: 'May 31, 2019',
        contributorType: 'Corporation',
        amount: 1000.0,
      },
      {
        contributorName: 'United Software Workers',
        date: 'August 15, 2019',
        contributorType: 'Trade Unions',
        amount: 500.0,
      },
    ]);
    return { contributions };
  },
};
</script>

<style>
table {
  margin-left: auto;
  margin-right: auto;
}
</style>
