using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonFactory : MonoBehaviour
{
    [SerializeField] Canon canon;
    [SerializeField] int canonLimit = 5;
    [SerializeField] Transform canonParent;

    Queue<Canon> canonQueue = new Queue<Canon>();

    public void PlaceCanon(Waypoint waypoint)
    {
        if (canonQueue.Count < canonLimit)
        {
            InstantiateNewCanon(waypoint);
        }

        else
        {
            MoveExistingTower(waypoint);
        }

    }

    private void InstantiateNewCanon(Waypoint baseWaypoint)
    {
        var newCanon = Instantiate(canon, baseWaypoint.transform.position, Quaternion.identity);
        newCanon.transform.parent = canonParent;
        baseWaypoint.isPlaceable = false;

        newCanon.SetBaseWaypoint(baseWaypoint);
        canonQueue.Enqueue(newCanon);
    }

    private void MoveExistingTower(Waypoint newWaypoint)
    {
        
        var oldestCanon = canonQueue.Dequeue();
        var oldWaypoint = oldestCanon.GetBaseWaypoint();

        oldestCanon.transform.position = newWaypoint.transform.position;
        newWaypoint.isPlaceable = false;
        oldestCanon.SetBaseWaypoint(newWaypoint);
        oldWaypoint.isPlaceable = true;
        
        canonQueue.Enqueue(oldestCanon);
    }
}
