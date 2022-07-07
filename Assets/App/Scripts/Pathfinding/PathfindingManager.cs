using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//reference : https://github.com/davecusatis/A-Star-Sharp/blob/master/Astar.cs

public class PathfindingManager
{
    static PathfindingManager me;
    [SerializeField] float maxNeighbourDistance = 1f;
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

        pn.gCost = new int[PathFinderManager.Me().NumberOfThreads];
        pn.hCost = new int[PathFinderManager.Me().NumberOfThreads];

        pn.neighbours = new List<PathfindingNode>();
        pn.parents = new PathfindingNode[PathFinderManager.Me().NumberOfThreads];

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
        GetNodeNeighbours();
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

        g.name = "NODE " + pn.Position + "|";

        for (int x = 0; x < pn.neighbours.Count; x++)
        {
            g.name += pn.neighbours[x].Position + "|";
        }
    }

    List<PathfindingNode> GetNodes(int x, int y)
    {
        List<PathfindingNode> retVal = new List<PathfindingNode>();

        if (!allNodes.ContainsKey(x))
        {
            return retVal;
        }

        if (!allNodes[x].ContainsKey(y))
        {
            return retVal;
        }

        retVal = allNodes[x][y].GetNodes();

        return retVal;
    }

    void GetNodeNeighbours()
    {
        foreach (KeyValuePair<int, Dictionary<int, NodeList>> kvp in allNodes)
        {
            foreach (KeyValuePair<int, NodeList> vals in kvp.Value)
            {
                for (int x = 0; x < vals.Value.GetNodes().Count; x++)
                {
                    vals.Value.GetNodes()[x].neighbours = GetNeighboursOfNodes(vals.Value.GetNodes()[x]);
                }
            }
        }
    }

    List<PathfindingNode> GetNeighboursOfNodes(PathfindingNode pn)
    {
        List<PathfindingNode> retVal = new List<PathfindingNode>();

        int xP = Mathf.RoundToInt(pn.Position.x);
        int yP = Mathf.RoundToInt(pn.Position.y);

        for (int x = xP - 1; x < xP + 2; x++)
        {
            for (int y = yP - 1; y < yP + 2; y++)
            {
                List<PathfindingNode> potentialNeighbours = GetNodes(x, y);

                for (int z = 0; z < potentialNeighbours.Count; z++)
                {
                    float d = Vector2.Distance(potentialNeighbours[z].Position, pn.Position);

                    if (d < maxNeighbourDistance && pn.Position != potentialNeighbours[z].Position)
                    {
                        retVal.Add(potentialNeighbours[z]);
                    }
                }
            }
        }

        return retVal;
    }

    public PathfindingNode GetNearestNodeToPosition(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);

        List<PathfindingNode> potential = GetNodes(x, y);

        PathfindingNode retVal = null;
        float dist = 999999f;

        for (int z = 0; z < potential.Count; z++)
        {
            float d2 = Vector2.Distance(potential[z].Position, pos);

            if (d2 < dist)
            {
                dist = d2;
                retVal = potential[z];
            }
        }

        return retVal;
    }
}

public class PathfindingNode
{
    public Vector3 Position;
    public List<PathfindingNode> neighbours;
    public int[] gCost, hCost;
    public int weight;
    public PathfindingNode[] parents;
    public bool walkable;

    public int GetFCost(int threadID)
    {
        return gCost[threadID] + hCost[threadID];
    }
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