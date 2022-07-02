using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _Scripts.GridBasedMovement.Grid_Generation
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GridGeneration)), CanEditMultipleObjects]
    internal class GridGenerationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            var gridGeneration = (GridGeneration)target;
            if (GUILayout.Button("Generate Grid"))
                gridGeneration.GenerateGrid();
            if (GUILayout.Button("Player Grid"))
                gridGeneration.PlayerGridGeneration();
            if (GUILayout.Button("Destroy Grid"))
                gridGeneration.DestroyGrid();

            DrawDefaultInspector();
        }
    }
#endif
    
    public class GridGeneration : MonoBehaviour
    {
        [SerializeField] private GameObject gridPrefab;
        [SerializeField] private Vector2Int gridSize;

        [SerializeField] private List<GameObject> child;

        public void GenerateGrid()
        {
            for (var x = 0; x < gridSize.x; x++)
            {
                for (var y = 0; y < gridSize.y; y++)
                {
                    var obj = Instantiate(gridPrefab,this.transform);
                    obj.name = "Tile_" + x + "_" + y;
                    obj.transform.position = new Vector3(x, 0, y);
                }
            }
        }
    

        public void PlayerGridGeneration()
        {
            DestroyGrid();
            
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y - x; y++)
                {
                    var obj = Instantiate(gridPrefab,this.transform);
                    obj.name = "Tile_" + x + "_" + y;
                    obj.transform.localPosition = new Vector3(x, 0.005f, y);
                    obj.GetComponent<GridTile>().location = new Vector2(x, y);

                }

                if (x > 0)
                {
                    for (int y = -1; y > (gridSize.y - x) * -1; y--)
                    {
                        var obj = Instantiate(gridPrefab,this.transform);
                        obj.name = "Tile_" + x + "_" + y;
                        obj.transform.localPosition = new Vector3(x, 0.005f, y);
                        obj.GetComponent<GridTile>().location = new Vector2(x, y);

                    }
               
                }

            }
        
            for (var x = 0; x > gridSize.x * -1; x--)
            {
                for (var y = 0; y > (gridSize.y + x) * -1; y--)
                {
                    var obj = Instantiate(gridPrefab,this.transform);
                    obj.name = "Tile_" + x + "_" + y;
                    obj.transform.localPosition = new Vector3(x, 0.005f, y);
                    obj.GetComponent<GridTile>().location = new Vector2(x, y);
                
                    if (x == 0 && y == 0)
                    {
                        DestroyImmediate(obj.gameObject);
                    }
                }

                if (x < 0)
                {
                    for (var y = 1; y < (gridSize.y + x); y++)
                    {
                        var obj = Instantiate(gridPrefab,this.transform);
                        obj.name = "Tile_" + x + "_" + y;
                        obj.transform.localPosition = new Vector3(x, 0.005f, y);
                        obj.GetComponent<GridTile>().location = new Vector2(x, y);
                    }
                }
            }

        }

        public void DestroyGrid()
        {
            var tempArray = new GameObject[this.transform.childCount];

            for(int i = 0; i < tempArray.Length; i++)
            {
                tempArray[i] = this.transform.GetChild(i).gameObject;
            }

            foreach(var child in tempArray)
            {
                DestroyImmediate(child);
            }
        }
    }
}