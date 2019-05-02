using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObstacle : MonoBehaviour {
    public float positioningSpeed = 0.08f;
    public float scaleSpeed = 0.08f;
    public static bool enabledObstaclePositioning;
    public static GameObject thisGameObject;
    Color initialColor;
    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        if (enabledObstaclePositioning)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    if (thisGameObject != null)
                    {
                        thisGameObject.transform.Translate(touchDeltaPosition.x * positioningSpeed, 0, touchDeltaPosition.y * positioningSpeed, Space.World);
                    }
                }
            }
            
                    // If there are two touches on the device...
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                Vector2 prevDir = touchOnePrevPos - touchZeroPrevPos;
                Vector2 currDir = touchOne.position - touchZero.position;

                if (thisGameObject != null)
                {
                    Vector3 newScale = thisGameObject.transform.localScale - new Vector3(deltaMagnitudeDiff * scaleSpeed, deltaMagnitudeDiff * scaleSpeed, deltaMagnitudeDiff * scaleSpeed);
                    if (newScale.x > 1 && newScale.x < 50 && newScale.y > 1 && newScale.y < 50 && newScale.z > 1 && newScale.z < 50)
                    {
                        thisGameObject.transform.localScale = newScale;
                    }
                    float angle = Vector2.SignedAngle(prevDir, currDir);
                    thisGameObject.transform.Rotate(0, -angle, 0);
                }
            }
        }
    }
}
