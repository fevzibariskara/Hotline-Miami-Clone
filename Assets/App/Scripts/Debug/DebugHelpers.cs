using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHelpers : MonoBehaviour
{
    [SerializeField] GameObject test;
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
    }
}
