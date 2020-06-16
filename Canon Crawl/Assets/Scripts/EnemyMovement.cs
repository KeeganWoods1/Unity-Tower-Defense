using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Pathfinder pathfinder;
    List<Waypoint> path; 
    [Range(0,10f)][SerializeField] float movementSpeed;
    [SerializeField] ParticleSystem selfDestructFX;
    EnemyHealth enemy;
    int pathIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        path = pathfinder.GetPathList();
        enemy = gameObject.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        SmoothEnemyMovement();
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
        float xDiff = nextWaypointPos.x - currentPos.x;
        float zDiff = nextWaypointPos.z - currentPos.z;

        Vector3 direction = nextWaypointPos - currentPos;
        direction.Normalize();

        float distance = Vector3.Distance(currentPos, nextWaypointPos);
        float posTolerance = 0.5f; // Acceptable distance range for a unit to be considered 'centered' on the waypoint   

        if (distance <= posTolerance)
        {
            if (pathIndex < path.Count -1)
            {
                pathIndex++;
            }

            else
            {
                enemy.DeathSequence(selfDestructFX);
            }            
        }

        else
        {
            transform.LookAt(nextWaypointPos);

            float xOffset = direction.x * movementSpeed * Time.deltaTime;
            float zOffset = direction.z * movementSpeed * Time.deltaTime;

            transform.position = new Vector3(currentPos.x + xOffset, currentPos.y, currentPos.z + zOffset);
        }
    }

    //For discrete movement 1 block/second movement
    IEnumerator FollowPath(List<Waypoint> path) 
    {
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
