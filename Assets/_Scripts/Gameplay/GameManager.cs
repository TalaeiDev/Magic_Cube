using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public PlayerSettings playerSettings;
    public Transform cameraPoint;
    public bool debug;

    [HideInInspector]
    public GameObject rubicCube;
    [HideInInspector]
    public RotatePieces pieceRotate;
    [HideInInspector]
    public Transform rubicCubeRoot;
    [HideInInspector]
    public CubeState cubeState;
    [HideInInspector]
    public CubeFacesCheck facesCheck;
    [HideInInspector]
    public Transform targetRotation;
    public GameObject[,,] rubicPieceRoot;

    [HideInInspector]
    public GameObject currentMenu;

    private RubicCubeBuilder rubicCubeBuilder;

    public bool mainMenu;

    public bool stop;
    public bool faceRotate;
    public bool Scrambling;

    public bool win;
    public bool gameOver;

    public float timer;

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
        targetRotation = new GameObject("Target Rotation").transform;
        timer = playerSettings.timer;

        if (mainMenu)
            Instantiate(playerSettings.startMenu, Vector3.zero, Quaternion.identity);


        else
        {
            rubicCubeBuilder = FindObjectOfType<RubicCubeBuilder>();
            rubicCubeRoot = new GameObject("RubicCube Root").transform;

            facesCheck = rubicCubeRoot.gameObject.AddComponent<CubeFacesCheck>();
            facesCheck.layerMask = playerSettings.faceLayer;
            facesCheck.debug = debug;
            cubeState = rubicCubeRoot.gameObject.AddComponent<CubeState>();
            pieceRotate = rubicCubeRoot.gameObject.AddComponent<RotatePieces>();

            rubicCubeBuilder.Build();
            StartCoroutine(waiter());
        }
        
    }

    public void SetFace()
    {      
        foreach (Transform t in rubicCube.GetComponentsInChildren<Transform>())
            if (t.gameObject.GetComponent<FaceState>() != null && t.gameObject.GetComponent<FaceState>().inGame == false)
                Destroy(t.gameObject);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.5f);
        SetFace();
    }

    public void CheckWin()
    {
        if (win)
        {
            Instantiate(playerSettings.winMenu);
            Destroy(currentMenu);
        }
            
    }

}
