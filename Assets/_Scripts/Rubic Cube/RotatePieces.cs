using System.Collections;
using UnityEngine;

public class RotatePieces : MonoBehaviour
{
    private float rotationTime;

    private void Start()
    {
        rotationTime = GameManager.Instance.playerSettings.pieceRotateSpeed;
        StartCoroutine(ScrambleRubicCubes(GameManager.Instance.playerSettings.scramblTime,GameManager.Instance.playerSettings.ScramblRotationSpeed));
    }

    public IEnumerator ScrambleRubicCubes(int scrambleTimes, float scrambleRotationTime)
    {
        GameManager.Instance.Scrambling = true;

        yield return new WaitForSeconds(1.5f);
        float oldRotationTime = rotationTime;
        rotationTime = scrambleRotationTime;

        for (int i = 0; i < scrambleTimes; i++)
        {
            int rotationType = Random.Range(0, 3);
            int rotationIndex = Random.Range(0, GameManager.Instance.playerSettings.rubicSize);
            int rotationAngle = Random.Range(-1, 1) < 0 ? -90 : 90;
            switch (rotationType)
            {
                case 0:
                    yield return StartCoroutine(RotateAlongX(rotationAngle, rotationIndex));
                    break;
                case 1:
                    yield return StartCoroutine(RotateAlongY(rotationAngle, rotationIndex));
                    break;
                case 2:
                    yield return StartCoroutine(RotateAlongZ(rotationAngle, rotationIndex));
                    break;
                default:
                    break;
            }
        }

        rotationTime = oldRotationTime;
        GameManager.Instance.Scrambling = false;
        GameManager.Instance.currentMenu =  Instantiate(GameManager.Instance.playerSettings.gameMenu);
    }

    public IEnumerator RotateAlongZ(float angle, int rotationIndex)
    {
        GameManager.Instance.faceRotate = true;
        GameObject pieceRotation = new GameObject("Piece Rotation");

        float elapsedTime = 0;

        // Unparent the cubes to be rotated
        for (int x = 0; x < GameManager.Instance.playerSettings.rubicSize; x++)
            for (int y = 0; y < GameManager.Instance.playerSettings.rubicSize; y++)
                GameManager.Instance.rubicPieceRoot[x, y, rotationIndex].transform.parent = pieceRotation.transform;

        yield return new WaitForSeconds(0.2f);

       // Rotate cubes unparnted in new rotation parent
       Quaternion quaternion = Quaternion.Euler(0f, 0f, angle);
       while (elapsedTime < rotationTime)
       {
           pieceRotation.transform.rotation = Quaternion.Lerp(pieceRotation.transform.localRotation, quaternion, (elapsedTime / rotationTime));
           elapsedTime += Time.deltaTime;
           yield return null;
       }
     
       // Parent back the rotated cubes
       pieceRotation.transform.rotation = quaternion;
       for (int x = 0; x < GameManager.Instance.playerSettings.rubicSize; x++)
           for (int y = 0; y < GameManager.Instance.playerSettings.rubicSize; y++)
                GameManager.Instance.rubicPieceRoot[x, y, rotationIndex].transform.parent = GameManager.Instance.rubicCube.transform;

        // Fix the location of the rotated cubes in the array
        GameManager.Instance.rubicPieceRoot = ResetPositionAfterRotation();
        Destroy(pieceRotation);
        GameManager.Instance.faceRotate = false;
     
       yield return new WaitForSeconds(0.1f);

    }

    public IEnumerator RotateAlongX(float angle, int rotationIndex)
    {

            GameManager.Instance.faceRotate = true;          
            GameObject pieceRotation = new GameObject("Piece Rotation");
          
            float elapsedTime = 0;

            // Unparent the cubes to be rotated
            for (int y = 0; y < GameManager.Instance.playerSettings.rubicSize; y++)
                for (int z = 0; z < GameManager.Instance.playerSettings.rubicSize; z++)
                   GameManager.Instance.rubicPieceRoot[rotationIndex, y, z].transform.parent = pieceRotation.transform;

           // Rotate cubes unparnted in new rotation parent
            Quaternion quaternion = Quaternion.Euler(angle, 0f, 0f);
            while (elapsedTime < rotationTime)
            {
                pieceRotation.transform.rotation = Quaternion.Lerp(pieceRotation.transform.localRotation, quaternion, (elapsedTime / rotationTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

           // Parent back the rotated cubes
            pieceRotation.transform.rotation = quaternion;
            for (int y = 0; y < GameManager.Instance.playerSettings.rubicSize; y++)
                for (int z = 0; z < GameManager.Instance.playerSettings.rubicSize; z++)
                    GameManager.Instance.rubicPieceRoot[rotationIndex, y, z].transform.parent = GameManager.Instance.rubicCube.transform;


            GameManager.Instance.rubicPieceRoot = ResetPositionAfterRotation();

            Destroy(pieceRotation);
            GameManager.Instance.faceRotate = false;
           
            yield return new WaitForSeconds(0.1f);            
    }

    public IEnumerator RotateAlongY(float angle, int rotationIndex)
    {
            GameManager.Instance.faceRotate = true;           
            GameObject pieceRotation = new GameObject("Piece Rotation");

            float elapsedTime = 0;

            // Unparent the cubes to be rotated
            for (int x = 0; x < GameManager.Instance.playerSettings.rubicSize; x++)
                for (int z = 0; z < GameManager.Instance.playerSettings.rubicSize; z++)
                    GameManager.Instance.rubicPieceRoot[x, rotationIndex, z].transform.parent = pieceRotation.transform;

            // Rotate cubes unparnted in new rotation parent
            Quaternion quaternion = Quaternion.Euler(0f, angle, 0f);
            while (elapsedTime < rotationTime)
            {
                pieceRotation.transform.rotation = Quaternion.Lerp(pieceRotation.transform.localRotation, quaternion, (elapsedTime / rotationTime));
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Parent back the rotated cubes
            pieceRotation.transform.rotation = quaternion;
            for (int x = 0; x < GameManager.Instance.playerSettings.rubicSize; x++)
                for (int z = 0; z < GameManager.Instance.playerSettings.rubicSize; z++)
                   GameManager.Instance.rubicPieceRoot[x, rotationIndex, z].transform.parent = GameManager.Instance.rubicCube.transform;

           // Fix the location of the rotated cubes in the array
           GameManager.Instance.rubicPieceRoot = ResetPositionAfterRotation();

            Destroy(pieceRotation);

           GameManager.Instance.faceRotate = false;
         
           yield return new WaitForSeconds(0.1f);      
    }

    public GameObject[,,] ResetPositionAfterRotation()
    {
        if (GameManager.Instance.Scrambling == false)
            GameManager.Instance.facesCheck.ReadState();

        float multi = GameManager.Instance.playerSettings.rubicSize / 2f - 0.5f;
        GameObject[,,] newPiece = new GameObject[GameManager.Instance.playerSettings.rubicSize, GameManager.Instance.playerSettings.rubicSize, GameManager.Instance.playerSettings.rubicSize];

        for (int x = 0; x < GameManager.Instance.playerSettings.rubicSize; x++)
        {
            for (int y = 0; y < GameManager.Instance.playerSettings.rubicSize; y++)
            {
                for (int z = 0; z < GameManager.Instance.playerSettings.rubicSize; z++)
                {

                    for (int x2 = 0; x2 < GameManager.Instance.playerSettings.rubicSize; x2++)
                    {
                        for (int y2 = 0; y2 < GameManager.Instance.playerSettings.rubicSize; y2++)
                        {
                            for (int z2 = 0; z2 < GameManager.Instance.playerSettings.rubicSize; z2++)
                            {

                                if (GameManager.Instance.rubicPieceRoot[x2, y2, z2].transform.position == new Vector3(-multi + x, -multi + y, -multi + z))
                                {
                                    newPiece[x, y, z] = GameManager.Instance.rubicPieceRoot[x2, y2, z2];
                                }

                            }
                        }
                    }

                }
            }
        }

        return newPiece;
    }
}
