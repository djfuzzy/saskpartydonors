<template>
  <div id="main">
    <slot></slot>
    <SiteFooter />
  </div>
</template>

<script>
import SiteFooter from '../components/SiteFooter';

export default {
  components: {
    SiteFooter,
  },
};
</script>

<style>
.is-sask-party {
  background-color: #005941;
  color: #fcb827;
}
.tag.is-british-columbia {
  background-color: #0045bc;
  color: #ffce00;
}
.tag.is-alberta {
  background-color: #ff0000;
  color: #ffffff;
}
.tag.is-saskatchewan {
  background-color: #016a1b;
  color: #ffffff;
}
.tag.is-manitoba {
  background-color: #00183f;
  color: #ffffff;
}
.tag.is-ontario {
  background-color: #be0000;
  color: #f3cd00;
}
.tag.is-quebec {
  background-color: #003ba6;
  color: #ffffff;
}
.tag.is-newfoundland {
  background-color: #003565;
  color: #ffa600;
}
.tag.is-united-states {
  background-color: #0000ff;
  color: #ffffff;
}
</style>
