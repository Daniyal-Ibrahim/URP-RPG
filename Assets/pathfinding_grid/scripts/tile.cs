using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


#if UNITY_EDITOR
[CustomEditor(typeof(tile)), CanEditMultipleObjects]
class tile_editor : Editor
{
    public override void OnInspectorGUI()
    {
        tile tile_s = (tile)target;
        if (GUILayout.Button("Disable Walls"))
            tile_s.disable_walls();
        if (GUILayout.Button("Top"))
            tile_s.add_wall(0);
        if (GUILayout.Button("Right"))
            tile_s.add_wall(1);
        if (GUILayout.Button("Bottom"))
            tile_s.add_wall(2);
        if (GUILayout.Button("Left"))
            tile_s.add_wall(3);

        DrawDefaultInspector();
    }
}
#endif


public class tile : MonoBehaviour
{
    public grid_manager gm_s;
    public Vector2 v2xy;
    public walls walls_s;
    public Image im;
    public MeshRenderer mr;
    public Button btn;
    public List<character> db_chars;
    public List<sub_tile> db_neighbors;
    public List<tile> db_path_lowest;


    public void add_wall(int tnum)
    {
        db_neighbors[tnum].blocked = true;
        walls_s.db_go_walls[tnum].SetActive(true);
        if (db_neighbors[tnum].tile_s != null)
        {
            var treverse = 0;
            if (tnum == 0)
                treverse = 2;
            else
            if (tnum == 1)
                treverse = 3;
            else
            if (tnum == 2)
                treverse = 0;
            else
            if (tnum == 3)
                treverse = 1;

            db_neighbors[tnum].tile_s.db_neighbors[treverse].blocked = true;
            db_neighbors[tnum].tile_s.walls_s.db_go_walls[treverse].SetActive(true);
        }
    }


    public void disable_walls()
    {
        for (int x = 0; x < db_neighbors.Count; x++)
        {
            db_neighbors[x].blocked = false;
            walls_s.db_go_walls[x].SetActive(false);
            if (db_neighbors[x].tile_s != null)
            {
                var treverse = 0;
                if (x == 0)
                    treverse = 2;
                else
                if (x == 1)
                    treverse = 3;
                else
                if (x == 2)
                    treverse = 0;
                else
                if (x == 3)
                    treverse = 1;

                db_neighbors[x].tile_s.db_neighbors[treverse].blocked = false;
                db_neighbors[x].tile_s.walls_s.db_go_walls[treverse].SetActive(false);
            }
        }
    }
}
