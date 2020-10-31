<template>
  <div class="home">
    <main-layout>
      <section class="introduction">
        <div class="content">
          <h1 class="title has-text-centered">Sask Party Donors</h1>
          <p>
            The Sask Party say they are for Saskatchewan. In a democracy, by
            definition, it is supposed to be the people (i.e. the voters) who
            hold the power. Reality is much different, however, especially in
            the province that has
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
            To really know who holds power over our government, one must follow
            the money. This tool was designed to make it easier to sift through
            the publicly-available information to help us do just that.
          </p>
          <p class="has-text-centered">
            <v-link-button
              url="/list"
              class="is-sask-party"
              icon="search-dollar"
            >
              <span>Show me the money</span>
            </v-link-button>
          </p>
          <h3 class="subtitle">
            Coming Soon
          </h3>
          <div class="content">
            <p>
              This site is a work in progress. Here is a list of some of the
              things you might see in the near future:
              <ul>
                <li>
                  More years of data and from more sources
                </li>
                <li>
                  Data visualization
                </li>
                <li>
                  Corporate donations on map
                </li>
                <li>
                  Ability to export data
                </li>
                <li>
                  Contributions for other parties
                </li>
                <li>
                  Contribution comparisons between parties
                </li>
                <li>
                  Contributions for candidates
                </li>
              </ul>
            </p>
          </div>
          <h3 class="subtitle">
            Contact
          </h3>
          <div class="content">
            <p>
              If you have any questions or suggestions, or would like to make a
              donation, please send an email to
              <a href="mailto:contact@saskpartydonors.ca">contact@saskpartydonors.ca</a>.
            </p>
          </div>
        </div>
      </section>
    </main-layout>
  </div>
</template>

<script>
import MainLayout from '../layouts/Main.vue';
import VLinkButton from '../components/VLinkButton.vue';

export default {
  name: 'Home',
  components: {
    MainLayout,
    VLinkButton,
  },
};
</script>

<style>
.home {
  margin: 2em auto auto auto;
  padding-left: 1em;
  padding-right: 1em;
  max-width: 800px;
}

.introduction p {
  padding-bottom: 1em;
}

.button.is-sask-party {
  border-color: transparent;
  font-weight: bold;
}

.button.is-sask-party:focus,
.button.is-sask-party:hover {
  color: #fcb827;
}

.button.is-sask-party:focus {
  border-color: #fcb827;
}
</style>
