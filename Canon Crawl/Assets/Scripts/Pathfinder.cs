﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2,Waypoint> grid = new Dictionary<Vector2, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Waypoint searchCenter;
    bool isRunning = true;


    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left,
    };

    private void BreadthFirstSearch(Waypoint endWaypoint)
    {
        queue.Enqueue(startWaypoint);
        
        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            HaltIfEndFound();
            ExploreNeighbours();
            searchCenter.isVisited = true;
        }
    }

    private void AddToPathList(Waypoint waypoint)
    {
        if (waypoint.Equals(startWaypoint))
        {
            path.Add(startWaypoint);
            waypoint.isPlaceable = false;

            path.Reverse();
            return;
        }

        else
        {
            
            path.Add(waypoint);
            waypoint.isPlaceable = false;

            AddToPathList(waypoint.visitedFrom);
        }
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();

        foreach (Waypoint waypoint in waypoints)
        {
            AddUniqueEntries(waypoint);
        }
    }

    private void AddUniqueEntries(Waypoint waypoint)
    {
        var gridPos = waypoint.GetGridPos();

        bool isOverlapping = grid.ContainsKey(gridPos);

        if (isOverlapping)
        {
            print("Overlapping block at key: " + gridPos + " skipping...");
        }

        else
        {
            grid.Add(gridPos, waypoint);
        }
    }

    private void HaltIfEndFound()
    {
        bool searchingFromEndWaypoint = searchCenter.Equals(endWaypoint);

        if (searchingFromEndWaypoint)
            {
                isRunning = false;
                return;
            }
    }

    private void ExploreNeighbours()
    {
        if (!isRunning) { return; }

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourBlockCoords = searchCenter.GetGridPos() + direction; 

            if(grid.ContainsKey(neighbourBlockCoords))
            {
                Waypoint neighbour = grid[neighbourBlockCoords];

                QueueNewNeighbours(neighbour);
            }    
        }
    }

    private void QueueNewNeighbours(Waypoint neighbour)
    {
        if (neighbour.isVisited || queue.Contains(neighbour))
        {
            //do nothing
        }

        else
        {
            queue.Enqueue(neighbour);
            neighbour.visitedFrom = searchCenter;
        }
    }

    public List<Waypoint> GetPathList()
    {
        if (path.Count == 0)
        {
            LoadBlocks();
            BreadthFirstSearch(endWaypoint);
            AddToPathList(endWaypoint);
        }

        return path;
    }
}
