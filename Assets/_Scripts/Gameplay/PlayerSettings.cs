using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerSetting", menuName = "Magic Cube/Player Setting", order = 1)]
public class PlayerSettings : ScriptableObject
{
    [Range(2,6)]
    public int rubicSize = 2;
    public float pieceSpace;
    public float pieceRotateSpeed;

    public int scramblTime;
    public float ScramblRotationSpeed;

    public float cameraDragSpeed;
    public float cameraRotateSpeed;

    public float timer;

    public GameObject pieceObject;
    public GameObject emptyTransfrom;

    public GameObject startMenu;
    public GameObject rubicSizeSelectionMenu;
    public GameObject winMenu;
    public GameObject gameOverMenu;
    public GameObject gameMenu;

    public LayerMask faceLayer;
}
