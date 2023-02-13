using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindTesting : MonoBehaviour
{
    [SerializeField]
    private GameObject debugTile;
    private static GameObject _debugTile;

    private static List<GameObject> debugTiles = new List<GameObject>();

    private void Awake()
    {
        _debugTile = debugTile;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                AStar pathfind = new AStar();
                List<Tile> path = pathfind.FindPath(TileMap.Instance.tileMap[Vector2.zero], TileMap.Instance.GetTileFromTouchPosition());

                if (path != null)
                {
                    Debug.Log("drawing lines");
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Instantiate(debugTile, path[i].worldPos,Quaternion.Euler(90,0,0));
                    }                    
                }
            }
        }*/
    }

    public static void ShowDebugTiles(List<Vector3> positions)
    {
        if(debugTiles.Count > 0)
        {
            foreach(GameObject tile in debugTiles)
            {
                Destroy(tile);
            }
            debugTiles.Clear();
        }

        for (int i = 0; i < positions.Count - 1; i++)
        {
            debugTiles.Add(Instantiate(_debugTile, positions[i], Quaternion.Euler(90, 0, 0)));
        }
    }
}
