using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    [Header("Ray Points Root")]
    [Space(5)]
    public List<GameObject> rayPoints;
    [Space(10)]

    [Header("All Sides Ray Points")]
    [Space(5)]
    public List<GameObject> frontRay;
    public List<GameObject> backRay;
    public List<GameObject> rightRay;
    public List<GameObject> leftRay;
    public List<GameObject> upRay;
    public List<GameObject> downRay;
    [Space(10)]

    [Header("Cube Sides Piece")]
    [Space(5)]
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();


    private int[] frontNumber;
    private int[] backNumber;
    private int[] rightNumber;
    private int[] leftNumber;
    private int[] upNumber;
    private int[] downNumber;

    private int frontSum;
    private int backSum;
    private int rightSum;
    private int leftSum;
    private int upSum;
    private int downSum;

    private void Start()
    {
        frontNumber = new int[GameManager.Instance.playerSettings.rubicSize * 2];
        backNumber = new int[GameManager.Instance.playerSettings.rubicSize * 2];
        rightNumber = new int[GameManager.Instance.playerSettings.rubicSize * 2];
        leftNumber = new int[GameManager.Instance.playerSettings.rubicSize * 2];
        upNumber = new int[GameManager.Instance.playerSettings.rubicSize * 2];
        downNumber = new int[GameManager.Instance.playerSettings.rubicSize * 2];

        frontSum = (GameManager.Instance.playerSettings.rubicSize * 2) * 0;
        backSum = (GameManager.Instance.playerSettings.rubicSize * 2) * 2;
        rightSum = (GameManager.Instance.playerSettings.rubicSize * 2) * 4;
        leftSum = (GameManager.Instance.playerSettings.rubicSize * 2) * 6;
        upSum = (GameManager.Instance.playerSettings.rubicSize * 2) * 8;
        downSum = (GameManager.Instance.playerSettings.rubicSize * 2) * 10;
    }

    public void CheckFaceState()
    {
        if (GameManager.Instance.Scrambling) return;

        UpdateFaceState(front, frontNumber);
        UpdateFaceState(back, backNumber);
        UpdateFaceState(right, rightNumber);
        UpdateFaceState(left, leftNumber);
        UpdateFaceState(up, upNumber);
        UpdateFaceState(down, downNumber);
    }

    void UpdateFaceState(List<GameObject> face, int[] sideNumbers)
    {
        for (int i = 0; i < sideNumbers.Length; i++)
        {
            if (face[i].name[0] == 'F')
                sideNumbers[i] = 0;

            if (face[i].name[0] == 'B')
                sideNumbers[i] = 2;
            
            if (face[i].name[0] == 'T')
                sideNumbers[i] = 8;

            if (face[i].name[0] == 'D')
                sideNumbers[i] = 10;

            if (face[i].name[0] == 'L')
                sideNumbers[i] = 6;

            if (face[i].name[0] == 'R')
                sideNumbers[i] = 4;
        }

      GameManager.Instance.win  = CheckWinState();
      GameManager.Instance.CheckWin();
    }

    private bool CheckWinState()
    {
        var sumFront = 0;
        var sumBack = 0;
        var sumRight = 0;
        var sumLeft = 0;
        var sumUp = 0;
        var sumDown = 0;

        for (int i = 0; i < frontNumber.Length; i++)
        {
            sumFront += frontNumber[i];
            sumBack += backNumber[i];
            sumRight += rightNumber[i];
            sumLeft += leftNumber[i];
            sumUp += upNumber[i];
            sumDown += downNumber[i];
        }

        bool win = (sumFront == frontSum && sumBack == backSum && sumRight == rightSum && sumLeft == leftSum && sumUp == upSum && sumDown == downSum);

        return win;

    }
}
