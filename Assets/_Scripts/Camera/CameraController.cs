using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private Vector3 previousMousePostion;
    private Vector3 mouseDelta;

    private float rotateSpeed;
    private float dragSpeed;

    private void Start()
    {
        rotateSpeed = GameManager.Instance.playerSettings.cameraRotateSpeed;
        dragSpeed = GameManager.Instance.playerSettings.cameraDragSpeed;
        Camera.main.gameObject.transform.localPosition = new Vector3(GameManager.Instance.playerSettings.rubicSize + 1, GameManager.Instance.playerSettings.rubicSize + 1, (GameManager.Instance.playerSettings.rubicSize * 2) + 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.Scrambling || GameManager.Instance.gameOver) return;

        Swipe();
        Drag();

        if (transform.rotation != GameManager.Instance.targetRotation.rotation)
        {
            var step = rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, GameManager.Instance.targetRotation.rotation, step);
        }

    }

    private void Drag()
    {
        if (Input.GetMouseButton(1))
        {
            mouseDelta = Input.mousePosition - previousMousePostion;
            mouseDelta *= dragSpeed;

            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;
        }

        else
           if (transform.rotation != GameManager.Instance.targetRotation.rotation)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, GameManager.Instance.targetRotation.rotation, rotateSpeed * Time.deltaTime);
        previousMousePostion = Input.mousePosition;
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (Input.GetMouseButtonUp(1))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            currentSwipe.Normalize();

            if (LeftSwipe(currentSwipe))
                GameManager.Instance.targetRotation.Rotate(0, 90, 0, Space.World);

            else if (RightSwipe(currentSwipe))
                GameManager.Instance.targetRotation.Rotate(0, -90, 0, Space.World);

            else if (UpLeftSwipe(currentSwipe))
                GameManager.Instance.targetRotation.Rotate(90, 0, 0, Space.World);

            else if (UpRightSwipe(currentSwipe))
                GameManager.Instance.targetRotation.Rotate(0, 0, -90, Space.World);

            else if (DownLeftSwipe(currentSwipe))
                GameManager.Instance.targetRotation.Rotate(0, 0, 90, Space.World);

            else if (DownRightSwipe(currentSwipe))
                GameManager.Instance.targetRotation.Rotate(-90, 0, 0, Space.World);
        }
    }

    private bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }
    private bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    private bool UpLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x < 0;
    }

    private bool UpRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0;
    }

    private bool DownLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0;
    }

    private bool DownRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0;
    }
}
