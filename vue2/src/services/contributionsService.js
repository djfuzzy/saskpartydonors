import axios from 'axios';

const httpClient = axios.create({
  baseURL: 'https://localhost:5001',
  timeout: 30000,
  headers: {
    'Content-Type': 'application/json',
  },
});

export default {
  async getContributions() {
    let res = await httpClient.get('api/contributions');
    return res.data;
  },
  async getContributionById(id) {
    let res = await httpClient.get(`api/contributions/${id}`);
    return res.data;
  },
};
