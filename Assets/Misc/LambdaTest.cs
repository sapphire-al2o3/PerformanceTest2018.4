using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Profiling;

public class LambdaTest : MonoBehaviour
{

    int Func0(int x)
    {
        return x * 2;
    }

    int Func1(int x) => x * 2;

    Func<int, int> Func2 = x => x * 2;

    int Call(int x, Func<int, int> func)
    {
        return func(x);
    }

    void Start()
    {
        // 10.9KB
        {
            int k = 0;
            Profiler.BeginSample("function 0");
            for (int i = 0; i < 100; i++)
            {
                k += Call(i, Func0);
            }
            Profiler.EndSample();
            Debug.Log(k);
        }

        // 10.9KB
        {
            int k = 0;
            Profiler.BeginSample("function 1");
            for (int i = 0; i < 100; i++)
            {
                k += Call(i, Func1);
            }
            Profiler.EndSample();
            Debug.Log(k);
        }

        // 0B
        {
            int k = 0;
            Profiler.BeginSample("function 2");
            for (int i = 0; i < 100; i++)
            {
                k += Call(i, Func2);
            }
            Profiler.EndSample();
            Debug.Log(k);
        }

        // 112B
        {
            int k = 0;
            Profiler.BeginSample("function 3");
            for (int i = 0; i < 100; i++)
            {
                k += Call(i, x => x * 2);
            }
            Profiler.EndSample();
            Debug.Log(k);
        }

        // 11KB
        {
            int k = 0;
            Profiler.BeginSample("function 4");
            for (int i = 0; i < 100; i++)
            {
                k += Call(i, x => i * 2);
            }
            Profiler.EndSample();
            Debug.Log(k);
        }
    }
}
