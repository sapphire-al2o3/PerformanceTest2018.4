﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System.IO;

public class FileTest : MonoBehaviour
{
	[SerializeField]
	string filePath = null;

	[SerializeField]
	string dirPath = null;

    private void Start()
    {
#if UNITY_EDITOR
        Run();
#endif
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Run();
        }
    }


    void Run()
    {
        string file = Application.dataPath + filePath;
        string dir = Application.dataPath + dirPath;

        Debug.Log(Application.dataPath);

        // 0B
        // IL2CPP 0.5KB
        // IL2CPP 2回目以降 0B
        {
            Profiler.BeginSample("Exists File");
            bool exists = File.Exists(file);
            Profiler.EndSample();

            Debug.Log(exists);
        }

        // 0.9KB
        // IL2CPP 1.5KB
        // IL2CPP 2回目以降 0.8KB
        // パスの長さに依存する
        {
            Profiler.BeginSample("Exists Directory");
            bool exists = Directory.Exists(dir);
            Profiler.EndSample();

            Debug.Log(exists);
        }
    }
}
