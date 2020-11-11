<template>
  <main-layout>
    <section class="contributions-list mt-4">
      <div class="container">
        <v-link href="/">Go back to home</v-link>
        <ContributionsTable
          :contributions="contributions"
          :isLoading="
            this.$store.getters['contributions/isConributionsLoading']
          "
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
                Elections Saskatchewan (Donations over $250 from 2016 to 2019,
                originating from Sask)
              </a>
            </li>
            <li>
              <a
                href="https://airtable.com/shrtS71S4Gw0V6QHy/tblm2NgY3pAW3896X/viwSk1LWyYK4kmX5U"
                target="_blank"
                rel="noopener nofollow"
              >
                https://airtable.com/shrtS71S4Gw0V6QHy/tblm2NgY3pAW3896X/viwSk1LWyYK4kmX5U
                (Corporate donations over $250 from 2006 to 2018, including
                out-of-province)
              </a>
            </li>
            <li>
              <a
                href="https://special.nationalpost.com/follow-the-money/database"
                target="_blank"
                rel="noopener nofollow"
              >
                National Post (Donations over $250 from 2009 to 2018,
                originating from Sask)
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
import { mapState } from 'vuex';

export default {
  name: 'List',
  components: {
    MainLayout,
    ContributionsTable,
    VLink,
  },
  computed: mapState({
    contributions: (state) => state.contributions.all,
  }),
  created() {
    this.$store.dispatch('contributions/getAllContributions');
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
