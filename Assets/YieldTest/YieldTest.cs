using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class YieldTest : MonoBehaviour {
	
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

	IEnumerator Test0()
	{
		while (true)
		{
			yield return null;
		}
	}

	IEnumerator Test1()
	{
		while (true)
		{
			yield return 0;
		}
	}

	void Start ()
	{
		Profiler.BeginSample("YieldTest");
		StartCoroutine(Test());
		Profiler.EndSample();

		StartCoroutine(Test0());
		//StartCoroutine(Test1());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
