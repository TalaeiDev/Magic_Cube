using System.Collections.Generic;
using UnityEngine;

public class CubeFacesCheck : MonoBehaviour
{
    public LayerMask layerMask;
    public bool debug;


    private GameManager gameManager;

    CubeState cubeState;

    private void Start()
    {
        gameManager = GameManager.Instance;
        cubeState = gameManager.cubeState;
    }
    public void ReadState()
    {       
       cubeState.front = ReadFace(gameManager.cubeState.frontRay, gameManager.cubeState.rayPoints[0].transform);
       cubeState.back = ReadFace(gameManager.cubeState.backRay, gameManager.cubeState.rayPoints[1].transform);
       cubeState.up = ReadFace(gameManager.cubeState.upRay, gameManager.cubeState.rayPoints[2].transform);
       cubeState.down = ReadFace(gameManager.cubeState.downRay, gameManager.cubeState.rayPoints[3].transform);
       cubeState.right = ReadFace(gameManager.cubeState.rightRay, gameManager.cubeState.rayPoints[4].transform);
       cubeState.left = ReadFace(gameManager.cubeState.leftRay, gameManager.cubeState.rayPoints[5].transform);

       GameManager.Instance.cubeState.CheckFaceState();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.Scrambling)
           ReadState(); 
    }

    public List<GameObject> ReadFace(List<GameObject> listStarts, Transform rayTransform)
    {
        List<GameObject> faceHit = new List<GameObject>();

        foreach(GameObject rayStart in listStarts)
        {           
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;

            if (debug)
                Debug.DrawRay(ray, rayTransform.forward * 3, Color.red);

            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                if(debug)
                  Debug.DrawRay(ray, rayTransform.forward * 3, Color.yellow);

                hit.collider.gameObject.GetComponent<FaceState>().inGame = true;
                faceHit.Add(hit.collider.gameObject);
            }           
        }
        return faceHit;
    }
}
