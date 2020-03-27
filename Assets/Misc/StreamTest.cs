using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System.IO;

public class StreamTest : MonoBehaviour
{
    void Start()
    {
        using (var ms = new MemoryStream(1024))
        using (var bw = new BinaryWriter(ms))
        {
            // 36byte
            using (new ProfilerScope("float"))
            {
                float f = 1.0f;
                bw.Write(f);
            }

            // 0byte
            using (new ProfilerScope("int"))
            {
                int i = 1;
                bw.Write(i);
            }
        }
    }
}
