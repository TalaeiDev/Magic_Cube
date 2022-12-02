using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColorGet : MonoBehaviour
{
    public Transform topPoint;
    public Transform downPoint;
    public Transform rightPoint;
    public Transform leftPoint;
    public Transform frontPoint;
    public Transform backPoint;

    public GameObject emptyGo;

    public LayerMask layerMask;

    private List<GameObject> frontRay = new List<GameObject>();
    private List<GameObject> backRay = new List<GameObject>();
    private List<GameObject> upRay = new List<GameObject>();
    private List<GameObject> downRay = new List<GameObject>();
    private List<GameObject> rightRay = new List<GameObject>();
    private List<GameObject> leftRay = new List<GameObject>();

    CubeState cubeState;
    CubeMap cubeMap;
    // Start is called before the first frame update
    void Start()
    {
        //cubeState = FindObjectOfType<CubeState>();
        //cubeMap = FindObjectOfType<CubeMap>();
        SetRayTransforms();

    }
    public void ReadState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();

        cubeState.front = ReadFace(frontRay, frontPoint);
        cubeState.back = ReadFace(backRay, backPoint);
        cubeState.top = ReadFace(upRay, topPoint);
        cubeState.down = ReadFace(downRay, downPoint);
        cubeState.right = ReadFace(rightRay, rightPoint);
        cubeState.left = ReadFace(leftRay, leftPoint);

        cubeMap.SetMap();
    }

    void SetRayTransforms()
    {
        upRay = BuildRays(topPoint, new Vector3(90, 90, 0));
        frontRay = BuildRays(frontPoint, new Vector3(0, 180, 90));
        //frontPoint.position = new Vector3(-3 + 1.5f, 3 - 1.5f, 3 + 1.5f);
        backRay = BuildRays(backPoint, new Vector3(0, 270, 0));
        rightRay = BuildRays(rightPoint, new Vector3(0, 0, 0));
        leftRay = BuildRays(leftPoint, new Vector3(0, 180, 0));
        downRay = BuildRays(downPoint, new Vector3(270, 90, 0));
    }

    // Update is called once per frame
    void Update()
    {
        ReadState();

       // Transform rayTransform = frontPoint;
       // Vector3 ray = new Vector3(rayTransform.transform.position.x - 0.5f, rayTransform.transform.position.y, rayTransform.transform.position.z);
       // RaycastHit hit;
       // //Debug.DrawRay(ray, -frontPoint.forward * 10, Color.yellow);
       // if (Physics.Raycast(ray, -rayTransform.forward, out hit, Mathf.Infinity, layerMask))
       // {
       //     Debug.DrawRay(ray, -rayTransform.forward * 10, Color.yellow);
       //     faceHit.Add(hit.collider.gameObject);
       // }


        //List<GameObject> faceHit = new List<GameObject>();
        //Transform rayTransform = frontPoint;
        //Vector3 ray = new Vector3(rayTransform.transform.position.x - 0.5f, rayTransform.transform.position.y, rayTransform.transform.position.z);
        //RaycastHit hit;
        ////Debug.DrawRay(ray, -frontPoint.forward * 10, Color.yellow);
        //if (Physics.Raycast(ray,-rayTransform.forward, out hit,Mathf.Infinity,layerMask))
        //{
        //    Debug.DrawRay(ray, -rayTransform.forward * 10, Color.yellow);
        //    faceHit.Add(hit.collider.gameObject);
        //}


        //  cubeState.front = faceHit;
        //  cubeMap.SetMap();
    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 Diraction)
    {
        int rayCount = 0;

        List<GameObject> rays = new List<GameObject>();

        for(int y =1; y > -1; y--)
        {
            for (int x=-1; x<1; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + ((float)x - 0.5f), rayTransform.localPosition.y + ((float)y -0.5f), rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGo, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }

        rayTransform.localRotation = Quaternion.Euler(Diraction);
        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> listStarts, Transform rayTransform)
    {
        List<GameObject> faceHit = new List<GameObject>();
        foreach(GameObject rayStart in listStarts)
        {
            //Transform rayTransform = frontPoint;
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;
            //Debug.DrawRay(ray, -frontPoint.forward * 10, Color.yellow);
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * 10, Color.yellow);
                faceHit.Add(hit.collider.gameObject);
            }
           
        }
        return faceHit;
        //cubeMap.SetMap();
    }
}
