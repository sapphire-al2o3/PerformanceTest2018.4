using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class InvokeTest : MonoBehaviour
{
    void Func()
    {

    }

    void Start()
    {
        // InvokeMethodOrCoroutineCheckedでGC Allocが発生する
        // 72byte
        {
            Profiler.BeginSample("Invoke");
            Invoke("Func", 0.0f);
            Profiler.EndSample();
        }
    }
}
