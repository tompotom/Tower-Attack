using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPathFindingCustom : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Transform target;
    [SerializeField] private Transform waypointParent;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Customisable Values")]
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDist = 3f;
    [SerializeField] private float timeForNextRay = 0.05f;
    [SerializeField] private bool isAutoPathFinding = false;

    [Header("Prefabs & List")]
    [SerializeField] private GameObject wayPoint;
    [SerializeField] List<GameObject> wayPoints;

    [SerializeField] LayerMask layer;

    // Components
    Path path;
    Seeker seeker;
    Rigidbody2D rb;
    Vector3 temporaryWaypointValues;
    
    // Values
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    private float timer = 0f;
    private int wayIndex;
    private bool isPressingStart = false;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {


        LineRendererSettings();

        // Check where is the target and draw a path.
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void LineRendererSettings()
    {
        wayIndex = 1;
        
        lineRenderer.enabled = false;
        lineRenderer.sortingOrder = 1;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.material.color = Color.white;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPressingStart = true;
        }
    }

    private void FixedUpdate()
    {
        if (isAutoPathFinding)
        {
            AIPath();
        }
        else
        {
            DrawPath();
        }
    }

    void OnPathComplete(Path p)
    {
        // Reset the path if a error happen
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void AIPath()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 targetPos = new Vector2(target.position.x, target.position.y);
        var force = speed * Time.deltaTime;
        var newPosition = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], force);

        Debug.Log(path.vectorPath[currentWaypoint]);

        rb.MovePosition(newPosition);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDist)
        {
            currentWaypoint++;
        }
    }

    void DrawPath()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer > timeForNextRay)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.enabled = true;
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
            mouseWorldPosition.z = 0f;

            Ray ray = Camera.main.ScreenPointToRay(worldPoint);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            Debug.DrawRay(ray.origin, ray.direction * 20);

            if (hit.collider.CompareTag("Background"))
            {
                return; 
            }
            else if (hit.collider.CompareTag("Path"))
            {
                if (mouseWorldPosition == temporaryWaypointValues)
                {
                    return;
                }

                temporaryWaypointValues = mouseWorldPosition;

                GameObject newWaypoint = Instantiate(wayPoint, mouseWorldPosition, Quaternion.identity, waypointParent);

                wayPoints.Add(newWaypoint);

                lineRenderer.positionCount = wayIndex + 1;
                lineRenderer.SetPosition(wayIndex, newWaypoint.transform.position);

                timer = 0;

                wayIndex++;
            }
            //Debug.DrawLine(Camera.main.transform.position, mouseWorldPosition, Color.green, 1f);   
        }

        if (isPressingStart)
        {
            var currentPos = wayPoints[currentWaypoint].transform.position;
            var speedOnDeltaTime = speed * Time.deltaTime;
            var newPosition = Vector3.MoveTowards(transform.position, currentPos, speedOnDeltaTime);

            rb.MovePosition(newPosition);

            if (transform.position == newPosition)
            {
                Destroy(wayPoints[currentWaypoint]);
                
                currentWaypoint++;
            }

            if (currentWaypoint == wayPoints.Count)
            {
                ClearPath();
            }
        }

    }

    //void DrawPathOffscreen()
    //{
    //    if (!waypointOffscreen.IsOffScreen)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        Debug.Log("aaaa");
    //        if (currentWaypoint == wayPoints.Count)
    //        {
    //            Destroy(wayPoints[currentWaypoint]);
    //        }
    //    }
    //}

    void ClearPath()
    {
        foreach (var item in wayPoints) Destroy(item);
        wayPoints.Clear();
        wayIndex = 1;
        currentWaypoint = 0;

        ClearLineRenderer();
        isPressingStart = false;
    }

    void ClearLineRenderer()
    {
        lineRenderer.positionCount = 1;
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Target")
        {
            Debug.Log("WIN !");
        }
    }
}
