using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Pathfinder pathfinder;
    List<Waypoint> path;
    [Range(0,0.2f)][SerializeField] float movementSpeed;
    int pathIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.GetPathList();
        StartCoroutine(FollowPath(path));
    }

    // Update is called once per frame
    void Update()
    {
        //SmoothEnemyMovement();
    }



    private void SmoothEnemyMovement()
    {

        if (pathIndex >= path.Count) { return; } //reached destination

        else
        {
            FollowPathSmoothly();

        }
    }

    private void FollowPathSmoothly()
    {
        Vector3 nextWaypointPos = path[pathIndex].transform.position;
        Vector3 currentPos = transform.position;
        Vector3 direction = nextWaypointPos - currentPos;

        float xDiff = nextWaypointPos.x - currentPos.x;
        float zDiff = nextWaypointPos.z - currentPos.z;
        float posTolerance = 0.001f;

        direction.Normalize();

        if (xDiff <= posTolerance && zDiff <= posTolerance)
        {
            pathIndex++;
        }

        else
        {
            float xOffset = direction.x * movementSpeed;
            float zOffset = direction.z * movementSpeed;

            transform.position = new Vector3(currentPos.x + xOffset, currentPos.y, currentPos.z + zOffset);
        }
    }

    IEnumerator FollowPath(List<Waypoint> path)
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
