using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PhotonView))]
public class TileMap : MonoBehaviour
{
    public PhotonView phView;

    [SerializeField]
    private int tileMapSizeX = 10, tileMapSizeY = 10;

    [SerializeField]
    private float tileSize = 1f;

    public Dictionary<Vector2, Tile> tileMap;

    [SerializeField]
    private LayerMask groundLayerMask;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        for (int x = 0; x < tileMapSizeX; x++)
        {
            Gizmos.DrawLine(new Vector3((x* tileSize)+(tileSize*0.5f),0,0),new Vector3((x * tileSize) + (tileSize * 0.5f), 0, (tileMapSizeY * tileSize) + (tileSize * 0.5f)));
        }

        for (int y = 0; y < tileMapSizeY; y++)
        {
            Gizmos.DrawLine(new Vector3(0, 0, (y * tileSize) + (tileSize * 0.5f)), new Vector3((tileMapSizeX * tileSize) + (tileSize * 0.5f), 0, (y * tileSize) + (tileSize * 0.5f)));
        }
    }

    public void resetPathingvariables()
    {
        foreach (KeyValuePair<Vector2, Tile> kvp in tileMap)
        {
            kvp.Value.gCost = int.MaxValue;
            kvp.Value.cameFromTile = null;
        }
    }

    public void Initialise()
    {
        phView = GetComponent<PhotonView>();
        CreateGrid();
    }

    private void CreateGrid()
    {
        tileMap = new Dictionary<Vector2, Tile>();

        for (int x = 0; x < tileMapSizeX; x++)
        {
            for (int y = 0; y < tileMapSizeY; y++)
            {
                tileMap.Add(new Vector2(x, y), new Tile(new Vector2(x, y), tileSize));
                //Debug.Log("Tile Created at pos X:" + x + " Y:" + y);
            }
        }
    }

    public Tile GetTileFromTouchPosition()
    {
        Touch touch = Input.GetTouch(0);

        Tile tile = tileMap[Vector2.zero];

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50, groundLayerMask))
        {
            //Debug.Log("Hit Ground");

            Vector3 pos = hit.point;

            if (!tileMap.TryGetValue(RoundToNearstTileCoords(new Vector2(pos.x, pos.z)), out tile))
            {
                tile = tileMap[Vector2.zero];
            }
            else
            {
                //Debug.Log("Found Tile at position X:" + (int)pos.x + " Y:" + (int)pos.z);
            }
        }

        return tile;
    }

    public Tile GetTileFromEnemyPosition(Vector3 position)
    {
        Tile tile = tileMap[Vector2.zero];

        //Debug.DrawRay(ray.origin, ray.direction * 50, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(position, Vector3.down * 5, out hit, 10f, groundLayerMask))
        {
            //Debug.Log("Hit Ground");

            Vector3 pos = hit.point;

            if (!tileMap.TryGetValue(RoundToNearstTileCoords(new Vector2(pos.x,pos.z)), out tile))
            {
                tile = tileMap[Vector2.zero];
            }
            else
            {
                //Debug.Log("Found Tile at position X:" + (int)pos.x + " Y:" + (int)pos.z);
            }
        }

        return tile;
    }

    public Tile GetTileFromWorldPosition(Vector3 pos)
    {
        Tile tile = tileMap[Vector2.zero];

        if (tileMap.TryGetValue(RoundToNearstTileCoords(new Vector2(pos.x, pos.z)), out tile))
        {
            return tile;
        }


        return tile;
    }

    private Vector2 RoundToNearstTileCoords(Vector2 coords)
    {
        coords.x = Mathf.Floor(coords.x/tileSize);
        coords.x = Mathf.RoundToInt(coords.x);
        coords.y = Mathf.Floor(coords.y / tileSize);
        coords.y = Mathf.RoundToInt(coords.y);

        return coords;
    }

    [PunRPC]
    public void UpdateTileVariables(int x, int y, bool pathable, bool buildable)
    {
        tileMap[new Vector2(x, y)].pathable = pathable;
        tileMap[new Vector2(x, y)].buildable = buildable;
    }
}
