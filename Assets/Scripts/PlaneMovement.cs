using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour {
    Vector2 startPos;
    Vector3 direction;
    public float rotationSpeed = 0.1f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    if (true)
                    {
                        direction = touch.position - startPos;
                        
                        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                        {
                            Vector3 relativePos = direction - transform.position;
                            Quaternion rotation = Quaternion.LookRotation(relativePos);
                            transform.rotation = rotation;
                        }
                        else
                        {
                            if (transform.position.x < 0)
                            {
                                transform.RotateAround(Vector3.zero, Vector3.forward, touch.deltaPosition.y * rotationSpeed);
                            }
                            else
                            {
                                transform.RotateAround(Vector3.zero, Vector3.right, touch.deltaPosition.y * rotationSpeed);
                            }

                        }
                    }
                    break;
                case TouchPhase.Ended:
                    break;

            }
        }

    }
}
