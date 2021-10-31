Thanks for trying Pathfinding Grid.  If you have any questions or errors, let me know.

This is a basic pathfinding system meant for relatively small grid-sizes.  Generally I recommend grids of 140 tiles or less (unless using max_tiles calcuation).  Otherwise it can take a long time to calculate the available paths.

Setup:
Choose an example scene, select grid_manager and choose the grid size in the v2_grid field.
Click 'Make Grid' button on grid_manager.  This will delete the previous grid and make a new one.

Try out a sample scene and try out the 4 calculation methods:
On_Hover - Calculates the path when you hover over a tile.
Once_Per_Turn - Calculates all available tiles after character reaches it's destination.
Max_Tiles - Same as Once_Per_Turn except limit the number of tiles (set in max_tiles field in character0).
On_Click - Will not calculate a path until you click a tile (the other 3 methods show the path it will take before you click).

Right click on a tile to move the demo character to that tile.


FAQ:
What is the max grid size I should do?
Unless using 'max_tile' I recommend 140 tiles or less.  The larger it gets, the exponentially more calculations it will take.

I'm Getting an error on the example scene 'pathfinding_big'.
That is likely because the character is starting on a tile that does not exist or is blocked.
Big characters move along 4 tiles at a time, and since characters start at random positions,
it is probably starting at an edge or in a wall.  To have it start at a specific tile, see 'start_game' in grid_manager.cs
and change it to whatever starting position method you want.





Xplory Games
www.XploryGames.com
Christian@xplorygames.com
