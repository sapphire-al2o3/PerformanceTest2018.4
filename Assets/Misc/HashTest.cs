using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.Profiling;

public class HashTest : MonoBehaviour
{
    void Start()
    {
        // 320B
        {
            Profiler.BeginSample("MD5CryptoServiceProvider");
            var md5 = new MD5CryptoServiceProvider();
            Profiler.EndSample();

            Debug.Log(md5.GetType());
        }

        // 1.5KB
        {
            Profiler.BeginSample("MD5.Create");
            var md5 = MD5.Create();
            Profiler.EndSample();

            Debug.Log(md5.GetType());
        }
    }
}
