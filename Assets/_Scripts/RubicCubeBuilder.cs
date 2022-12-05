using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class RubicCubeBuilder : MonoBehaviour
{
    public GameManager gameManager;
    [Range (2,20)]
    public int rubicSize = 2;
    public float pieceSpace = 1f;
    //public GameObject 
    
    private GameObject pieceRoot;

    private GameObject smallCube;
    public void Build()
    {
          gameManager.rayPoints = new List<GameObject>();
         
              if (pieceRoot == null)
                  pieceRoot = GameObject.Find("Piece Root");
         
              if(pieceRoot != null)
                  DestroyImmediate(pieceRoot);
         
              pieceRoot = new GameObject("Piece Root");
              pieceRoot.transform.parent = gameManager.rubicCube;
         
              for (int y = 0; y < rubicSize; y++)
                  for (int x = 0; x < rubicSize; x++)
                      for (int z = 0; z < rubicSize; z++)
                      {
                        GameObject piece =  Instantiate(gameManager.rubicPiece, new Vector3(x, y, z) * pieceSpace, Quaternion.identity, pieceRoot.transform);
                        piece.transform.localPosition = new Vector3(x - rubicSize * 0.5f + 0.5f, y - rubicSize * 0.5f + 0.5f, z - rubicSize * 0.5f + 0.5f);
                      }

        for(int i=0; i < 6; i++)
        {
            GameObject raytransfrom = new GameObject("RayPoint");
            gameManager.rayPoints.Add(raytransfrom.gameObject);

            if (i == 0)
                gameManager.frontRay = BuildRaysPoints(raytransfrom.transform, gameManager.emptyTransfrom, i);

           //if (i == 1)
           //    gameManager.backRay = BuildRaysPoints(raytransfrom.transform, gameManager.emptyTransfrom, i);
           //
           //if (i == 2)
           //    gameManager.upRay = BuildRaysPoints(raytransfrom.transform, gameManager.emptyTransfrom, i);
           //
           //if (i == 3)
           //    gameManager.downRay = BuildRaysPoints(raytransfrom.transform, gameManager.emptyTransfrom, i);
           //
           //if (i == 4)
           //    gameManager.rightRay = BuildRaysPoints(raytransfrom.transform, gameManager.emptyTransfrom, i);
           //
           //if (i == 5)
           //    gameManager.leftRay = BuildRaysPoints(raytransfrom.transform, gameManager.emptyTransfrom, i);
        }
        
         
        

    }


    List<GameObject> BuildRaysPoints(Transform rayTransform, GameObject emptyObject,int index)
    {

        switch (index)
        {
            case 0:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 90));
                rayTransform.parent = pieceRoot.transform;

                rayTransform.localPosition = new Vector3(0, 0, rayTransform.localPosition.z + rubicSize / 2 + 1f);
                rayTransform.name = "FrontRay";
                break;

           //case 1:
           //    rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, -180));
           //    rayTransform.parent = pieceRoot.transform;
           //
           //    foreach (Transform child in pieceRoot.transform)
           //        rubicSumPosition += child.position;
           //
           //    rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
           //    rayTransform.position = new Vector3(rubicCenter.x, rubicCenter.y, rubicCenter.z - rubicSize);
           //    rayTransform.localPosition = new Vector3((float)rubicSize - 2, 1, rayTransform.localPosition.z);
           //    rayTransform.name = "BackRay";
           //    break;
           //
           //case 2:
           //    rayTransform.localRotation = Quaternion.Euler(new Vector3(90, 90, 0));
           //    rayTransform.parent = pieceRoot.transform;
           //
           //    foreach (Transform child in pieceRoot.transform)
           //        rubicSumPosition += child.position;
           //
           //    rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
           //    rayTransform.position = new Vector3(rubicCenter.x, rubicCenter.y + rubicSize, rubicCenter.z);
           //    rayTransform.localPosition = new Vector3((float)rubicSize - 2, rayTransform.localPosition.y, (float)rubicSize - 2);
           //    rayTransform.name = "UpRay";
           //    break;
           //
           //case 3:
           //    rayTransform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 180));
           //    rayTransform.parent = pieceRoot.transform;
           //
           //    foreach (Transform child in pieceRoot.transform)
           //        rubicSumPosition += child.position;
           //
           //    rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
           //    rayTransform.position = new Vector3(rubicCenter.x, rubicCenter.y - rubicSize, rubicCenter.z);
           //    rayTransform.localPosition = new Vector3((float)rubicSize - 2, rayTransform.localPosition.y, (float)rubicSize - 2);
           //    rayTransform.name = "DownRay";
           //    break;
           //
           //case 4:
           //    rayTransform.localRotation = Quaternion.Euler(new Vector3(0, -90, -90));
           //    rayTransform.parent = pieceRoot.transform;
           //
           //    foreach (Transform child in pieceRoot.transform)
           //        rubicSumPosition += child.position;
           //
           //    rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
           //    rayTransform.position = new Vector3(rubicCenter.x + rubicSize, rubicCenter.y, rubicCenter.z);
           //    rayTransform.localPosition = new Vector3(rayTransform.localPosition.x, (float)rubicSize - 2, (float)rubicSize - 2);
           //    rayTransform.name = "RightRay";
           //    break;
           //
           //case 5:
           //    rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
           //    rayTransform.parent = pieceRoot.transform;
           //
           //    foreach (Transform child in pieceRoot.transform)
           //        rubicSumPosition += child.position;
           //
           //    rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
           //    rayTransform.position = new Vector3(rubicCenter.x - rubicSize, rubicCenter.y, rubicCenter.z);
           //    rayTransform.localPosition = new Vector3(rayTransform.localPosition.x, (float)rubicSize - 2, (float)rubicSize - 2);
           //    rayTransform.name = "LeftRay";
           //    break;
        }

        int rayCount = 0;

        Vector3 rubicSumPosition = Vector3.zero;
        Vector3 rubicCenter = Vector3.zero;

        List<GameObject> rays = new List<GameObject>();

        for (int y = 1; y > -(rubicSize - 1) ; y--)
        {
            for (int x = -1; x < rubicSize -1 ; x++)
            {
                Debug.Log((float)x/2);
                Debug.Log((float)y/2);
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + (float)x + ((float)rubicSize/2 - 0.5f), rayTransform.localPosition.y + (float)y - ((float)rubicSize / 2 - 0.5f), rayTransform.position.z);
                GameObject rayStart = Instantiate(emptyObject, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                //rayTransform.localPosition = new Vector3(rayTransform.position.x)
                rayCount++;
            }
        }


       // switch (index)
       // {
       //     case 0:
       //         rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 90));
       //         rayTransform.parent = pieceRoot.transform;
       //         
       //         rayTransform.localPosition = new Vector3(0, 0, rayTransform.localPosition.z + rubicSize /2 + 1f);
       //         rayTransform.name = "FrontRay";
       //         break;
       //
       //     case 1:
       //         rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, -180));
       //         rayTransform.parent = pieceRoot.transform;
       //
       //         foreach (Transform child in pieceRoot.transform)
       //             rubicSumPosition += child.position;
       //
       //         rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
       //         rayTransform.position = new Vector3(rubicCenter.x, rubicCenter.y, rubicCenter.z - rubicSize);
       //         rayTransform.localPosition = new Vector3((float)rubicSize - 2, 1, rayTransform.localPosition.z);
       //         rayTransform.name = "BackRay";
       //         break;
       //
       //     case 2:
       //         rayTransform.localRotation = Quaternion.Euler(new Vector3(90, 90, 0));
       //         rayTransform.parent = pieceRoot.transform;
       //
       //         foreach (Transform child in pieceRoot.transform)
       //             rubicSumPosition += child.position;
       //
       //         rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
       //         rayTransform.position = new Vector3(rubicCenter.x, rubicCenter.y + rubicSize, rubicCenter.z);
       //         rayTransform.localPosition = new Vector3((float)rubicSize - 2, rayTransform.localPosition.y, (float)rubicSize - 2);
       //         rayTransform.name = "UpRay";
       //         break;
       //
       //     case 3:
       //         rayTransform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 180));
       //         rayTransform.parent = pieceRoot.transform;
       //
       //         foreach (Transform child in pieceRoot.transform)
       //             rubicSumPosition += child.position;
       //
       //         rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
       //         rayTransform.position = new Vector3(rubicCenter.x, rubicCenter.y - rubicSize, rubicCenter.z);
       //         rayTransform.localPosition = new Vector3((float)rubicSize - 2, rayTransform.localPosition.y, (float)rubicSize - 2);
       //         rayTransform.name = "DownRay";
       //         break;
       //
       //     case 4:
       //         rayTransform.localRotation = Quaternion.Euler(new Vector3(0, -90, -90));
       //         rayTransform.parent = pieceRoot.transform;
       //
       //         foreach (Transform child in pieceRoot.transform)
       //             rubicSumPosition += child.position;
       //
       //         rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
       //         rayTransform.position = new Vector3(rubicCenter.x + rubicSize, rubicCenter.y, rubicCenter.z);
       //         rayTransform.localPosition = new Vector3(rayTransform.localPosition.x, (float)rubicSize - 2, (float)rubicSize - 2);
       //         rayTransform.name = "RightRay";
       //         break;
       //
       //     case 5:
       //         rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
       //         rayTransform.parent = pieceRoot.transform;
       //
       //         foreach (Transform child in pieceRoot.transform)
       //             rubicSumPosition += child.position;
       //
       //         rubicCenter = rubicSumPosition / pieceRoot.transform.childCount;
       //         rayTransform.position = new Vector3(rubicCenter.x - rubicSize, rubicCenter.y, rubicCenter.z);
       //         rayTransform.localPosition = new Vector3(rayTransform.localPosition.x, (float)rubicSize - 2, (float)rubicSize - 2);
       //         rayTransform.name = "LeftRay";
       //         break;
       // }

        return rays;
    }
}
