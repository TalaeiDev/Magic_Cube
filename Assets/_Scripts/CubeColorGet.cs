using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorGet : MonoBehaviour
{
    public GameManager gameManager;
    public LayerMask layerMask;

    CubeState cubeState;
    CubeMap cubeMap;
 
    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        cubeState.front = ReadFace(gameManager.frontRay, gameManager.rayPoints[0].transform);
        cubeState.back = ReadFace(gameManager.backRay, gameManager.rayPoints[1].transform);
        cubeState.top = ReadFace(gameManager.upRay, gameManager.rayPoints[2].transform);
        cubeState.down = ReadFace(gameManager.downRay, gameManager.rayPoints[3].transform);
        cubeState.right = ReadFace(gameManager.rightRay, gameManager.rayPoints[4].transform);
        cubeState.left = ReadFace(gameManager.leftRay, gameManager.rayPoints[5].transform);

        cubeMap.SetMap();
    }

    // Update is called once per frame
    void Update()
    {
        ReadState(); 
    }

    public List<GameObject> ReadFace(List<GameObject> listStarts, Transform rayTransform)
    {
        List<GameObject> faceHit = new List<GameObject>();
        foreach(GameObject rayStart in listStarts)
        {            
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;
            Debug.DrawRay(ray, rayTransform.forward * 3, Color.red);
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * 3, Color.yellow);
                faceHit.Add(hit.collider.gameObject);
            }
           
        }
        return faceHit;
    }
}
