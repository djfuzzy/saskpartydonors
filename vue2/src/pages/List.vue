<template>
  <main-layout>
    <section class="contributions-list mt-4">
      <div class="container">
        <v-link href="/">Go back to home</v-link>
        <ContributionsTable
          :contributions="contributions"
          :isLoading="isLoading"
        />
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
      isLoading: false,
    };
  },
  async mounted() {
    this.isLoading = true;
    this.contributions = await ContributionsService.getContributions();
    this.isLoading = false;
  },
};
</script>

<style>
table {
  margin-left: auto;
  margin-right: auto;
}
</style>
