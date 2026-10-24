<template>
  <div class="">
    <div class="row">
      <div class="three columns">
        <h5>Table of Contents</h5>
        <ul class="menu">
          <template v-for="item in menu" :key="item.title">
            <li>
              <a href="#" @click.prevent="fetchArticle(item.link)">{{
                item.title
              }}</a>
            </li>
          </template>
        </ul>
      </div>
      <div class="nine columns">
        <div v-html="article"></div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import markdownit from "markdown-it";
import highlightjs from "markdown-it-highlightjs";

export default defineComponent({
  name: "AppMarkdownHelp",
  data() {
    return {
      article:
        "<h3>Welcome to the Help</h3> <p>To begin click an article from the table of contents.</p>",
      menu: [
        { title: "Equipment Review", link: "equipment-review" },
        //{ title: "Random Test", link: "random" },
      ].sort((a, b) => (a.title > b.title ? 1 : -1)),
    };
  },
  methods: {
    fetchArticle(link: string) {
      fetch(`./help/${link}.md`)
        .then(async (resp) => {
          if (resp.ok) {
            const txt = await resp.text();
            this.article = markdownit().use(highlightjs).render(txt);
            return;
          }
          throw new Error();
        })
        .catch(() => {
          this.article =
            "<div><b>OOPs!</b> the article of link is broken.</div>";
        });
    },
  },
});
</script>

<style lang="scss" scoped>
.row {
  display: flex;
  //flex-wrap: wrap;

  .columns {
    width: 100%;
    margin: 4px;
    padding: 4px 8px;
    float: left;
    box-sizing: border-box;
  }

  .three {
    width: 22%;
  }

  .nine {
    width: 78%;
  }
}

ul.menu {
  padding-inline-start: 0px;

  li {
    list-style: none;

    a {
      color: var(--color--black);
      text-decoration: none;

      &:hover {
        color: var(--color--primary);
      }
    }
  }
}
</style>

<style lang="scss">
blockquote {
  border: 1px solid var(--color--info2);
  border-left: 4px solid var(--color--info2);
  background: var(--color--info1);
  padding-left: 4px;
}
</style>
