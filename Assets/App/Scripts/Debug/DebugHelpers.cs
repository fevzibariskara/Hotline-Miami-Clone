using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHelpers : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PathfindingManager.Me().GenerateDebugDisplay();
        }
    }
}
