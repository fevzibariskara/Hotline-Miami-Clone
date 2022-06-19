using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaTimeManager
{
    static float GameplayDeltaMultiplier = 1f;

    public static float GetGameplayDelta()
    {
        return Time.deltaTime * GameplayDeltaMultiplier;
    }

    public void SetGameplayDeltaMod(float val)
    {
        GameplayDeltaMultiplier = val;
        if (GameplayDeltaMultiplier < 0)
        {
            GameplayDeltaMultiplier = 0;
        }
    }
}
