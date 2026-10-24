export default [
  {
    id: 1,
    name: "Portifolio",
    type: "project",
    parent: 0,
    expand: false,
  },
  {
    id: 2,
    name: "Fun Stuff",
    type: "directory",
    parent: 1,
    expand: false,
  },
  {
    id: 3,
    name: "Work In Progress",
    type: "directory",
    parent: 1,
    expand: false,
  },
  { id: 4, name: "AboutMe.html", type: "file", parent: 1, expand: false },
  { id: 5, name: "Resume.html", type: "file", parent: 1, expand: false },
  {
    id: 6,
    name: "BinaryClock.html",
    type: "file",
    parent: 2,
    expand: false,
  },
  {
    id: 7,
    name: "AudioPlaylist.html",
    type: "file",
    parent: 3,
    expand: false,
  },
  // {
  //   id: 8,
  //   name: "Bible.html",
  //   type: "file",
  //   parent: 3,
  //   expand: false,
  // },
  // {
  //   id: 9,
  //   name: "BubbleTip.html",
  //   type: "file",
  //   parent: 3,
  //   expand: false,
  // },
  {
    id: 10,
    name: "Gameboard.html",
    type: "file",
    parent: 3,
    expand: false,
  },
  {
    id: 11,
    name: "Metronome.html",
    type: "file",
    parent: 3,
    expand: false,
  },
  {
    id: 12,
    name: "FloatingWindow.html",
    type: "file",
    parent: 2,
    expand: false,
  },
  {
    id: 13,
    name: "ParseCsv.html",
    type: "file",
    parent: 2,
    expand: false,
  },
  {
    id: 14,
    name: "PinterestGrid.html",
    type: "file",
    parent: 2,
    expand: false,
  },
  {
    id: 15,
    name: "ProgressBar.html",
    type: "file",
    parent: 2,
    expand: false,
  },
  {
    id: 16,
    name: "DragDrop.html",
    type: "file",
    parent: 3,
    expand: false,
  },
  {
    id: 17,
    name: "FishTank.html",
    type: "file",
    parent: 3,
    expand: false,
  },
  {
    id: 18,
    name: "ComponentBook.html",
    type: "file",
    parent: 1,
    expand: false,
  },
].sort((a, b) => {
  // parent
  if (a.parent > b.parent) return 1;
  if (a.parent < b.parent) return -1;

  // type
  if (a.type > b.type) return 1;
  if (a.type < b.type) return -1;

  // name
  if (a.name > b.name) return 1;
  if (a.name < b.name) return -1;

  return 0;
});
