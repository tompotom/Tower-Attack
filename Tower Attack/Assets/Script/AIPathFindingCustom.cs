using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPathFindingCustom : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private Transform target;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Customisable Values")]
    [SerializeField] private float speed = 200f;
    [SerializeField] private float nextWaypointDist = 3f;
    [SerializeField] private float timeForNextRay = 0.05f;

    [Header("Prefabs & List")]
    [SerializeField] private GameObject wayPoint;
    [SerializeField] List<GameObject> wayPoints;

    // Components
    Path path;
    Seeker seeker;
    Rigidbody2D rb;
    
    // Values
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    private float timer = 0f;
    private int currenWayPoint = 0;
    private int wayIndex;
    private bool move;
    private bool touchStartedOnPlayer;
    private bool pressStart = false;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        lineRenderer.enabled = false;
        wayIndex = 1;
        move = false;
        touchStartedOnPlayer = false;

        // Check where is the target and draw a path.
        //seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void Update()
    {
        DrawPath();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            pressStart = true;
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

    public void OnMouseDown()
    {
        lineRenderer.enabled = true;
        touchStartedOnPlayer = true;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    void DrawPath()
    {
        timer += Time.deltaTime;

        if (Input.GetMouseButton(0) && timer > timeForNextRay)
        {
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.z = Mathf.Abs(Camera.main.transform.position.z);
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(worldPoint);
            mouseWorldPosition.z = 0f;

            //RaycastHit2D hit = Physics2D.Raycast(Camera.main.transform.position, mouseWorldPosition, 100f);

            GameObject newWaypoint = Instantiate(wayPoint, mouseWorldPosition, Quaternion.identity);

            wayPoints.Add(newWaypoint);

            lineRenderer.positionCount = wayIndex + 1;
            lineRenderer.SetPosition(wayIndex, newWaypoint.transform.position);

            timer = 0;

            wayIndex++;

            Debug.DrawLine(Camera.main.transform.position, mouseWorldPosition, Color.green, 1f);
        }

        if (pressStart)
        {
            var currentPos = wayPoints[currentWaypoint].transform.position;
            var speedOnDeltaTime = speed * Time.deltaTime;
            var newPosition = Vector3.MoveTowards(transform.position, currentPos, speedOnDeltaTime);

            rb.MovePosition(newPosition);
            

            //transform.Translate(newPos * speedOnDeltaTime);

            if (transform.position == newPosition)
            {
                currentWaypoint++;
            }

            if (currentWaypoint == wayPoints.Count)
            {
                ClearPath();
            }
        }
    }

    void ClearPath()
    {
        move = false;

        foreach (var item in wayPoints) Destroy(item);

        wayPoints.Clear();

        wayIndex = 1;
        currentWaypoint = 0;

        pressStart = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collision"))
        {
            Debug.Log("touché");
        }
    }
}
