using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder
{
    public List<PathfindingNode> path = new List<PathfindingNode>();

    public void GetPath(PathfindingNode start, PathfindingNode end)
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
                if (openSet[i].fCost <= current.fCost)
                {
                    if (openSet[i].hCost < current.hCost)
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

                int newCostNeighbour = current.gCost + GetDist(current, current.neighbours[x]) + current.weight;

                if (newCostNeighbour < current.neighbours[x].gCost || !openSet.Contains(current.neighbours[x]))
                {
                    current.neighbours[x].gCost = newCostNeighbour;
                    current.neighbours[x].hCost = GetDist(current.neighbours[x], end);
                    current.neighbours[x].parents[0] = current;
                    
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
                current = current.parents[0];
            }
            path.Reverse();
        }
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
