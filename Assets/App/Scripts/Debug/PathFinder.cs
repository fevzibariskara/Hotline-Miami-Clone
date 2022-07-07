using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder
{
    PathfindingNode startNode, endNode;

    public bool isPathDone = false;
    int threadID = -1;

    public List<PathfindingNode> path = new List<PathfindingNode>();

    public void GetMultiThreadedPath(PathfindingNode start, PathfindingNode end)
    {
        if (start == null || end == null)
        {
            return;
        }
        isPathDone = false;

        startNode = start;
        endNode = end;

        Debug.Log("REQUESTING THREAD FOR PATH");

        PathFinderManager.Me().RequestThread(this);
    }

    public void SetThreadID(int i)
    {
        Debug.Log("SET ID TO " + i);
        threadID = i;
    }

    public int GetID()
    {
        return threadID;
    }

    public void OnThreadAvailable()
    {
        GetPath(startNode, endNode);
    }

    void GetPath(PathfindingNode start, PathfindingNode end)
    {
        path = new List<PathfindingNode>();

        HashSet<PathfindingNode> closedSet = new HashSet<PathfindingNode>();
        List<PathfindingNode> openSet = new List<PathfindingNode>();
        PathfindingNode current = null;

        openSet.Add(start);

        while (openSet.Count > 0)
        {
            current = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].GetFCost(threadID) <= current.GetFCost(threadID))
                {
                    if (openSet[i].hCost[threadID] < current.hCost[threadID])
                    {
                        current = openSet[i];
                    }
                }
            }
            openSet.Remove(current);
            closedSet.Add(current);

            if (current == end)
            {
                break;
            }

            for (int x = 0; x < current.neighbours.Count; x++)
            {
                if (current.neighbours[x].walkable == false | closedSet.Contains(current.neighbours[x]))
                {
                    continue;
                }

                int newCostNeighbour = current.gCost[threadID] + GetDist(current, current.neighbours[x]) + current.weight;

                if (newCostNeighbour < current.neighbours[x].gCost[threadID] || !openSet.Contains(current.neighbours[x]))
                {
                    current.neighbours[x].gCost[threadID] = newCostNeighbour;
                    current.neighbours[x].hCost[threadID] = GetDist(current.neighbours[x], end);
                    current.neighbours[x].parents[threadID] = current;
                    
                    if (!openSet.Contains(current.neighbours[x]))
                    {
                        openSet.Add(current.neighbours[x]);
                    }
                }
            }
        }

        if (current == end)
        {
            //successful path
            while (current != start)
            {
                path.Add(current);
                current = current.parents[threadID];
            }
            path.Reverse();
        }
        isPathDone = true;
        PathFinderManager.Me().OnPathFinish(this);
    }

    int GetDist(PathfindingNode pn1, PathfindingNode pn2)
    {
        int dstX = Mathf.RoundToInt(Mathf.Abs(pn1.Position.x - pn2.Position.x));
        int dstY = Mathf.RoundToInt(Mathf.Abs(pn1.Position.y - pn2.Position.y));

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
