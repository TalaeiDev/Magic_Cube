using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class RubicCubeBuilder : MonoBehaviour
{
    public GameObject rubicPiece;
    public Transform rubicCube;
    [Range (2,20)]
    public int rubicSize = 2;
    public float pieceSpace = 1f;
    
    private GameObject pieceRoot;

    public void Build()
    {
            if (pieceRoot == null)
                pieceRoot = GameObject.Find("Piece Root");

            if(pieceRoot != null)
                DestroyImmediate(pieceRoot);

            pieceRoot = new GameObject("Piece Root");
            pieceRoot.transform.parent = rubicCube;

            for (int y = 0; y < rubicSize; y++)
                for (int x = 0; x < rubicSize; x++)
                    for (int z = 0; z < rubicSize; z++)
                       Instantiate(rubicPiece, new Vector3(x,y,z) * pieceSpace, Quaternion.identity,pieceRoot.transform);
           
            float pos = 0.5f * ((float)rubicSize - 1);

            pieceRoot.transform.position = new Vector3(-pos, -pos, -pos);
      
    }
}
