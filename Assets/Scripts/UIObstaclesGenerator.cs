using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UIObstaclesGenerator : MonoBehaviour {

    public GameObject shortOnstacle, longObstacle, offMeshLinks;
    GameObject obstacleObject;
    public static bool hited = false;
    public static RaycastHit hit;
    bool isPressed = true;
    
    public void InstantiateObstacle(string obstacleType)
    {
        if (isPressed)
        {
            StartCoroutine("IEnumeratorInstantiateObstacle", obstacleType);
            isPressed = false;
        }
    }

    public IEnumerator IEnumeratorInstantiateObstacle(string objType)
    {
        switch (objType)
        {
            case "Short": obstacleObject = shortOnstacle; break;
            case "Long": obstacleObject = longObstacle; break;
            default:
                break;
        }
        hited = false;
        yield return new WaitWhile(() => hited == false);
        Instantiate(obstacleObject, hit.point, Quaternion.identity);
            
        isPressed = true;
    }
}
