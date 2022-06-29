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

        while (openSet.Count > 0 && current != end)
        {
            current = openSet[0];
            openSet.RemoveAt(0);
            closedSet.Add(current);

            for (int x = 0; x < current.neighbours.Count; x++)
            {

                if (current.neighbours[x].walkable)
                {
                    current.neighbours[x].parents[0] = current;
                    current.neighbours[x].gCost = Mathf.Abs(current.neighbours[x].Position.x - end.Position.x)
                        + Mathf.Abs(current.neighbours[x].Position.y - end.Position.y);
                    current.neighbours[x].fCost = current.neighbours[x].weight + current.fCost;

                    openSet.Add(current.neighbours[x]);

                    openSet = openSet.OrderBy(node => node.fCost).ToList<PathfindingNode>();
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
        else
        {
            //no path
            path = null;
        }
    }
}
