export default {
  name: "app-resume",
  template: `
<div class="app-resume">
  <button 
    type='button' 
    title='print resume' 
    @click='printMe()'>
      <span class="fa fa-print">
  </button>
  <iframe 
    ref='iframe' 
    src="/resume.html" 
    width='100%' 
    height='100%' 
    title='my resume'>
  </iframe>
</div>`,
  mounted() {
    const iframe = this.$refs.iframe;
    iframe.onload = function () {
      iframe.style.height =
        (iframe.contentWindow.document.body.scrollHeight + 20).toString() +
        "px";

      iframe.style.width =
        (iframe.contentWindow.document.body.scrollWidth + 20).toString() + "px";
    };
  },
  methods: {
    printMe() {
      const iframe = this.$refs.iframe;
      iframe.contentWindow.print();
    },
  },
};
