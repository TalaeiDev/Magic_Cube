using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    CubeState cubeState;
    CubeColorGet readState;

    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        readState = FindObjectOfType<CubeColorGet>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
          //  readState.ReadState();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;

                List<List<GameObject>> cubeObjects = new List<List<GameObject>>()
                {
                    cubeState.top,
                    cubeState.down,
                    cubeState.front,
                    cubeState.back,
                    cubeState.right,
                    cubeState.left
                
                };

                foreach(List<GameObject> f in cubeObjects)
                {
                    if(f.Contains(face))
                    {
                        cubeState.PickUp(f);
                    }
                }

            }
        }
    }
}
