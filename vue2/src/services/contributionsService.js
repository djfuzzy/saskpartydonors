import axios from 'axios';

const ajax = axios.create({
  baseURL: 'https://localhost:5001',
  timeout: 30000,
});

export default {
  async getContributions() {
    let res = await ajax.get('api/contributions');
    return res.data;
  },
  async getContributionById(id) {
    let res = await ajax.get(`api/contributions/${id}`);
    return res.data;
  },
};
