import Vue from 'vue';
import Vuex from 'vuex';
import contributions from './modules/contributions';
import createLogger from '../plugins/logger';

Vue.use(Vuex);

const debug = process.env.NODE_ENV !== 'production';

export default new Vuex.Store({
  modules: {
    contributions,
  },
  strict: debug,
  plugins: debug ? [createLogger()] : [],
});
