using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reference : https://github.com/davecusatis/A-Star-Sharp/blob/master/Astar.cs

public class PathfindingManager
{
    static PathfindingManager me;
    [SerializeField] int NumberOfThreads = 3;
    Dictionary<int, Dictionary<int, NodeList>> allNodes;
    GameObject DebugParent;

    public static PathfindingManager Me()
    {
        if (me == null)
        {
            me = new PathfindingManager();
            me.Init();
        }
        return me;
    }


    public void Init()
    {
        allNodes = new Dictionary<int, Dictionary<int, NodeList>>();
        GenerateGrid();
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

        AddToAllNodes(coordPos.x, coordPos.y, pn);
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
        for (float x = -50f; x < 0f; x += .5f)
        {
            for (float y = -50f; y < 0f; y += .5f)
            {
                GenerateNode(new Vector3(x, y, 0));
            }
        }
    }

    public void GenerateDebugDisplay()
    {
        if (DebugParent != null)
        {
            GameObject.Destroy(DebugParent);
        }
        DebugParent = new GameObject();

        foreach (KeyValuePair<int, Dictionary<int, NodeList>> kvp in allNodes)
        {
            foreach (KeyValuePair<int, NodeList> vals in kvp.Value)
            {
                for (int x = 0; x < vals.Value.GetNodes().Count; x++)
                {
                    GenerateDebugCube(vals.Value.GetNodes()[x]);
                }
            }
        }
    }

    void GenerateDebugCube(PathfindingNode pn)
    {
        GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cube);

        g.transform.position = pn.Position;
        g.transform.localScale = new Vector3(.25f, .25f, .25f);
        g.transform.parent = DebugParent.transform;

        g.name = "NODE " + pn.Position;
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
    List<PathfindingNode> nodes;
    int yCoordinate;

    public NodeList(int yCoord)
    {
        nodes = new List<PathfindingNode>();
    }

    public void AddNode(PathfindingNode pn)
    {
        nodes.Add(pn);
    }

    public void RemoveNode(PathfindingNode pn)
    {
        if (nodes.Contains(pn))
        {
            nodes.Remove(pn);
        }
    }

    public List<PathfindingNode> GetNodes()
    {
        return nodes;
    }

}