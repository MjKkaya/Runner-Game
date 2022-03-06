using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    private static readonly float HORIZONTAL_MOVEMENT_STEP = 0.5f;

    public  float speed = 5f;

    private float mLeftBorder;
    private float mRightBorder;
    private bool mIsMove;


    public void SetData(float leftBorder, float rightBorder)
    {
        mIsMove = true;
        mLeftBorder = leftBorder;
        mRightBorder = rightBorder;
        Debug.Log($"PlayerMovement-mLeftBorder:{mLeftBorder}, mRightBorder:{mRightBorder}");
    }

    private void Update()
    {
        if (!mIsMove)
            return;

        if(Input.GetKeyDown(KeyCode.A) && transform.localPosition.x > (mLeftBorder + HORIZONTAL_MOVEMENT_STEP ))
        {
            Debug.Log($"PlayerMovement-x:{transform.localPosition.x}, leftBorder:{(mLeftBorder + HORIZONTAL_MOVEMENT_STEP)}");
            transform.localPosition = new Vector3(transform.localPosition.x - HORIZONTAL_MOVEMENT_STEP, transform.localPosition.y, transform.localPosition.z);
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.localPosition.x < (mRightBorder - HORIZONTAL_MOVEMENT_STEP))
        {
            Debug.Log($"PlayerMovement-x:{transform.localPosition.x}, leftBorder:{(mRightBorder - HORIZONTAL_MOVEMENT_STEP)}");
            transform.localPosition = new Vector3(transform.localPosition.x + HORIZONTAL_MOVEMENT_STEP, transform.localPosition.y, transform.localPosition.z);
        }

        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }
}
