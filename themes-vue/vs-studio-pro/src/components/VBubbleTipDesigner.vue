<template>
  <div class="skeleton-ui">
    <div class="row">
      <div class="tweleve columns">
        <button
          type="button"
          class="button button-primary"
          @click="editMode = !editMode"
        >
          {{ editMode ? "Exit" : "" }} Edit Mode
        </button>
      </div>
    </div>
    <div v-if="editMode">
      <div class="row">
        <div class="columns">
          <template v-for="(bubble, idx) in bubbles" :key="idx">
            <input type="radio" name="bubble-group" :value="idx" />
            {{ bubble.label }}
          </template>
        </div>
      </div>
      <div class="row">
        <div class="six columns">
          <div>
            <span>Label Name</span>
            <input
              type="text"
              v-model="bubble.label"
              :disabled="bubble.id === -1"
              placeholder="label name"
            />
            <span>Left Position</span>
            <input
              type="number"
              v-model="bubble.left"
              min="0"
              max="1024"
              step="1"
              size="10"
              :disabled="bubble.id === -1"
              placeholder="left position"
            />
            <span>Top Position</span>
            <input
              type="number"
              v-model="bubble.top"
              min="0"
              max="768"
              step="1"
              size="10"
              :disabled="bubble.id === -1"
              placeholder="top position"
            />

            <button type="button" class="button">Add Bubble</button>
            <button type="button" class="button">Remove Bubble</button>
            <button type="button" class="button button-primary">Save</button>
          </div>
        </div>
        <div class="six columns">
          <div>
            <label
              >Show Tip
              <input
                type="checkbox"
                v-model="bubble.showTip"
                :disabled="bubble.id === -1"
            /></label>
          </div>
          <div>
            <template v-for="(tip, idx) in bubble.tips" :key="idx">
              <label
                >{{ `tip-${idx + 1}` }}
                <input
                  :id="`tip-${tip.id}`"
                  type="radio"
                  name="tip-group"
                  :value="idx"
                />
              </label>
            </template>
          </div>
          <div>
            <textarea
              rows="3"
              cols="50"
              v-model="bubble.tips[0].text"
              placeholder="balloon HTML text"
            ></textarea>
          </div>
          <div>
            <span>Left</span>
            <input
              v-model.number="bubble.tips[0].left"
              type="number"
              min="0"
              max="1024"
              step="1"
              size="10"
              :disabled="bubble.id === -1"
            />
          </div>
          <div>
            <span>Top</span>
            <input
              v-model.number="bubble.tips[0].top"
              type="number"
              min="0"
              max="768"
              step="1"
              size="10"
              :disabled="bubble.id === -1"
            />
          </div>
          <div>
            <span>Width</span>
            <input
              v-model.number="bubble.tips[0].width"
              type="number"
              min="0"
              max="1024"
              step="1"
              size="10"
              :disabled="bubble.id === -1"
            />
          </div>
          <button type="button" class="button">Add Tip</button>
          <button type="button" class="button">Remove Tip</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "VBubbleTipDesigner",
  data() {
    return {
      editMode: false,
      bubble: {
        id: -1,
        label: "",
        left: 0,
        top: 0,
        showTip: false,
        tips: [{ id: -1, text: "", left: 0, top: 0, width: 0 }],
      },
      bubbles: [{ label: "1" }],
    };
  },
});
</script>

<style lang="scss"></style>
