﻿using System.Collections;
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

        // 0byte
        Profiler.BeginSample("GetInstanceID");
        int id = go.GetInstanceID();
        Profiler.EndSample();

        // 120byte
        Profiler.BeginSample("GetChild");
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
        }
        Profiler.EndSample();

        // 0byte
        Profiler.BeginSample("GetChild 2");
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
        }
        Profiler.EndSample();

        // 32byte
        Profiler.BeginSample("foreach transform");
        foreach (var child in transform)
        {
        }
        Profiler.EndSample();

        // 32byte
        Profiler.BeginSample("foreach transform 2");
        foreach (var child in transform)
        {
        }
        Profiler.EndSample();


        Transform c;
        Transform cc;
        // 40byte
        Profiler.BeginSample("Find");
        c = transform.Find("child0");
        cc = c.Find("child00");
        Profiler.EndSample();

        Debug.Assert(cc != null);

        // 40byte
        Profiler.BeginSample("Find 2");
        cc = transform.Find("child1/child10");
        Profiler.EndSample();

        Debug.Assert(cc != null);

        // 368byte
        {
            var animator = GetComponent<Animator>();
            int hash = 0;
            Profiler.BeginSample("Animator.parameters");
            foreach (var p in animator.parameters)
            {
                hash = p.nameHash;
            }
            Profiler.EndSample();
        }

        // GetParameterはparametersを内部で呼び出している
        // 2.9KB
        {
            var animator = GetComponent<Animator>();
            Profiler.BeginSample("Animator.GetParameter");
            int hash = 0;
            for (int i = 0; i < animator.parameterCount; i++)
            {
                hash = animator.GetParameter(i).nameHash;
            }
            Profiler.EndSample();
        }

        // 42byte
        {
            Profiler.BeginSample("tag equals");
            bool ret = gameObject.tag == "hoge";
            Profiler.EndSample();
        }

        // 0byte
        {
            Profiler.BeginSample("tag CompareTag");
            bool ret = gameObject.CompareTag("hoge");
            Profiler.EndSample();
        }

        // リフレクション呼び出しがあるがIL2CPPビルドだとGC Allocが少なくなる
        // 4.2KB
        {
            Profiler.BeginSample("SupportsTextureFormat 0");
            bool result = SystemInfo.SupportsTextureFormat(TextureFormat.ETC2_RGB);
            Profiler.EndSample();
        }

        // キャッシュされるので2回目の呼び出しはGC Allocが少なくなる
        // 400B
        {
            Profiler.BeginSample("SupportsTextureFormat 1");
            bool result = SystemInfo.SupportsTextureFormat(TextureFormat.ETC2_RGB);
            Profiler.EndSample();
        }

        // SupportsTextureFormatが呼び出されている
        // 1.1KB
        {
            Profiler.BeginSample("new Texture2D");
            var tex = new Texture2D(1, 1);
            Destroy(tex);
            Profiler.EndSample();
        }

        // 40byte
        {
            Profiler.BeginSample("LayerToName");
            string layer = LayerMask.LayerToName(0);
            Profiler.EndSample();
        }

        // 0byte
        {
            Profiler.BeginSample("NameToLayer");
            int layer = LayerMask.NameToLayer("Default");
            Profiler.EndSample();
        }

        // 40byte
        {
            Profiler.BeginSample("Camera.main 0");
            var camera = Camera.main;
            Profiler.EndSample();
        }

        // 0byte
        {
            Profiler.BeginSample("Camera.main 1");
            var camera = Camera.main;
            Profiler.EndSample();
        }
    }
}
