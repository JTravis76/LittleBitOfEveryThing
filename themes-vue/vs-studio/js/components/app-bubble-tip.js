export default {
  name: "app-bubble-tip",
  template: `
<div class="bubble-tip--wrapper">
  <div><button type="button" class="nav-button">&times;</button></div>
  <div class="bubble-tip--img-container">
    <div class="bubble-tip--overlay">
      <button type="button" class="btn-1">
        <span>1</span>
      </button>
      <button type="button" class="btn-2">2</button>
      <button type="button" class="btn-3">3</button>
      <button type="button" class="btn-4">4</button>
      <div class="bubble-tip animate__animated animate__slow">
        Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's
        standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to
        make a type specimen book.
      </div>
    </div>
    <img src="./img/stockphoto1.jpg" alt="image" />
  </div>
  <div><button type="button" class="nav-button">&times;</button></div>
</div>`,
};
