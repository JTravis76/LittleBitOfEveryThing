export default {
  name: "app-floating-window",
  template: `
<div>
  <p style="margin-left:20px;">
    This page demostrates a floating assistance window in the lower right corner. Upon clicking will open a popup
    container where you can display about anything
  </p>
  <div id="hero-iframe-container" class="position-overridden">
    <div class="hero-launcher-container active hwp-smart-container" style="transform: translate(-5px, -40px);">
      <div class="hero-launcher-avatar-container active standard">
        <section data-qa="launcher-icon-iframe" class="hero-launcher-avatar">
          <div class="frame-root">
            <div class="frame-content">
              <div class="launcher standard">
                <button id="btn1" data-qa="launcher-icon-button" class="launcher-button"
                  type="button" title="need assistance" style="background-color: rgb(0, 0, 0);">
                    <span>
                      <svg width="40" height="40" fill="none"
                        class="launcher-button-cross" style="stroke: white;">
                        <path opacity="0.01" d="M6 6h28v28H6z"></path>
                        <path d="M25 25l-5-5 5-5M15 25l5-5-5-5"></path>
                      </svg>
                    <div class="launcher-button-content">
                      <div class="launcher-button-icon">
                        <svg width="40" height="40" fill="none" class=""
                          style="fill: white;">
                          <path opacity="0.01" d="M6 6h28v28H6z"></path>
                          <path clip-rule="evenodd"
                            d="M25.243 21.28c-.783 0-1.418-.655-1.418-1.463s.635-1.464 1.418-1.464c.783 0 1.418.655 1.418 1.464 0 .808-.635 1.464-1.418 1.464zm-5.02 0c-.783 0-1.418-.655-1.418-1.463s.635-1.464 1.418-1.464c.783 0 1.417.655 1.417 1.464 0 .808-.634 1.464-1.417 1.464zm-5.02 0c-.784 0-1.418-.655-1.418-1.463s.634-1.464 1.418-1.464c.782 0 1.417.655 1.417 1.464 0 .808-.635 1.464-1.418 1.464zm12.015-9.126h-13.99c-2.305 0-4.173 1.885-4.173 4.21V28.99c0 .662.765 1.025 1.27.602l1.997-1.669a3.972 3.972 0 012.549-.927H27.218c2.305 0 4.172-1.885 4.172-4.21v-6.423c0-2.325-1.867-4.21-4.172-4.21z">
                          </path>
                        </svg>
                      </div>
                    </div>
                  </span>
                </button>
              </div>
            </div>
          </div>
        </section>
      </div>
    </div>
    <div class="hero-plugin-container inactive" style="background-color: white;">
      <section class="hero-plugin active">
        <div style="height:80px; background-color: black;">
          <button type="button" style="color: white;margin: 4px;" title="close window">&times;</button>
        </div>
      </section>
    </div>
  </div>
</div>`,
  mounted() {
    this.$nextTick(() => {
      const btns = document.querySelectorAll("button");
      btns.forEach((b) => {
        b.addEventListener("click", (e) => {
          // e.stopPropagation();
          var con = document.querySelector(".hero-plugin-container");
          con.classList.toggle("active");
          con.classList.toggle("inactive");
        });
      });
    });
  },
};
