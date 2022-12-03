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
                       Instantiate(gameManager.rubicPiece, new Vector3(x,y,z) * pieceSpace, Quaternion.identity,pieceRoot.transform);
           
            float pos = 0.5f * ((float)rubicSize - 1);

            pieceRoot.transform.position = new Vector3(-pos, -pos, -pos);

        for(int i=0; i < 2; i++)
        {
            GameObject raytransfrom = new GameObject("RayPoint");
            gameManager.rayPoints.Add(raytransfrom.gameObject);

            if(i == 0)
              gameManager.frontRay = BuildRaysPoints(raytransfrom.transform, gameManager.emptyTransfrom, i);

            if (i == 1)
                gameManager.backRay = BuildRaysPoints(raytransfrom.transform, gameManager.emptyTransfrom, i);
        }
      
    }


    List<GameObject> BuildRaysPoints(Transform rayTransform, GameObject emptyObject,int index)
    {
        int rayCount = 0;

        List<GameObject> rays = new List<GameObject>();

        for (int y = 1; y > -(rubicSize - 1); y--)
        {
            for (int x = -1; x < rubicSize - 1; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x, rayTransform.localPosition.y + y, rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyObject, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }

        switch(index)
        {
            case 0:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 90));
                rayTransform.parent = pieceRoot.transform;
                rayTransform.localPosition = new Vector3((float)rubicSize - 2, 1, rubicSize + 0.5f);
                rayTransform.name = "FrontRay";
                break;

            case 1:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                rayTransform.parent = pieceRoot.transform;
                rayTransform.localPosition = new Vector3((float)rubicSize - 1, 1, -(rubicSize + 0.5f));
                rayTransform.name = "BackRay";
                break;

            case 3:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 90));
                rayTransform.parent = pieceRoot.transform;
                rayTransform.localPosition = new Vector3((float)rubicSize - 2, 1, 6);
                rayTransform.name = "FrontRay";
                break;

            case 4:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 90));
                rayTransform.parent = pieceRoot.transform;
                rayTransform.localPosition = new Vector3((float)rubicSize - 2, 1, 6);
                rayTransform.name = "FrontRay";
                break;

            case 5:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 90));
                rayTransform.parent = pieceRoot.transform;
                rayTransform.localPosition = new Vector3((float)rubicSize - 2, 1, 6);
                rayTransform.name = "FrontRay";
                break;

            case 6:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 90));
                rayTransform.parent = pieceRoot.transform;
                rayTransform.localPosition = new Vector3((float)rubicSize - 2, 1, 6);
                rayTransform.name = "FrontRay";
                break;
        }
        //rayTransform.localRotation = Quaternion.Euler(Diraction);
        //rayTransform.parent = pieceRoot;
        //rayTransform.localPosition = new Vector3((float)cubeBuilder.rubicSize - 2, 1, 6);
        return rays;
    }
}
