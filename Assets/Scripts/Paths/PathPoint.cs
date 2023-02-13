using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPoint : MonoBehaviour
{
    public Vector2 coords;
    public Tile tile;
    public int index;

    public void Initialise(Vector2 coords, int index)
    {
        this.coords = coords;
        tile = TDRoyaleSingleton.Instance.tileMap.GetTileFromWorldPosition(transform.position);
        this.index = index;
    }

    /*private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Monster")
        {
            col.gameObject.GetComponent<Monster>().SetPathPoint(index);
        }
    }*/
}
