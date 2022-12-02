using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public Transform rubicCube;

    [HideInInspector]
    public Transform rotationTarget;

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
        initializing();    
    }

    private void initializing()
    {
        rotationTarget = new GameObject("Rotation Target").GetComponent<Transform>();
    }

}
