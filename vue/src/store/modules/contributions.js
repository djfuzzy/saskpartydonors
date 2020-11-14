import ContributionsService from "../../services/contributionsService.js";

// initial state
const state = () => ({
  all: [],
  isLoading: false
});

// getters
const getters = {
  isContributionsLoading: state => state.isLoading,
  allContributions: state => state.all
};

// actions
const actions = {
  getAllContributions({ commit }) {
    commit("setIsContributionsLoading", true);
    ContributionsService.getContributions(contributions => {
      commit("setContributions", contributions);
      commit("setIsContributionsLoading", false);
    });
  }
};

// mutations
const mutations = {
  setContributions(state, contributions) {
    state.all = contributions;
  },
  setIsContributionsLoading(state, isLoading) {
    state.isLoading = isLoading;
  }
};

export default {
  namespaced: true,
  state,
  getters,
  actions,
  mutations
};
