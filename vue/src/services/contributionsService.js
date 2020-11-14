import HttpClient from "./httpClient";

export default {
  async getContributions(cb) {
    let res = await HttpClient.get(
      "api/contributions/recipients/B4230209-8DA3-43DB-A374-D3BBD3803AC1",
      { useCache: true }
    );
    return cb(res.data);
  },
  async getContributionById(id, cb) {
    let res = await HttpClient.get(`api/contributions/${id}`, {
      useCache: true
    });
    return cb(res.data);
  }
};
