using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{

    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> right = new List<GameObject>();
    public List<GameObject> left = new List<GameObject>();
    public List<GameObject> top = new List<GameObject>();
    public List<GameObject> down = new List<GameObject>();

    public Transform pieceRoot;

    Vector3 rubicSumPosition;
    Vector3 rubicCenter;

    public List<GameObject> fg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(List<GameObject> cubeSide)
    {
        fg = cubeSide;
        rubicSumPosition = Vector3.zero;



        foreach (GameObject face in cubeSide)
        {

            rubicSumPosition += face.transform.position;
           
        }
        GameObject g = new GameObject("G");
        rubicCenter = rubicSumPosition / cubeSide.Count;
        g.transform.position = rubicCenter;
    }
}
