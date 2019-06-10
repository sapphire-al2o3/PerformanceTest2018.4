using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class YeildTest : MonoBehaviour {


	IEnumerator Test()
	{
		int i = 0;

		while (true)
		{
			{
				Matrix4x4 a = Matrix4x4.identity;
				Matrix4x4 b = Matrix4x4.identity;
				Matrix4x4 c = Matrix4x4.identity;
				Matrix4x4 d = Matrix4x4.identity;
				Matrix4x4 e = Matrix4x4.identity;

				i += 1;
			}
			yield return null;
		}
	}

	void Start ()
	{
		Profiler.BeginSample("YieldTest");
		StartCoroutine(Test());
		Profiler.EndSample();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
