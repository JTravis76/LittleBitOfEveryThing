# RPG - The Mini Adventures of Jeremiah

This web program is writing by Jeremy Travis.
To use this for your web site you must change the following values
in the index.htm file located in this directory.
================

Open index.htm in your text editor, such as notepad.
Line number: 214 - should show: fileindex = 4;

The number 4 is the sub-directory number of where the images are stored.
Change this number to match the the directory level on the tree.

EX:
absolute path is "C:\Users\Public\Documents\Visual Studio 2013\Projects\RPG"
or "http://localhost:8080/

To test if working, run the game and then walk up and face the sign by the castle and press the action key (5)
on the keypad. If you see a message said "Kingdom of Light" you know it's working.

=================
Line number: 573 -should show: location.href = 'http://localhost/'
Change this address to point to your home page.

Line number: 695 -should read: newWindow.document.location.href ='http://localhost/RPG/how_to.htm'
Change the address to point to the how_to.htm file.

## Map Setup and Rules

Object|IconActionType|

Object - the name of any .gif file that is a object, such as a tree, building, etc. If left blank, grass is default.

IconActionType - stores the information about the object. Its tells if the object is a solid, non-solid, or a action. This is a hyphen(-) delimited arguments value. Ex: 0-0-0-0

- 0 = object type is a non-solid **(able to walk-through)**.
- 1 = object type is a solid **(unable to walk-through)**.
- 2 = object type is a jump action.
- 3 = object type can use an action.

### Jump Action Settings

Ex:(2-0-1-5)  
2 -is a jump action.  
0 -is the map number to jump to.  
1 -is the new Y position of player.  
5 -is the new X position of player.

### Action Settings

Ex: (3-0-Welcome to the Forest Maze)

- 3 = is a solid action (unable to walk-through).
- 0 = action type.
- 0 = is a Message action. Uses the 3th arguments to display the message.
- 1 = is a Axe action, use to cut trees. Uses the 3th arguments to change the object to a new object (name of GIF file).

If the new object is a black_hole then set the object to a jump action using the 4th arguments is map number,
5th arguments is the Y position of player when he lands,
6th arguments is the X position of player when he lands.
2 -is a Open Treasure action. Uses the 3th arguments to change the object to a new object (name of GIF file).
Uses the 4th arguments to display a message of what is found in the treasure chest. ex:(3-2-treasure_open-You found a 5 Golds.)
3 -is a Flute action. Uses the 3th arguments to change the object to a new object (name of GIF file).
4 - Set the object type to a non-solid and a action.

This Setting takes several arguments and they use a hyphen(-) between each arguments.
Ex: (4-0-2)
4 -is a non-solid action (able to walk-through).
0 -is a Torch action.
2 -is the map number to jump to.
1 -is a Fight action. Jump to fight scene and defeat off enemies.
2 -is the map number to jump to.
