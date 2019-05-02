using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    Vector2 startPos;
    Vector3 direction;
    public float rotationSpeed = 2f;
    public float perspectiveZoomSpeed = 0.5f;
    bool isZooming = false, isRotating = false;
    public static bool isCameraMoving = false;
    float minY = 5, maxY = 49.9f, minTransformPsition = 4, maxTransformPosition = 50;
    
    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            if (transform.position.y > minTransformPsition && transform.position.y <= maxTransformPosition)
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(direction);
                transform.RotateAround(Vector3.zero, transform.right, -pos.y * rotationSpeed);
                transform.RotateAround(Vector3.zero, Vector3.up, pos.x * rotationSpeed);
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

                if (transform.position.y == minY && direction.y > 0)
                {
                    rotationSpeed = 0;
                }
                else
                {
                    rotationSpeed = (transform.position.y == maxY && direction.y < 0) ? 0 : 2;
                }
            }
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    isCameraMoving = true;
                    break;
                    
                case TouchPhase.Moved:
                    if (!isZooming)
                    {
                        direction = touch.position - startPos;
                        isRotating = true;
                    }
                    break;

                case TouchPhase.Ended:
                    isZooming = false;
                    isRotating = false;
                    isCameraMoving = false;
                    break;
            }
        }
        
        if (Input.touchCount == 2)
        {
            isZooming = true;
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
            
            
            Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
            
            
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 5, 60);
        }
    }
}
