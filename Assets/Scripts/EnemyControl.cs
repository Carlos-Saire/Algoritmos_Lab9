using System.Collections;
using UnityEngine;
public class EnemyControl : MonoBehaviour
{
    private Vector2 positionToMove; 
    public float speedMove;
    public float energy;
    private float fullenergy;
    private bool confirms = true; 
    private bool playerDetected = false; 

    public Material VisionConeMaterial;
    public float VisionRange = 5f;
    public float VisionAngle = 90f;
    public LayerMask VisionObstructingLayer;
    public int VisionConeResolution = 120;

    private LineRenderer lineRenderer;

    private NodeControl currentNode; 

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        fullenergy = energy;

        lineRenderer.material = VisionConeMaterial;
        lineRenderer.positionCount = VisionConeResolution + 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.useWorldSpace = true;

        SetInitialNode();
    }

    private void Update()
    {
        DrawVisionCone();

        if (confirms)
        {
            if (playerDetected)
            {
                transform.position = Vector2.MoveTowards(transform.position, positionToMove, speedMove * Time.deltaTime);
            }
            else
            {
                MoveAlongPath();
            }
        }
    }

    private void SetInitialNode()
    {
        currentNode = FindObjectOfType<NodeControl>();
        if (currentNode != null)
        {
            positionToMove = currentNode.transform.position; 
        }
    }

    private void MoveAlongPath()
    {
        if (Vector2.Distance(transform.position, positionToMove) < 0.1f) 
        {
            NodeControl nextNode = currentNode.GetAdjacentNode(); 
            if (nextNode != null)
            {
                positionToMove = nextNode.transform.position;
                currentNode = nextNode;
            }
            else
            {
                
                
                SetInitialNode(); 
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, positionToMove, speedMove * Time.deltaTime);
        }
    }

    public void SetNewPosition(Vector2 newPosition)
    {
        positionToMove = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Node"))
        {
            SetNewPosition(collision.GetComponent<NodeControl>().GetAdjacentNode().transform.position);
            energy -= collision.GetComponent<NodeControl>().energy;
            if (energy <= 0)
            {
                confirms = false;
                StartCoroutine(RestaurarVida());
            }
        }
    }

    private void DrawVisionCone()
    {
        Vector2 direction = (positionToMove - (Vector2)transform.position).normalized;
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            float angleStep = VisionAngle / VisionConeResolution;
            float angleOffset = -VisionAngle / 2;
            Vector3[] conePoints = new Vector3[VisionConeResolution + 2];

            conePoints[0] = transform.position;

            playerDetected = false; 

            for (int i = 0; i <= VisionConeResolution; i++)
            {
                float rad = Mathf.Deg2Rad * (angle + angleOffset);
                Vector2 coneDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

                RaycastHit2D hit = Physics2D.Raycast(transform.position, coneDirection, VisionRange, VisionObstructingLayer);

                if (hit.collider != null)
                {
                    conePoints[i + 1] = hit.point;
                    if (hit.collider.CompareTag("Player"))
                    {
                        positionToMove = hit.collider.transform.position;
                        playerDetected = true; 
                    }
                }
                else
                {
                    conePoints[i + 1] = (Vector2)transform.position + coneDirection * VisionRange;
                }

                angleOffset += angleStep;
            }

            lineRenderer.SetPositions(conePoints);
        }
    }

    private IEnumerator RestaurarVida()
    {
        while (energy < fullenergy)
        {
            energy += fullenergy * 0.1f;
            yield return new WaitForSeconds(1f);
        }
        confirms = true;
    }
}
