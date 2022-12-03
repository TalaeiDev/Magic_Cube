using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject rubicPiece;
    public GameObject emptyTransfrom;
    public Transform rubicCube;
    public List<GameObject> rayPoints;

    public List<GameObject> frontRay;
    public List<GameObject> backRay;
    public List<GameObject> upRay;
    public List<GameObject> downRay;
    public List<GameObject> rightRay;
    public List<GameObject> leftRay;

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
