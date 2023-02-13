using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform opponentsCameraPosition;
    
    public void UseOpponentsCamera()
    {
        Camera.main.transform.position = opponentsCameraPosition.position;
        Camera.main.transform.rotation = opponentsCameraPosition.rotation;
    }
}
