using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System.Linq;

public class IntersectTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<int> a3 = new List<int>();
        List<int> a5 = new List<int>();

        for (int i = 1; i <= 1000; i++)
        {
            if (i % 3 == 0)
            {
                a3.Add(i);
            }
            if (i % 5 == 0)
            {
                a5.Add(i);
            }
        }


        Profiler.BeginSample("intersect linq");
        List<int> a15 = a3.Intersect(a5).ToList();
        Profiler.EndSample();

        foreach (var e in a15)
        {
            Debug.Log(e);
        }

        Profiler.BeginSample("intersect");
        List<int> b15 = new List<int>();
        foreach (var e in a3)
        {
            if (a5.Contains(e))
            {
                b15.Add(e);
            }
        }
        Profiler.EndSample();

        foreach (var e in b15)
        {
            Debug.Log(e);
        }

        Profiler.BeginSample("intersect list");
        int s = 0;
        for (int i = 0; i < a3.Count; i++)
        {
            if (a5.Contains(a3[i]))
            {
                if (s == 0)
                {
                    s = i;
                }
                a5.Add(a3[i]);
            }
        }
        a5.RemoveRange(s, a5.Count - s);
        Profiler.EndSample();

        foreach (var e in b15)
        {
            Debug.Log(e);
        }
    }
}
