using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class YieldTest : MonoBehaviour
{
    IEnumerator Test()
    {
        int i = 0;

        while (true)
        {
            {
                Matrix4x4 a = Matrix4x4.identity;
                Matrix4x4 b = Matrix4x4.identity;
                Matrix4x4 c = Matrix4x4.identity;
                Matrix4x4 d = Matrix4x4.identity;
                Matrix4x4 e = Matrix4x4.identity;

                i += 1;
            }
            yield return null;
        }
    }

    IEnumerator Test0()
    {
        while (true)
        {
            yield return null;
        }
    }

    IEnumerator Test1()
    {
        while (true)
        {
            yield return 0;
        }
    }

    int[] array = new int[3];
    IEnumerator Test2()
    {
        int count = array.Length;
        int s = 0;
        for (int i = 0; i < count; i++)
        {
            int a = array[i];
            s += a;
            yield return null;
        }
    }

    IEnumerator Test3()
    {
        int s = 0;
        for (int i = 0; i < array.Length; i++)
        {
            s += array[i];
            yield return null;
        }
    }

    IEnumerator Test4()
    {
        int s = 0;
        foreach (var a in array)
        {
            s += a;
            yield return null;
        }
    }

    void Start ()
    {
        Profiler.BeginSample("YieldTest");
        StartCoroutine(Test());
        Profiler.EndSample();

        StartCoroutine(Test0());
        //StartCoroutine(Test1());

        // 80byte
        {
            Profiler.BeginSample("IEnumerator size 0");
            StartCoroutine(Test2());
            Profiler.EndSample();
        }

        // 72byte
        {
            Profiler.BeginSample("IEnumerator size 1");
            StartCoroutine(Test3());
            Profiler.EndSample();
        }

        // 80byte
        {
            Profiler.BeginSample("IEnumerator size 2");
            StartCoroutine(Test4());
            Profiler.EndSample();
        }
    }
}
