using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class RubicCubeBuilder : MonoBehaviour
{  
    [HideInInspector]
    public Transform raysRoot;

    public void Build()
    {
        GameManager.Instance.rubicPieceRoot = new GameObject[GameManager.Instance.playerSettings.rubicSize, GameManager.Instance.playerSettings.rubicSize, GameManager.Instance.playerSettings.rubicSize];
        GenerateRubicCube();
    }

    public void GenerateRubicCube()
    {
        GameManager.Instance.cubeState.rayPoints = new List<GameObject>();

        GameManager.Instance.rubicCube = new GameObject("Rubic Cube");
        raysRoot = new GameObject("Rays").transform;

       GameManager.Instance.cameraPoint = GameManager.Instance.rubicCube.transform;

        GameManager.Instance.rubicCube.transform.parent = GameManager.Instance.rubicCubeRoot;
        raysRoot.transform.parent = GameManager.Instance.rubicCubeRoot;

        // build a rubic cube with rubic size user define on runtime or editor 
        for (int y = 0; y < GameManager.Instance.playerSettings.rubicSize; y++)
            for (int x = 0; x < GameManager.Instance.playerSettings.rubicSize; x++)
                for (int z = 0; z < GameManager.Instance.playerSettings.rubicSize; z++)
                {
                    GameObject newRubicPiece = Instantiate(GameManager.Instance.playerSettings.pieceObject) as GameObject;
                    GameManager.Instance.rubicPieceRoot[x, y, z] = newRubicPiece;
                    newRubicPiece.transform.parent = GameManager.Instance.rubicCube.transform;
                    newRubicPiece.transform.localPosition = new Vector3(x - GameManager.Instance.playerSettings.rubicSize * 0.5f + 0.5f,
                        y - GameManager.Instance.playerSettings.rubicSize * 0.5f + 0.5f,
                        z - GameManager.Instance.playerSettings.rubicSize * 0.5f + 0.5f);                   
                }

        // build raypoint for each 6 sides of cube piece depend on cube size for check cube state with raycast 
        for(int i=0; i < 6; i++)
        {
            GameObject raytransfrom = new GameObject();
            GameManager.Instance.cubeState.rayPoints.Add(raytransfrom.gameObject);
       
            if (i == 0)
                GameManager.Instance.cubeState.frontRay = BuildRaysPoints(raytransfrom.transform, GameManager.Instance.playerSettings.emptyTransfrom, i);
       
            if (i == 1)
                GameManager.Instance.cubeState.backRay = BuildRaysPoints(raytransfrom.transform, GameManager.Instance.playerSettings.emptyTransfrom, i);
            
            if (i == 2)
                GameManager.Instance.cubeState.upRay = BuildRaysPoints(raytransfrom.transform, GameManager.Instance.playerSettings.emptyTransfrom, i);
            
            if (i == 3)
                GameManager.Instance.cubeState.downRay = BuildRaysPoints(raytransfrom.transform, GameManager.Instance.playerSettings.emptyTransfrom, i);
            
            if (i == 4)
                GameManager.Instance.cubeState.rightRay = BuildRaysPoints(raytransfrom.transform, GameManager.Instance.playerSettings.emptyTransfrom, i);
            
            if (i == 5)
                GameManager.Instance.cubeState.leftRay = BuildRaysPoints(raytransfrom.transform, GameManager.Instance.playerSettings.emptyTransfrom, i);
        }
    }

    // 
    List<GameObject> BuildRaysPoints(Transform rayTransform, GameObject emptyObject,int index)
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        for (int y = 1; y > -(GameManager.Instance.playerSettings.rubicSize - 1) ; y--)
        {
            for (int x = -1; x < GameManager.Instance.playerSettings.rubicSize - 1 ; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + (float)x + ((float)GameManager.Instance.playerSettings.rubicSize / 2 - 0.5f),
                    rayTransform.localPosition.y + (float)y - ((float)GameManager.Instance.playerSettings.rubicSize / 2 - 0.5f),
                    rayTransform.position.z);

                GameObject rayStart = Instantiate(emptyObject, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }

        rayTransform.parent = raysRoot;

        switch (index)
        {
            case 0:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));                
                rayTransform.localPosition = new Vector3(rayTransform.localPosition.x + (GameManager.Instance.playerSettings.rubicSize - 2),
                    rayTransform.localPosition.y + (GameManager.Instance.playerSettings.rubicSize - 2),
                    rayTransform.localPosition.z + (GameManager.Instance.playerSettings.rubicSize / 2 + 1f));
                rayTransform.name = "FrontRay";
                break;

            case 1:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                rayTransform.localPosition = new Vector3(rayTransform.localPosition.x - (GameManager.Instance.playerSettings.rubicSize - 2),
                    rayTransform.localPosition.y + (GameManager.Instance.playerSettings.rubicSize - 2),
                    rayTransform.localPosition.z -(GameManager.Instance.playerSettings.rubicSize / 2 + 1f));

                rayTransform.name = "BackRay";
                break;
                
            case 2:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));                
                rayTransform.localPosition = new Vector3(rayTransform.localPosition.x - (GameManager.Instance.playerSettings.rubicSize - 2),
                    rayTransform.localPosition.z + (GameManager.Instance.playerSettings.rubicSize / 2 + 1f),
                    rayTransform.localPosition.z + (GameManager.Instance.playerSettings.rubicSize - 2));

                rayTransform.name = "UpRay";                  
                break;
                 
             case 3:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));                    
                rayTransform.localPosition = new Vector3(rayTransform.localPosition.x - (GameManager.Instance.playerSettings.rubicSize - 2),
                    rayTransform.localPosition.z - (GameManager.Instance.playerSettings.rubicSize / 2 + 1f),
                    rayTransform.localPosition.z - (GameManager.Instance.playerSettings.rubicSize - 2));

                rayTransform.name = "DownRay";
                break;
                  
             case 4:      
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));                   
                rayTransform.localPosition = new Vector3(rayTransform.localPosition.z + (GameManager.Instance.playerSettings.rubicSize / 2 + 1f),
                    rayTransform.localPosition.y + (GameManager.Instance.playerSettings.rubicSize - 2),
                    rayTransform.localPosition.z - (GameManager.Instance.playerSettings.rubicSize - 2));

                rayTransform.name = "RightRay";
                break;
                 
             case 5:
                rayTransform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));             
                rayTransform.localPosition = new Vector3(rayTransform.localPosition.z - (GameManager.Instance.playerSettings.rubicSize / 2 + 1f),
                    rayTransform.localPosition.y + (GameManager.Instance.playerSettings.rubicSize - 2),
                    rayTransform.localPosition.z + (GameManager.Instance.playerSettings.rubicSize - 2));

                rayTransform.name = "LeftRay";
                break;
        }
        return rays;
    }
}
