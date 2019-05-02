using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DragonMovement : MonoBehaviour {

    public GameObject targetTrigger;
    NavMeshAgent dragonAgent;
    Animator animator;
    RaycastHit target;
    public Material materialD;

    RaycastHit hit;
    bool isMoving, isRunning;

    private void Start()
    {
        dragonAgent = this.GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = Manager.target;
        dragonAgent.destination = target.point;
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Terrain")
                {
                    Destroy(GameObject.FindWithTag("Target"));
                    isMoving = true;
                    dragonAgent.destination = hit.point;
                    if (!CameraMovement.isCameraMoving)
                    {
                        Instantiate(targetTrigger, hit.point, Quaternion.identity);
                    }
                }
            }
        }

        if (!dragonAgent.pathPending)
        {
            if (dragonAgent.remainingDistance <= dragonAgent.stoppingDistance)
            {
                if (!dragonAgent.hasPath || dragonAgent.velocity.sqrMagnitude == 0f)
                {
                    isMoving = false;
                    isRunning = false;
                }
            }
        }
        if (!isRunning)
        {
            if (isMoving)
            {
                animator.SetTrigger("Walk");
            }
            else
            {
                animator.SetTrigger("Idle");
            }
        }

        if (dragonAgent.currentOffMeshLinkData.valid)
        {
            animator.SetTrigger("Jump");
            dragonAgent.transform.localPosition += Vector3.up;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            animator.SetTrigger("Walk");
        }

        DrawLine();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Target")
        {
            isRunning = true;
            animator.SetTrigger("Run");
            Destroy(other.gameObject);
        }
    }
    void DrawLine()
    {
        if (dragonAgent == null || dragonAgent.path == null)
            return;

        var lineRenderer = this.GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = this.gameObject.AddComponent<LineRenderer>();
            lineRenderer.sortingOrder = 10;
            lineRenderer.material = materialD;
            lineRenderer.startWidth = 0.2f;
            lineRenderer.endWidth = 0.2f;
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.yellow;
        }

        var path = dragonAgent.path;

        lineRenderer.positionCount = path.corners.Length;

        for (int i = 0; i < path.corners.Length; i++)
        {
            lineRenderer.SetPosition(i, path.corners[i]);
        }

    }
}