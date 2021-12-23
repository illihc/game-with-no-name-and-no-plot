using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyPathAI : MonoBehaviour
{
    private Seeker seeker;
    public float Speed = 10f;
    [SerializeField] private Transform TargetT;
    public float PathUpdateFrequency = 0.5f;
    private Rigidbody2D rb;
    private int WayPointIndex = 0;
    Path path;
    private bool ReachedEndOfPath;
    [SerializeField] private float NextWaypointDistance = 0.2f;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InvokeRepeating("UpdatePath", 0f, PathUpdateFrequency);
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(transform.position, TargetT.position, OnPathComplete);
    }

    private void OnPathComplete(Path p)
    {
        if(p.error)
        {
            Debug.LogError(p.error);
            return;
        }

        path = p;
        WayPointIndex = 0;
    }

    private void FixedUpdate()
    {
        if (path == null)
            return;

        if (WayPointIndex >= path.vectorPath.Count)
        {
            ReachedEndOfPath = true;
            return;
        }
        else
            ReachedEndOfPath = false;

        //Calculating the direction of the player an moving
        Vector2 direction = ((Vector2)path.vectorPath[WayPointIndex] - rb.position).normalized;
        rb.AddForce(direction * Speed * Time.fixedDeltaTime);

        //Calculating the Distance to check if next waypoint is reached
        if (Vector2.Distance(path.vectorPath[WayPointIndex], rb.position) < NextWaypointDistance)
            WayPointIndex++;
    }
}
