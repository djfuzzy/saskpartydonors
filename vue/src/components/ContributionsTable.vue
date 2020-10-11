<template>
  <table class="table">
    <thead>
      <tr>
        <th>
          Contributor
        </th>
        <th>
          Year
        </th>
        <th>
          Type
        </th>
        <th>
          Amount
        </th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="contribution in contributions" :key="contribution.id">
        <td>
          {{ contribution.contributorName }}
        </td>
        <td>
          {{ contribution.date }}
        </td>
        <td>
          {{ contribution.contributorType }}
        </td>
        <td>
          {{ contribution.amount }}
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script>
export default {
  name: 'ContributionsTable',
  props: {
    contributions: {},
  },
};
</script>
