<template>
  <main-layout>
    <section class="introduction">
      <div class="content">
        <h1 class="title has-text-centered">Sask Party Donors</h1>
        <p>
          The Sask Party says they are for Saskatchewan. In a democracy, its
          supposed to be the people (i.e. the voters) who hold the power.
          Reality is much different, however, especially in the province that
          has
          <a
            href="https://www.progressalberta.ca/worst_campaign_finance_rules_in_canada"
            target="_blank"
            rel="noopener norefer"
            >the loosest campaign financing laws in Canada</a
          >, including having no limits on donations, allowing donations from
          outside the province, and allowing donations from corporations and
          unions.
        </p>
        <p>
          To really know who our government answers to, one must follow the
          money. This tool was designed to make it easier to sift through the
          publicly-available information to help us do just that.
        </p>
        <p class="has-text-centered">
          <v-link
            href="/list"
            class="button is-sask-party-theme has-text-centered list-button"
          >
            <span>Follow the money</span>
          </v-link>
        </p>
        <h2 class="subtitle">
          Coming Soon
        </h2>
        <div class="content">
          <ul>
            <li>
              Add more years of data and from more sources
            </li>
            <li>
              Show corporate donations on map
            </li>
            <li>
              Show contributions for other parties
            </li>
            <li>
              Compare contributions between parties
            </li>
            <li>
              Show contributions for candidates
            </li>
          </ul>
        </div>
      </div>
    </section>
  </main-layout>
</template>

<script>
import MainLayout from '../layouts/Main.vue';
import VLink from '../components/VLink.vue';

export default {
  name: 'Home',
  components: {
    MainLayout,
    VLink,
  },
};
</script>

<style>
.introduction p {
  padding-bottom: 1em;
}

.is-sask-party-theme {
  background-color: #005941;
  border-color: transparent;
  color: #fcb827 !important;
  font-weight: bold;
}
</style>
