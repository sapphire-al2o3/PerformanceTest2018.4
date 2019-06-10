using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class UnityAPITest : MonoBehaviour
{
    void Start()
    {
        // 46byte
        Profiler.BeginSample("Object.name");
        string s = gameObject.name;
        Profiler.EndSample();

        // 460byte
        Profiler.BeginSample("Object.name 10");
        for (int i = 0; i < 10; i++)
        {
            s = gameObject.name;
        }
        Profiler.EndSample();

        // 40byte
        Profiler.BeginSample("GameObject");
        var go = new GameObject();
        Profiler.EndSample();

        // 0.5KB
        Profiler.BeginSample("AddComponent");
        go.AddComponent<EmptyComponent>();
        Profiler.EndSample();

        // 0byte
        Profiler.BeginSample("GetComponent");
        var e = go.GetComponent<EmptyComponent>();
        Profiler.EndSample();

        // 32byte
        Profiler.BeginSample("GetComponents<Collider>");
        var rs = go.GetComponents<Collider>();
        Profiler.EndSample();

        // 0.6KB
        // 存在しないComponentを取得しようとするとメモリがとられる？
        Profiler.BeginSample("GetComponent<Rigidbody>");
        var r = go.GetComponent<Rigidbody>();
        Profiler.EndSample();

        // 120byte
        Profiler.BeginSample("GetComponents<Rigidbody>(List)");
        List<Rigidbody> rigidBodyList = new List<Rigidbody>();
        go.GetComponents<Rigidbody>(rigidBodyList);
        Profiler.EndSample();
    }
}
