<template>
  <main-layout>
    <section class="contributions-list mt-4">
      <div class="container">
        <v-link href="/">Go back to home</v-link>
        <ContributionsTable
          :contributions="contributions"
          :isLoading="isLoading"
        />
        <h3>Sources</h3>
        <div class="content is-small">
          <ul>
            <li>
              <a
                href="https://www.elections.sk.ca/reports-data/candidate-political-party-finances/fiscal-period-returns/"
                target="_blank"
                rel="noopener nofollow"
              >
                https://www.elections.sk.ca/reports-data/candidate-political-party-finances/fiscal-period-returns/
              </a>
            </li>
            <li>
              <a
                href="https://airtable.com/shrtS71S4Gw0V6QHy/tblm2NgY3pAW3896X/viwSk1LWyYK4kmX5U"
                target="_blank"
                rel="noopener nofollow"
              >
                https://airtable.com/shrtS71S4Gw0V6QHy/tblm2NgY3pAW3896X/viwSk1LWyYK4kmX5U
              </a>
            </li>
          </ul>
        </div>
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

<style scoped>
.contributions-list {
  max-width: 1000px;
  margin-left: auto;
  margin-right: auto;
}
</style>
