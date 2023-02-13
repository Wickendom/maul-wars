using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public Vector2 coords;
    public Vector3 worldPos;

    public GameObject debugTile;

    public int gCost, hCost, fCost;//gCost = walk cost from start point, hCost=walk cost to end point without walls, fCost is g+h
    public Tile cameFromTile;
    public bool pathable = true;
    public bool buildable = true;

    public Tile(Vector2 coords,float tileSize)
    {
        this.coords = coords;
        worldPos = new Vector3(coords.x * tileSize,0, coords.y * tileSize);
        gCost = int.MaxValue;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public Tile GetNeighbor(int dir)
    {
        Tile tile = null;
        switch (dir)
        {
            case 0:
                {
                    tile = null;
                    TDRoyaleSingleton.Instance.tileMap.tileMap.TryGetValue(coords + new Vector2(-1,0), out tile);//left
                    break;
                }
            case 1:
                {
                    tile = null;
                    TDRoyaleSingleton.Instance.tileMap.tileMap.TryGetValue(coords + new Vector2(1, 0), out tile);//right
                    break;
                }
            case 2:
                {
                    tile = null;
                    TDRoyaleSingleton.Instance.tileMap.tileMap.TryGetValue(coords + new Vector2(0, 1), out tile);//up
                    break;
                }
            case 3:
                {
                    tile = null;
                    TDRoyaleSingleton.Instance.tileMap.tileMap.TryGetValue(coords + new Vector2(0, -1), out tile);//down
                    break;
                }
        }

        return tile;
    }
}
