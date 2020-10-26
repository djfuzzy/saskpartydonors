import HttpClient from './httpClient';

export default {
  async getContributions() {
    let res = await HttpClient.get('api/contributions', { useCache: true });
    return res.data;
  },
  async getContributionById(id) {
    let res = await HttpClient.get(`api/contributions/${id}`, {
      useCache: true,
    });
    return res.data;
  },
};
