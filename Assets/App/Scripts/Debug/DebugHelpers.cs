using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHelpers : MonoBehaviour
{
    [SerializeField] GameObject test, test2;
    PathFinder pf;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PathfindingManager.Me().GenerateDebugDisplay();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PathfindingNode nearest = PathfindingManager.Me().GetNearestNodeToPosition(test.transform.position);

            if (nearest == null)
            {
                Debug.LogError("COULD NOT GET NODE AT POSITION " + test.transform.position);
            }
            else
            {
                GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                g.transform.position = nearest.Position;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PathFinder pf = new PathFinder();
            pf.GetPath(PathfindingManager.Me().GetNearestNodeToPosition(test.transform.position), 
                PathfindingManager.Me().GetNearestNodeToPosition(test2.transform.position));

            if (pf.path.Count == 0)
            {
                Debug.LogError("COULD NOT GET PATH");
            }
            else
            {
                for (int x = 0; x < pf.path.Count; x++)
                {
                    GameObject g = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    g.transform.position = pf.path[x].Position;

                    g.name = "PATH NODE " + x;
                }
            }
        }
    }
}
