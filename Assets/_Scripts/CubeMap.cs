using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    CubeState cubeState;

    public Transform up;
    public Transform down;
    public Transform right;
    public Transform left;
    public Transform front;
    public Transform back;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMap()
    {
        cubeState = FindObjectOfType<CubeState>();

        UpdateMap(cubeState.front, front);
       // UpdateMap(cubeState.top, up);
       // UpdateMap(cubeState.down, down);
       // UpdateMap(cubeState.right, right);
       // UpdateMap(cubeState.left, left);
       // UpdateMap(cubeState.back, back);
       
    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach(Transform t in side)
        {
            if(face[i].name[0] == 'F')
            {
                t.GetComponent<Image>().color = new Color(1, 0.5f, 0,1);
            }

           // if (face[i].name[0] == 'B')
           // {
           //     t.GetComponent<Image>().color = Color.red;
           // }
           //
           // if (face[i].name[0] == 'U')
           // {
           //     t.GetComponent<Image>().color = Color.yellow;
           // }
           //
           // if (face[i].name[0] == 'D')
           // {
           //     t.GetComponent<Image>().color = Color.white;
           // }
           //
           // if (face[i].name[0] == 'L')
           // {
           //     t.GetComponent<Image>().color = Color.green;
           // }
           //
           // if (face[i].name[0] == 'R')
           // {
           //     t.GetComponent<Image>().color = Color.blue;
           // }
            i++;
        }
    }
}
