using System.Collections;
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

    void Start()
    {
        filePath = Application.dataPath + filePath;
        dirPath = Application.dataPath + dirPath;

        Debug.Log(Application.dataPath);

        // 0B
        {
            Profiler.BeginSample("Exists File");
            bool exists = File.Exists(filePath);
            Profiler.EndSample();

            Debug.Log(exists);
        }

        // 0.9KB
        {
            Profiler.BeginSample("Exists Directory");
            bool exists = Directory.Exists(dirPath);
            Profiler.EndSample();

            Debug.Log(exists);
        }
    }
}
