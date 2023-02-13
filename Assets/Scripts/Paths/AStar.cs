using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AStar
{
    private Tile startTile, endTile;
    private List<Tile> openList;//path points that are queued up to be searched
    private List<Tile> closedList;//all neighbor path points have been searched

    public List<Tile> FindPath(Tile startPoint,Tile endPoint)
    {
        //Debug.Log("finding Path");
        startTile = startPoint;
        endTile = endPoint;

        startTile.gCost = 0;
        startTile.hCost = CalculateDistance(startPoint, endPoint);
        startTile.CalculateFCost();

        endTile.gCost = int.MaxValue;
        endTile.hCost = 0;
        endTile.CalculateFCost();

        //Debug.Log(startTile.coords + " " + endTile.coords);

        openList = new List<Tile> { startPoint };
        closedList = new List<Tile>();

        while(openList.Count > 0)
        {
            Tile currentTile = GetLowestFCostPathNode(openList);

            if(currentTile == endTile)
            {
                return CalculatePath();
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            foreach(Tile neighborNode in GetPathNodeNeighbors(currentTile))
            {
                //Debug.Log("Neighbor found");
                if (closedList.Contains(neighborNode)) continue;

                int tentativeGCost = currentTile.gCost + CalculateDistance(currentTile, neighborNode);
                //Debug.Log("tentative cost:" + tentativeGCost + "from " + currentNode.coords + " to " + neighborNode.coords);
                //Debug.Log("Neighbor gCost = " + neighborNode.gCost);

                if(tentativeGCost < neighborNode.gCost)
                {
                    //Debug.Log("neighbor node gcost is lower");
                    neighborNode.cameFromTile = currentTile;
                    neighborNode.gCost = tentativeGCost;
                    neighborNode.hCost = CalculateDistance(neighborNode,endPoint);
                    neighborNode.CalculateFCost();

                    if(!openList.Contains(neighborNode))
                    {
                        if(neighborNode.pathable)openList.Add(neighborNode);
                    }
                }
            }
        }

        //out of open nodes in open list. Could not complete path to end point

        //Debug.Log("Pathing unable to reach endpoint");
        TDRoyaleSingleton.Instance.tileMap.resetPathingvariables();
        return null;
    }

    private List<Tile> GetPathNodeNeighbors(Tile node)
    {
        List<Tile> neighborList = new List<Tile>();

        Tile tile = null;

        for (int i = 0; i < 4; i++)
        {
            if(node.GetNeighbor(i) != null)
            {
                if (TDRoyaleSingleton.Instance.tileMap.tileMap.TryGetValue(node.GetNeighbor(i).coords, out tile))
                {
                    neighborList.Add(tile);
                }
            }
        }

        return neighborList;
    }

    private List<Tile> CalculatePath()
    {
        List<Tile> path = new List<Tile>();
        path.Add(endTile);
        Tile currentNode = endTile;

        while(currentNode.cameFromTile != null)
        {
            //Debug.Log("Added thing to path");
            path.Add(currentNode.cameFromTile);
            currentNode = currentNode.cameFromTile;
        }

        path.Reverse();
        TDRoyaleSingleton.Instance.tileMap.resetPathingvariables();

        return path;
    }

    public List<Vector3> GetPathPositions(Tile startTile, Tile endTile)
    {
        List<Vector3> positions = new List<Vector3>();

        List<Tile> path = FindPath(startTile,endTile);

        for (int i = 0; i < path.Count; i++)
        {
            positions.Add(path[i].worldPos);
        }

        return positions;
    }

    private int CalculateDistance(Tile a, Tile b)
    {
        int xDistance = (int)(a.coords.x - b.coords.x);
        int yDistance = (int)(a.coords.y - b.coords.y);

        int distance = Mathf.Abs(xDistance + yDistance);

        return distance;
    }

    private Tile GetLowestFCostPathNode(List<Tile> nodes)
    {
        Tile lowestNode = nodes[0];
        for (int i = 0; i < nodes.Count; i++)
        {
            if (nodes[i].fCost < lowestNode.fCost)
            {
                lowestNode = nodes[i];
            }
        }
        return lowestNode;
    }
}
