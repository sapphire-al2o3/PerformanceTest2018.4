using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class PrefabTest : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    void Start()
    {
        // 152byte
        {
            Profiler.BeginSample("prefab Instantiate");
            var go = GameObject.Instantiate(prefab);
            Profiler.EndSample();
        }
    }
}
