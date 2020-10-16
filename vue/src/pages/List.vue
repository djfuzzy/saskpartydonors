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
import ContributionsTable from '../components/ContributionsTable.vue';
import VLink from '../components/VLink';
import MainLayout from '../layouts/Main.vue';
import ContributionsService from '../services/contributionsService';

export default {
  name: 'List',
  components: {
    MainLayout,
    ContributionsTable,
    VLink,
  },
  data() {
    return {
      contributions: [],
    };
  },
  async created() {
    this.contributions = await ContributionsService.getContributions();
  },
};
</script>

<style>
table {
  margin-left: auto;
  margin-right: auto;
}
</style>
