using UnityEngine;

public class CubeRotate : MonoBehaviour
{
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private Vector3 previousMousePostion;
    private Vector3 mouseDelta;

    public float rotateSpeed;
    public float dragSpeed;

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
       
    }

    private void Drag()
    {
        if(Input.GetMouseButton(1))
        {
            mouseDelta = Input.mousePosition - previousMousePostion;
            mouseDelta *= dragSpeed;

            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0) * transform.rotation;
        }

        else
            if (transform.rotation != GameManager.Instance.rotationTarget.rotation)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, GameManager.Instance.rotationTarget.rotation, rotateSpeed * Time.deltaTime);

        previousMousePostion = Input.mousePosition;
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if(Input.GetMouseButtonUp(1))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            currentSwipe.Normalize();

            if (LeftSwipe(currentSwipe))
                GameManager.Instance.rotationTarget.Rotate(0, 90, 0, Space.World);

            else if (RightSwipe(currentSwipe))
                GameManager.Instance.rotationTarget.Rotate(0, -90, 0, Space.World);

            else if (UpLeftSwipe(currentSwipe))
                GameManager.Instance.rotationTarget.Rotate(90, 0, 0, Space.World);

            else if (UpRightSwipe(currentSwipe))
                GameManager.Instance.rotationTarget.Rotate(0, 0, -90, Space.World);

            else if (DownLeftSwipe(currentSwipe))
                GameManager.Instance.rotationTarget.Rotate(0, 0, 90, Space.World);

            else if (DownRightSwipe(currentSwipe))
                GameManager.Instance.rotationTarget.Rotate(-90, 0, 0, Space.World);
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
