<template>
  <b-table
    :data="contributions"
    default-sort="contributorName"
    default-sort-direction="asc"
    sortIcon="chevron-up"
    :paginated="true"
    per-page="10"
    :debounce-search="500"
    :loading="isLoading"
    class="contributions-table"
  >
    <b-table-column field="contributorName" label="Name" sortable searchable>
      <template slot="searchable" slot-scope="props">
        <b-input
          v-model="props.filters['contributorName']"
          placeholder="Search..."
          icon="search"
        />
      </template>
      <template v-slot:header="{ column }">
        {{ column.label }}
      </template>
      <template v-slot="props">
        {{ props.row.contributorName }}
      </template>
    </b-table-column>
    <b-table-column field="year" label="Year" sortable searchable width="120">
      <template slot="searchable" slot-scope="props">
        <b-input
          v-model="props.filters['year']"
          placeholder="Search..."
          icon="search"
          class="year-filter-input"
        />
      </template>
      <template v-slot:header="{ column }">
        {{ column.label }}
      </template>
      <template v-slot="props">
        {{ props.row.year }}
      </template>
    </b-table-column>
    <b-table-column field="contributorType" label="Type" sortable searchable>
      <template slot="searchable" slot-scope="props">
        <b-input
          v-model="props.filters['contributorType']"
          placeholder="Search..."
          icon="search"
        />
      </template>
      <template v-slot:header="{ column }">
        {{ column.label }}
      </template>
      <template v-slot="props">
        {{ props.row.contributorType }}
      </template>
    </b-table-column>
    <b-table-column field="amount" label="Amount" sortable numeric width="135">
      <template v-slot:header="{ column }">
        {{ column.label }}
      </template>
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
};
</script>
