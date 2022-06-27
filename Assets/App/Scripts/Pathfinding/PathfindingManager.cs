using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager
{
    static PathfindingManager me;

    public static PathfindingManager Me()
    {
        if (me == null)
        {
            me = new PathfindingManager();
        }
        return me;
    }

    [SerializeField] int NumberOfThreads = 3;
    Dictionary<int, Dictionary<int,NodeList>> allNodes;

    public void Init()
    {
        allNodes = new Dictionary<int, Dictionary<int, NodeList>>();
    }

    void GenerateNode(Vector3 position)
    {
        PathfindingNode pn = new PathfindingNode();
        pn.Position = position;
        Vector3Int coordPos = new Vector3Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.y), Mathf.RoundToInt(position.z));

        pn.walkable = true;
        pn.weight = 1;

        pn.neighbours = new List<PathfindingNode>();
        pn.parents = new PathfindingNode[NumberOfThreads];
    }

    void AddToAllNodes(int x, int y, PathfindingNode pn)
    {
        if (!allNodes.ContainsKey(x))
        {
            allNodes.Add(x, new Dictionary<int, NodeList>());
        }

        if (!allNodes[x].ContainsKey(y))
        {
            allNodes[x].Add(y, new NodeList(y));
        }
        allNodes[x][y].AddNode(pn);
    }

    void GenerateGrid()
    {

    }
}

public class PathfindingNode
{
    public Vector3 Position;
    public List<PathfindingNode> neighbours;
    public float gCost, fCost, weight;
    public PathfindingNode[] parents;
    public bool walkable;
}

class NodeList
{
    Dictionary<int, List<PathfindingNode>> nodes;
    int yCoordinate;

    public NodeList(int yCoord)
    {
        nodes = new Dictionary<int, List<PathfindingNode>>();
        nodes.Add(yCoord, new List<PathfindingNode>());
        yCoordinate = yCoord;
    }

    public void AddNode(PathfindingNode pn)
    {
        nodes[yCoordinate].Add(pn);
    }

    public void RemoveNode(PathfindingNode pn)
    {
        if (nodes[yCoordinate].Contains(pn))
        {
            nodes[yCoordinate].Remove(pn);
        }
    }

}