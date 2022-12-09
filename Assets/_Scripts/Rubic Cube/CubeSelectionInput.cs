using UnityEngine;

public class CubeSelectionInput : MonoBehaviour
{
    public enum _Input { Mouse, Touch}
    public _Input input;

    private GameObject firstHit;
    private Vector3 firstHitNormal;
    private Vector3 firstHitCenter;

    private GameObject secondHit;
    private Vector3 secondHitNormal;
    private Vector3 secondHitCenter;

    private float offset;
    private float rotationAngle = 90f;

    // Start is called before the first frame update
    void Start()
    {
        offset = GameManager.Instance.playerSettings.rubicSize * 0.5f - 0.5f;
    }

    private void Update()
    {
        if (GameManager.Instance.Scrambling || GameManager.Instance.win || GameManager.Instance.gameOver || GameManager.Instance.stop) return;
        if (input == _Input.Mouse)
            MouseButtonInput();

        if (input == _Input.Touch)
            TouchInput();
    }

    private void MouseButtonInput()
    {

        if (Input.GetMouseButtonDown(0))
        {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    firstHitNormal = hit.normal;
                    firstHitCenter = hit.transform.gameObject.GetComponent<Renderer>().bounds.center;
                    firstHit = hit.transform.parent.gameObject;
                }  
        }

        if (Input.GetMouseButtonUp(0))
        {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    secondHitNormal = hit.normal;
                    secondHitCenter = hit.transform.gameObject.GetComponent<Renderer>().bounds.center;
                    secondHit = hit.transform.parent.gameObject;

                    Vector3 move = secondHitCenter - firstHitCenter;
                    move.Normalize();
                   
                    DoTheRotation(move);
                }            
        }
    }

    private void TouchInput()
    {
        
    }

 
    private bool ConfirmWhichRotation(Vector3 normal, Vector3 tester, Vector3 move, char axis)
    {
        Vector3 sum = normal + tester;
        if (axis == 'X')
            sum = new Vector3(Mathf.Abs(move.x), Mathf.Abs(sum.y), Mathf.Abs(sum.z));

        else if (axis == 'Y')
            sum = new Vector3(Mathf.Abs(sum.x), Mathf.Abs(move.y), Mathf.Abs(sum.z));

        else if (axis == 'Z')
            sum = new Vector3(Mathf.Abs(sum.x), Mathf.Abs(sum.y), Mathf.Abs(move.z));

        return sum == new Vector3(1, 1, 1);
    }

    // check if mouse mouse click or touch between down and up state hit 2 difrrent face on cube
    private bool CheckForHitOnDifferentPlanes(Vector3 fromNormal, Vector3 fromCompare, Vector3 toNormal, Vector3 toCompare)
    {
        fromNormal = new Vector3(Mathf.Abs(fromNormal.x), Mathf.Abs(fromNormal.y), Mathf.Abs(fromNormal.z));
        toNormal = new Vector3(Mathf.Abs(toNormal.x), Mathf.Abs(toNormal.y), Mathf.Abs(toNormal.z));
        return (fromNormal == fromCompare && toNormal == toCompare);
    }

    private void DoTheRotation(Vector3 move)
    {
        if (firstHitNormal == secondHitNormal)
        {
            if (ConfirmWhichRotation(firstHitNormal, new Vector3(0, 0, 1), move, 'Y'))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongZ(firstHitNormal.x * move.y * rotationAngle, Mathf.RoundToInt(firstHit.transform.position.z + offset)));

            else if (ConfirmWhichRotation(firstHitNormal, new Vector3(0, 1, 0), move, 'Z'))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongY(firstHitNormal.x * move.z * -rotationAngle, Mathf.RoundToInt(firstHit.transform.position.y + offset)));
 
            else if (ConfirmWhichRotation(firstHitNormal, new Vector3(0, 0, 1), move, 'X'))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongZ(firstHitNormal.y * move.x * -rotationAngle, Mathf.RoundToInt(firstHit.transform.position.z + offset)));

            else if (ConfirmWhichRotation(firstHitNormal, new Vector3(1, 0, 0), move, 'Z'))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongX(firstHitNormal.y * move.z * rotationAngle, Mathf.RoundToInt(firstHit.transform.position.x + offset)));

            else if (ConfirmWhichRotation(firstHitNormal, new Vector3(0, 1, 0), move, 'X'))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongY(firstHitNormal.z * move.x * rotationAngle, Mathf.RoundToInt(firstHit.transform.position.y + offset)));
           
            else if (ConfirmWhichRotation(firstHitNormal, new Vector3(1, 0, 0), move, 'Y'))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongX(firstHitNormal.z * move.y * -rotationAngle, Mathf.RoundToInt(firstHit.transform.position.x + offset)));
         
        }
        else
        {
             if (CheckForHitOnDifferentPlanes(firstHitNormal, new Vector3(0, 0, 1), secondHitNormal, new Vector3(0, 1, 0)))
                 StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongX(firstHitNormal.z * secondHitNormal.y * -rotationAngle, Mathf.RoundToInt(firstHit.transform.position.x + offset)));

            else if (CheckForHitOnDifferentPlanes(firstHitNormal, new Vector3(0, 1, 0), secondHitNormal, new Vector3(0, 0, 1)))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongX(firstHitNormal.y * secondHitNormal.z * rotationAngle, Mathf.RoundToInt(firstHit.transform.position.x + offset)));

            else if (CheckForHitOnDifferentPlanes(firstHitNormal, new Vector3(0, 0, 1), secondHitNormal, new Vector3(1, 0, 0)))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongY(firstHitNormal.z * secondHitNormal.x * rotationAngle, Mathf.RoundToInt(firstHit.transform.position.y + offset)));

            else if (CheckForHitOnDifferentPlanes(firstHitNormal, new Vector3(1, 0, 0), secondHitNormal, new Vector3(0, 0, 1)))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongY(firstHitNormal.x * secondHitNormal.z * -rotationAngle, Mathf.RoundToInt(firstHit.transform.position.y + offset)));

            else if (CheckForHitOnDifferentPlanes(firstHitNormal, new Vector3(0, 1, 0), secondHitNormal, new Vector3(1, 0, 0)))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongZ(firstHitNormal.y * secondHitNormal.x * -rotationAngle, Mathf.RoundToInt(firstHit.transform.position.z + offset)));

            else if (CheckForHitOnDifferentPlanes(firstHitNormal, new Vector3(1, 0, 0), secondHitNormal, new Vector3(0, 1, 0)))
                StartCoroutine(GameManager.Instance.pieceRotate.RotateAlongZ(firstHitNormal.x * secondHitNormal.y * rotationAngle, Mathf.RoundToInt(firstHit.transform.position.z + offset)));
        }
    }   
}
