using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PathFinderManager
{
    static PathFinderManager me;
    public int NumberOfThreads = 3;

    Thread[] threads;

    List<int> threadIDs;
    List<PathFinder> findersWaiting;

    public static PathFinderManager Me()
    {
        if (me == null)
        {
            me = new PathFinderManager();
            me.Init();
        }
        return me;
    }

    void Init()
    {
        threadIDs = new List<int>();
        threads = new Thread[NumberOfThreads];

        for (int x = 0; x < NumberOfThreads; x++)
        {
            threadIDs.Add(x);
        }
        findersWaiting = new List<PathFinder>();
    }

    public void RequestThread(PathFinder pf)
    {
        if (threadIDs.Count > 0)
        {
            pf.SetThreadID(threadIDs[0]);
            threadIDs.RemoveAt(0);
            ThreadStart t = new ThreadStart(pf.OnThreadAvailable);
            threads[pf.GetID()] = new Thread(t);
            threads[pf.GetID()].Start(); 
        }
        else
        {
            AddToFindersWaiting(pf);
        }
    }

    void AddToFindersWaiting(PathFinder pf)
    {
        if (findersWaiting == null)
        {
            findersWaiting = new List<PathFinder>();
        }
        if (!findersWaiting.Contains(pf))
        {
            findersWaiting.Add(pf);
        }
    }

    public void OnPathFinish(PathFinder pf)
    {
        threadIDs.Add(pf.GetID());
        //threads[pf.GetID()].Abort();

        pf.SetThreadID(-1);

        if (findersWaiting.Count > 0)
        {
            PathFinder pf2 = findersWaiting[0];
            findersWaiting.RemoveAt(0);
            RequestThread(pf2);
        }
    }
}
