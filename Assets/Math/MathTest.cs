using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class MathTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 21.51ms
        {
            Profiler.BeginSample("Mathf.CeilToInt");
            int a = 10;
            int b = 7;
            int r = 0;
            for (int i = 0; i < 1000000; i++)
            {
                r = Mathf.CeilToInt((float)a / b);
            }
            Profiler.EndSample();
            Debug.Log(r);
        }

        // 4.18ms
        {
            Profiler.BeginSample("Ceil");
            int a = 10;
            int b = 7;
            int r = 0;
            for (int i = 0; i < 1000000; i++)
            {
                r = (a + b - 1) / b;
            }
            Profiler.EndSample();
            Debug.Log(r);
        }
    }
}
