using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class InvokeTest : MonoBehaviour
{
	void Func()
	{

	}

	// Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
