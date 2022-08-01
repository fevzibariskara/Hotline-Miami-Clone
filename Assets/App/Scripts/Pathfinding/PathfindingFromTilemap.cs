using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingFromTilemap : MonoBehaviour
{
    [SerializeField] Tilemap unwalkableAreas;
    [SerializeField] Vector2Int bottomLeftCorner, topRightCorner;

    static PathfindingFromTilemap me;

    public static PathfindingFromTilemap Me()
    {
        if (me == null)
        {
            me = FindObjectOfType<PathfindingFromTilemap>();
        }
        return me;
    }

    private void Start()
    {
        GenerateNodes();
    }

    void GenerateNodes()
    {
        Vector3Int pos = Vector3Int.zero;
        for (int x = bottomLeftCorner.x; x <= topRightCorner.x; x++)
        {
            for (int y = bottomLeftCorner.y; y <= topRightCorner.y; y++)
            {
                pos = new Vector3Int(x, y, 0);
                if (unwalkableAreas.GetTile(pos) != null)
                {
                    PathfindingManager.Me().GenerateNode(new Vector3(x, y, 0), false);
                }
                else
                {
                    PathfindingManager.Me().GenerateNode(new Vector3(x, y, 0), true);
                }
            }
        }
        PathfindingManager.Me().GetNodeNeighbours();
        PathfindingManager.Me().GenerateDebugDisplay();
    }
}
