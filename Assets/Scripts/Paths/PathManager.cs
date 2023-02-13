using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PathManager : MonoBehaviour
{
    public PhotonView phView;

    [HideInInspector]
    public PathPoint spawnPoint, playersEndPoint, opponentsEndPoint;
    [SerializeField]
    private PathPoint[] playersPathPoints;
    [SerializeField]
    private PathPoint[] opponentsPathPoints;

    public List<Vector3> playersFullPathPositions, opponentFullPathPositions;
    private List<List<Vector3>> playersIndividualPathPositions, opponentsIndividualPathPoisitons;

    public void Initialise()
    {
        phView = GetComponent<PhotonView>();
        //Debug.Log("Initialising");
        playersFullPathPositions = new List<Vector3>();
        playersIndividualPathPositions = new List<List<Vector3>>();
        opponentFullPathPositions = new List<Vector3>();
        opponentsIndividualPathPoisitons = new List<List<Vector3>>();

        spawnPoint = playersPathPoints[0];
        playersEndPoint = playersPathPoints[playersPathPoints.Length - 1];
        opponentsEndPoint = opponentsPathPoints[opponentsPathPoints.Length - 1];

        for (int i = 0; i < playersPathPoints.Length; i++)
        {
            Vector2 coords = TDRoyaleSingleton.Instance.tileMap.GetTileFromWorldPosition(playersPathPoints[i].transform.position).coords;
            playersPathPoints[i].Initialise(coords,i);
        }

        for (int i = 0; i < opponentsPathPoints.Length; i++)
        {
            Vector2 coords = TDRoyaleSingleton.Instance.tileMap.GetTileFromWorldPosition(opponentsPathPoints[i].transform.position).coords;
            opponentsPathPoints[i].Initialise(coords, i);
        }

        CreateBothPlayersPaths();
    }

    public void CreateBothPlayersPaths()
    {
        //Debug.Log("Creating Player Path");
        CreatePath(1);
        //Debug.Log("Creating Opponent Path");
        CreatePath(2);
    }

    [PunRPC]
    public void CreatePath(int playerID)
    {
        if(GetPlayersFullPathPositions(playerID).Count > 0)
        {
            GetPlayersFullPathPositions(playerID).Clear();
            GetPlayersIndividualPathPositions(playerID).Clear();
        }

        AStar pathfinding = new AStar();

        PathPoint[] pathPoints = GetPlayersPathPoints(playerID);

        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            //Debug.Log("adding to full positions");

            List<Vector3> positions = pathfinding.GetPathPositions(pathPoints[i].tile, pathPoints[i + 1].tile);
            GetPlayersFullPathPositions(playerID).AddRange(positions);
            GetPlayersIndividualPathPositions(playerID).Add(positions);
        }

        //PathfindTesting.ShowDebugTiles(fullPathPositions);
    }

    public PathPoint[] GetPlayersPathPoints(int playerID)
    {
        if(playerID == 1)
        {
            return playersPathPoints;
        }
        else
        {
            return opponentsPathPoints;
        }
    }
    public List<Vector3> GetPlayersFullPathPositions(int playerID)
    {
        if (playerID == 1)
        {
            return playersFullPathPositions;
        }
        else
        {
            return opponentFullPathPositions;
        }
    }
    public List<List<Vector3>> GetPlayersIndividualPathPositions(int playerID)
    {
        if (playerID == 1)
        {
            return playersIndividualPathPositions;
        }
        else
        {
            return opponentsIndividualPathPoisitons;
        }
    }

    public List<Vector3> CreatePathFromMonsterToPlayersEndPoint(int playerID,Tile tileEnemyIsOn,int monsterPathIndex)
    {
        List<Vector3> positions = new List<Vector3>();

        AStar pathfinding = new AStar();

        positions = pathfinding.GetPathPositions(tileEnemyIsOn, GetPlayersPathPoints(playerID)[monsterPathIndex + 1].tile);

        positions.AddRange(GetNextPathPositionsFromPathIndex(playerID,monsterPathIndex));

        //PathfindTesting.ShowDebugTiles(positions);

        return positions;
    }

    //this gets the rest of the path after the initial pathfind from the enemy to the next pathpoint it needs to reach
    private List<Vector3> GetNextPathPositionsFromPathIndex(int playerID,int pathPointIndex)
    {
        List<Vector3> positions = new List<Vector3>();

        for (int i = pathPointIndex; i < playersPathPoints.Length - 2; i++)
        {
            positions.AddRange(GetPlayersIndividualPathPositions(playerID)[i + 1]);
        }

        return positions;
    }

    public Vector3 GetPathPointPosition(int index)
    {
        if((playersIndividualPathPositions.Count - 1) < index)
        {
            return Vector3.zero;
        }
        return playersIndividualPathPositions[index][0];
    }
}
