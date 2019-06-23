using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using UnityEngine.Profiling;

public class HashTest : MonoBehaviour
{
    void Start()
    {
        byte[] data = { 0, 1, 2, 3, 4, 5, 6, 7 };

        // 192B
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                Profiler.BeginSample("ComputeHash");
                var hash = md5.ComputeHash(data);
                Profiler.EndSample();
            }
        }

        // 228B
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                Profiler.BeginSample("TransformBlock");
                md5.TransformBlock(data, 4, 4, null, 0);
                md5.TransformFinalBlock(data, 4, 4);
                var hash = md5.Hash;
                Profiler.EndSample();
            }
        }

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
