<template>
  <b-table
    :data="contributions"
    default-sort="contributorName"
    default-sort-direction="asc"
    sort-icon="chevron-up"
    icon-pack="fas"
    paginated
    per-page="10"
    :debounce-search="500"
    :loading="isLoading"
    :mobile-cards="false"
    class="contributions-table"
  >
    <b-table-column
      field="contributorName"
      label="Contributor"
      sortable
      searchable
    >
      <template slot="searchable" slot-scope="props">
        <b-input
          v-model="props.filters['contributorName']"
          placeholder="Search..."
          icon="search"
        />
      </template>
      <template v-slot="props">
        {{ props.row.contributorName }}
      </template>
    </b-table-column>
    <b-table-column
      field="contributorType"
      label="Type"
      sortable
      searchable
      centered
      width="200"
    >
      <template slot="searchable" slot-scope="props">
        <b-input
          v-model="props.filters['contributorType']"
          placeholder="Search..."
          icon="search"
        />
        <!-- <b-tooltip
          label="Options are:<br>Corporation<br>Individual<br>TradeUnions<br>Unincorporated"
          :active="typeSearchTooltipActive"
          always
        >
          <b-button
            type="is-text"
            icon-right="info-circle"
            @click="typeSearchTooltipActive = !typeSearchTooltipActive"
          />
        </b-tooltip> -->
      </template>
      <template v-slot="props">
        <b-icon
          :icon="
            $options.filters.contributorTypeIconName(props.row.contributorType)
          "
          size="is-small"
          :title="props.row.contributorType"
        ></b-icon>
      </template>
    </b-table-column>
    <!-- <b-table-column field="recipientName" label="Recipient" sortable searchable>
      <template slot="searchable" slot-scope="props">
        <b-input
          v-model="props.filters['recipientName']"
          placeholder="Search..."
          icon="search"
        />
      </template>
      <template v-slot="props">
        <span
          class="tag"
          :class="$options.filters.recipientClassName(props.row.recipientName)"
          >{{ props.row.recipientName }}</span
        >
      </template>
    </b-table-column> -->
    <b-table-column
      field="location"
      label="Location"
      sortable
      searchable
      centered
      width="120"
    >
      <template slot="searchable" slot-scope="props">
        <b-input
          v-model="props.filters['location']"
          placeholder="Search..."
          icon="search"
        />
      </template>
      <template v-slot="props">
        <span
          class="tag"
          :class="$options.filters.locationClassName(props.row.location)"
          >{{ props.row.location }}</span
        >
      </template>
    </b-table-column>
    <b-table-column
      field="year"
      label="Year"
      sortable
      searchable
      centered
      width="120"
    >
      <template slot="searchable" slot-scope="props">
        <b-input
          v-model="props.filters['year']"
          placeholder="Search..."
          icon="search"
          class="year-filter-input"
        />
      </template>
      <template v-slot="props">
        {{ props.row.year }}
      </template>
    </b-table-column>
    <b-table-column field="amount" label="Amount" sortable numeric width="135">
      <template v-slot="props">
        {{ props.row.amount | currency }}
      </template>
    </b-table-column>
  </b-table>
</template>

<script>
export default {
  name: 'ContributionsTable',
  props: ['contributions', 'isLoading'],
  data() {
    return {
      typeSearchTooltipActive: false,
    };
  },
  filters: {
    contributorTypeIconName: function(value) {
      switch (value) {
        case 'Corporation':
          return 'landmark';
        case 'Individual':
          return 'user';
        case 'TradeUnions':
          return 'toolbox';
        case 'Unincorporated':
          return 'building';
        default:
          return '';
      }
    },
    recipientClassName: function(value) {
      switch (value) {
        case 'Saskatchewan Party':
          return 'is-sask-party';
        default:
          return '';
      }
    },
    locationClassName: function(value) {
      switch (value) {
        case 'BC':
          return 'is-british-columbia';
        case 'AB':
          return 'is-alberta';
        case 'SK':
          return 'is-saskatchewan';
        case 'MB':
          return 'is-manitoba';
        case 'ON':
          return 'is-ontario';
        case 'QC':
          return 'is-quebec';
        case 'NL':
          return 'is-newfoundland';
        case 'US':
          return 'is-united-states';
        default:
          return '';
      }
    },
  },
};
</script>
