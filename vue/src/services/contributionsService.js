import HttpClient from './httpClient';

export default {
  async getContributions(cb) {
    let res = await HttpClient.get('api/contributions', { useCache: true });
    return cb(res.data);
  },
  async getContributionById(id, cb) {
    let res = await HttpClient.get(`api/contributions/${id}`, {
      useCache: true,
    });
    return cb(res.data);
  },
};
