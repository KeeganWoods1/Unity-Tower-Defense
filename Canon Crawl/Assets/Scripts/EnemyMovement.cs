using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Pathfinder pathfinder;

    // Start is called before the first frame update
    void Start()
    {
        pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPathList();
        StartCoroutine(FollowPath(path));
    }

    // Update is called once per frame
    void Update()
    {
        
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
