using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public static RaycastHit target;
    public GameObject dragonPrefab, targetTriggerPrefab, panel, button;
    GameObject dragon, targetTrigger;
    GameObject cameraObject;
    int hitCount = 0;
    int maxOfHits = 2;
    // Use this for initialization
    void Start () {
        dragon = dragonPrefab;
        targetTrigger = targetTriggerPrefab;
        cameraObject = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Terrain")
                    {
                        UIObstaclesGenerator.hit = hit;
                        UIObstaclesGenerator.hited = true;
                        if (hitCount < maxOfHits)
                        {
                            if (hitCount == 0)
                            {
                                target = hit;
                                Instantiate(targetTrigger, target.point, Quaternion.identity);
                                hitCount++;
                            }
                            else
                            {
                                Instantiate(dragon, hit.point, Quaternion.identity);
                                hitCount++;
                            }
                        }
                    }
                    if (hit.collider.tag == "Obstacle")
                        DragObstacle.thisGameObject = hit.transform.gameObject;
                }
            }
    }

    public void ShowPanel()
    {
        maxOfHits = 0;
        cameraObject.transform.eulerAngles = new Vector3(90, 0, 0);
        cameraObject.transform.position = new Vector3(0, 50, 0);
        Camera.main.fieldOfView = 60;
        panel.SetActive(true);
        button.SetActive(false);
        EnableDragonAndTarget(false);
        EnableCameraScript(false);
        DragObstacle.enabledObstaclePositioning = true;
    }

    public void HidePanel()
    {
        maxOfHits = 2;
        panel.SetActive(false);
        button.SetActive(true);
        EnableCameraScript(true);
        EnableDragonAndTarget(true);
        DragObstacle.enabledObstaclePositioning = false;
        UIObstaclesGenerator.hited = false;
    }

    void EnableDragonAndTarget(bool enabled)
    {
        if (enabled != true)
        {
            dragon = GameObject.FindWithTag("Player");
            targetTrigger = GameObject.FindWithTag("Target");
        }
        if (dragon!=null)
        {
            dragon.GetComponent<DragonMovement>().enabled = enabled;
        }
        else
        {
            dragon = dragonPrefab;
        }

        if (targetTrigger != null)
        {
            //targetTrigger.SetActive(enabled);
        }
        else
        {
            targetTrigger = targetTriggerPrefab;
        }
    }

    void EnableCameraScript(bool enabled)
    {
        cameraObject.GetComponent<CameraMovement>().enabled = enabled;
    }
}
