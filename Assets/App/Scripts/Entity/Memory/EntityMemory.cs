using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMemory : MonoBehaviour
{
    public Dictionary<string, object> Memory;

    public void AddMemory(string key, object o)
    {
        if (Memory == null)
        {
            Memory = new Dictionary<string, object>();
        }

        if (Memory.ContainsKey(key))
        {
            Memory[key] = o;
        }
        else
        {
            Memory.Add(key, o);
        }
    }

    public object GetMemory(string key)
    {
        if (Memory == null)
        {
            return null;
        }

        if (Memory.ContainsKey(key))
        {
            return Memory[key];
        }
        else
        {
            return null;
        }
    }

    public void RemoveMemory(string key)
    {
        if (Memory == null)
        {
            return;
        }

        if (Memory.ContainsKey(key))
        {
            Memory.Remove(key);
        }
    }
}
